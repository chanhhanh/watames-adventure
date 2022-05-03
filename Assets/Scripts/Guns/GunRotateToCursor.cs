using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotateToCursor : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    Vector3 mousePos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;

        float angle;

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

    void UpdateHangunPos()
    {
        if (gameObject.name == "handgun")
        {
            Vector2 handgunPosLeft = new Vector2(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
            Vector2 handgunPosRight = new Vector2(Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
            if (direction.x < 0)
                GetComponent<Transform>().localPosition = handgunPosLeft;
            else if (direction.x >= 0)
                GetComponent<Transform>().localPosition = handgunPosRight;
        }
    }
}
