using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotateToCursor : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    Vector3 mousePos;
    Vector2 tr;
    Vector2 reverseTr;
    void Awake()
    {
        tr = GetComponent<Transform>().localPosition;
        reverseTr = new Vector2(Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
    }

    void FixedUpdate()
    {

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePos - transform.position).normalized;
        float currDir = (int)(direction.x * 2);
        float y = 0f;
        if (direction.x > 0)
        {
            GetComponent<Transform>().localPosition = tr;
        }
        else if (direction.x < 0)
        {
            y = -180f;
            GetComponent<Transform>().localPosition = reverseTr;
        }

        Quaternion target = Quaternion.Euler(0f, y, 0f);

        GetComponent<Transform>().rotation = target;
        
    }
}
