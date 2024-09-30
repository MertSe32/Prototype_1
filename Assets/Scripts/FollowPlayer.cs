using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0f, 5f, -7f);
    public Camera Main_Camera;
    public Camera camera1;
    private Quaternion mainCameraRotation;
    private Quaternion camera1Rotation;

    // Start is called before the first frame update
    void Start()
    {
        Main_Camera.enabled = true;
        camera1.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position - player.transform.forward + offset;
        Main_Camera.transform.rotation = mainCameraRotation;
        camera1.transform.rotation = camera1Rotation;
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchCameras();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            kameradegis();
        }
        mainCameraRotation = Main_Camera.transform.rotation;
        camera1Rotation = camera1.transform.rotation;
    }

    void SwitchCameras()
    {
        Main_Camera.enabled = false;
        camera1.enabled = true;
    }
    void kameradegis()
    {
        Main_Camera.enabled = true;
        camera1.enabled = false;
    }
}
