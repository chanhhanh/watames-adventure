using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    Transform player;
    #region Singleton
    public static Compass Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }
    void FixedUpdate()
    {
        if(FindBox())
        {
            Vector2 direction = (FindBox().position - player.transform.position).normalized;
            float facingDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Vector3 target = new Vector3(transform.rotation.x, transform.rotation.y, facingDirection);
            gameObject.transform.eulerAngles = target;
        }
    }
    
    Transform FindBox()
    {
        Transform tr = GameObject.FindGameObjectWithTag("Box").transform;
        return tr;
    }
}
