using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotateToCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position).normalized;

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
    }
}
