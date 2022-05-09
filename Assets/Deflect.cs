using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflect : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(collision.gameObject);
        }
    }
}
