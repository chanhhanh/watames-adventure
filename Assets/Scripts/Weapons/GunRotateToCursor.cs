using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotateToCursor : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    Vector3 mousePos;
    private float inputHorizontal;
    private float inputVertical;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float y;
        float angle;

        if (!Menu.m_gamepad)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - transform.position).normalized;


            if (direction.x < 0)
            {
                y = -180f;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1;
            }
            else
            {
                y = 0f;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            }
            Quaternion target = Quaternion.Euler(y, 0f, angle);

            GetComponent<Transform>().rotation = target;
            UpdateHangunPos();
        }
        else
        {

            inputHorizontal = Menu.instance.m_rightThumbstick.GetComponent<FixedJoystick>().Horizontal;
            inputVertical = Menu.instance.m_rightThumbstick.GetComponent<FixedJoystick>().Vertical;
            direction = new Vector2(inputHorizontal, inputVertical);

            if (direction.x < 0)
            {
                y = -180f;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1;
            }
            else
            {
                y = 0f;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            }
            Quaternion target = Quaternion.Euler(y, 0f, angle);

            GetComponent<Transform>().rotation = target;
            UpdateHangunPos();
        }
        
    }

    void UpdateHangunPos()
    {
        if (gameObject.name == "handgun")
        {
            float currDir = (int)(direction.x * 2);

            Vector2 handgunPosLeft = new Vector2(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
            Vector2 handgunPosRight = new Vector2(Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
            if (currDir == -1)
                GetComponent<Transform>().localPosition = handgunPosLeft;
            else if (currDir == 1)
                GetComponent<Transform>().localPosition = handgunPosRight;
        }
    }
}
