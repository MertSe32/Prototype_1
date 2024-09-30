using UnityEngine;

public class SwitchCar : MonoBehaviour
{
    public DriverController DC; //For switching/modifying vehicle (Optional)

    void Start()
    {
        SwitchModel(false, 0, 2000, 160, 0.8f);
        DC.CarLoaded = true;
    }

    private void Update()
    {
        if(Input.anyKey)
            if (Input.GetKeyUp(KeyCode.C)) //If shift + c pressed
                SwitchCarModel();
    }

    public void SwitchCarModel()
    {
        if (DC.carModel < DC.carModels.Length - 1) //Making sure current car model isn't the last model order
            DC.carModel++;
        else
            DC.carModel = 0;
        SwitchModel(false, DC.carModel, 0, 0, 0);
    }

    public void SwitchModel(bool modified, int model, float weight, float top_speed, float steer_mul)
    {
        DC.carModel = model;
        foreach (GameObject cmdl in DC.carModels) //Hide all car models
            cmdl.SetActive(false);
        DC.carModels[model].SetActive(true); //Show selected car model
        for (int i = 0; i < DC.wheels.Length; i++) //Reassign wheels of current car model as active wheels
            DC.wheels[i] = DC.carModels[model].transform.GetChild(i);
        DC.wheels[0] = DC.wheels[0].GetChild(0); DC.wheels[1] = DC.wheels[1].GetChild(0);
        foreach (Collider c in DC.cols) //Disable colliders
            c.enabled = false;
        if (model == 0)
        {
            DC.rBody.mass = 2000;
            DC.topSpd = 160;
            DC.steerMul = 0.8f;
            DC.cols[0].enabled = true; //Reenable collider with correct size
        }
        else if (model == 1)
        {
            DC.rBody.mass = 2000;
            DC.topSpd = 140;
            DC.steerMul = 0.9f;
            DC.cols[0].enabled = true;
        }
        else if (model == 2)
        {
            DC.rBody.mass = 3500;
            DC.topSpd = 100;
            DC.steerMul = 0.7f;
            DC.cols[1].enabled = true;
        }
        else if (model == 3)
        {
            DC.rBody.mass = 15000;
            DC.topSpd = 270;
            DC.steerMul = 0.6f;
            DC.cols[2].enabled = true;
        }

        if (modified) //Override car's stats if modification is applied
        {
            DC.rBody.mass = weight;
            DC.topSpd = top_speed;
            DC.steerMul = steer_mul;
        }
    }
}
