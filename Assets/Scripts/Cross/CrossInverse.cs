using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossInverse : MonoBehaviour
{
    public float projectileForce;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InverseVel());
    }


    IEnumerator InverseVel()
    {
        float maxProjectileForce = projectileForce * 1.6f;
        yield return new WaitForSeconds(0.2f);
        while(projectileForce > 0)
        {
            projectileForce -= 0.25f;
            gameObject.GetComponent<Rigidbody2D>().velocity = projectileForce * direction;
            yield return new WaitForSeconds(0.01f);
        }
        while (projectileForce < maxProjectileForce)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = -1 * projectileForce * direction;
            gameObject.GetComponent<Rigidbody2D>().AddTorque(1f);
            projectileForce += 0.25f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
