using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public Transform Player;
    public float smoothing;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, Player.transform.position + offset, smoothing);
            transform.position = newPos;
        }
           
    }
}
