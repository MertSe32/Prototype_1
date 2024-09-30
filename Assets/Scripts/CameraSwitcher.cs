using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera Main_Camera;
    public Camera camera1;

    // Start is called before the first frame update
    void Start()
    {
        Main_Camera.enabled = true;
        camera1.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
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
