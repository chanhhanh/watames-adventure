using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    Transform player;
    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }
    void FixedUpdate()
    {
        if(FindBox() && player)
        {
            PointTo(FindBox().transform);
        }
        else if (FindBoss() && player)
        {
            PointTo(FindBoss().transform);
        }
    }
    
    GameObject FindBox()
    {
        GameObject tr = GameObject.FindGameObjectWithTag("Box");
        return tr;
    }
    GameObject FindBoss()
    {
        GameObject tr = PlayerStats.Instance.m_boss.m_boss;
        return tr;
    }
    void PointTo(Transform transform)
    {
        Vector2 direction = (transform.position - player.transform.position).normalized;
        float facingDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 target = new Vector3(transform.rotation.x, transform.rotation.y, facingDirection);
        gameObject.transform.eulerAngles = target;
    }
}
