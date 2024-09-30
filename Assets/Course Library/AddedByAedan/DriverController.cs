using UnityEngine;

public class DriverController : MonoBehaviour
{
    public int carModel; //Current car model order
    public GameObject[] carModels; //Car models on standby
    public Transform[] wheels; //Active wheels
    public Collider[] cols; //Colliders on standby
    public Rigidbody rBody;
    public SwitchCar SC;
    public float topSpd, steerMul = 1;
    public bool CarLoaded;

    [SerializeField] private float speed;

    private float horizontalInput, forwardInput, velocity, speedMul, power = 4, brakeSpd = 40, steerDir, steerSpd, curFwd, preFwd;
    private Vector3 prev_velocity;

    public bool reversing;

    private void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (CarLoaded)
        {
            if (Input.anyKey)
            {
                //This is where we get player input
                forwardInput = Input.GetAxis("Vertical");
                horizontalInput = Input.GetAxis("Horizontal");

                if (forwardInput != 0)
                {
                    curFwd = Mathf.Sign(forwardInput);

                    if (!reversing)
                    {
                        if (curFwd != preFwd && preFwd != 0) //If direction changed (forward/backward)
                            Brake();
                        if (speedMul < rBody.mass)
                            speedMul += power / (Mathf.Max(speedMul, rBody.mass / 2) * 2 / rBody.mass);
                        else
                            speedMul -= brakeSpd / (Mathf.Max(speedMul, rBody.mass / 2) * 2 / rBody.mass);
                    }
                    else
                    {
                        curFwd = preFwd;
                        Brake();
                    }
                }

                //Wheels spin with car velocity
                foreach (Transform w in wheels)
                    w.Rotate(Vector3.right, velocity * forwardInput, Space.Self);

                //Rotate front wheels for more realistic car response
                if (horizontalInput != 0)
                    steerDir = Mathf.Clamp(steerDir + steerSpd * 100 * Time.deltaTime, 0, 30);
                else
                    steerDir = Mathf.Clamp(steerDir - steerSpd * 100 * Time.deltaTime, 0, 30);
                wheels[0].parent.localEulerAngles = new Vector3(0, steerDir * horizontalInput, 0);
                wheels[1].parent.localEulerAngles = wheels[0].parent.localEulerAngles;
            }
            else
            {
                preFwd = curFwd;
            }

            if (Input.GetAxis("Vertical") == 0 && speedMul > 0) //When not driving AUTO BRAKE
                Brake();
            else if (speedMul < 0)
            {
                speedMul = 0;
                if (reversing)
                {
                    reversing = false;
                    preFwd = curFwd;
                }
            }

            if (Input.GetAxis("Horizontal") == 0 && steerDir != 0) //steer front wheels to straight angle when not turning
            {
                steerDir = Mathf.Clamp(steerDir - steerSpd * 100 * Time.deltaTime, 0, 30);
                wheels[0].parent.localEulerAngles = new Vector3(0, steerDir * horizontalInput, 0);
                wheels[1].parent.localEulerAngles = wheels[0].parent.localEulerAngles;
            }
            speed = Mathf.Round(speedMul / rBody.mass * topSpd);
            if (curFwd < 0) //Reduce speed by twice when going backward
            {
                speed /= 2;
                steerSpd = 0.5f * steerMul;
            }
            else
                steerSpd = 1 * steerMul;

            //Move the vehicle forward or backward.
            transform.Translate(curFwd * speed * 0.2f * Time.deltaTime * Vector3.forward);
            //We steer the vehicle
            transform.Rotate(Vector3.up, Time.deltaTime * (Mathf.Abs(steerDir) * 3) * (speedMul / (rBody.mass / 2)) * Input.GetAxis("Horizontal") * curFwd);

            velocity = (transform.position - prev_velocity).magnitude / Time.deltaTime;
            prev_velocity = transform.position;
        }
    }
    public void Brake()
    {
        reversing = true;
        speedMul -= brakeSpd / (Mathf.Max(speedMul, rBody.mass / 2) * 2 / rBody.mass);
    }
}
