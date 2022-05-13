using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualHandgunToggle : MonoBehaviour
{
    bool handgunToggle = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ToggleHandgun();
        }
    }

    void ToggleHandgun()
    {
        if (!handgunToggle)
        {
            transform.localPosition = new Vector3(transform.localPosition.x * -1, 0.25f, 0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            //transform.SetPositionAndRotation(new Vector3(0.35f, 0.25f, 0f), Quaternion.Euler(0f, 0f,0f));
        }
        else if (handgunToggle)
        {
            transform.localPosition = new Vector3(transform.localPosition.x * -1, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, -180f, 0f);
            //transform.SetPositionAndRotation(new Vector3(-0.35f, 0f, 0f), Quaternion.Euler(0f, -180f, 0f));
        }
        handgunToggle = !handgunToggle;
    }
}
