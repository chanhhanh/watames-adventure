using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDecay : MonoBehaviour
{
    // Start is called before the first frame update
    public float decayTime = 10;
    void Start()
    {
        StartCoroutine(DestroySpell());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroySpell()
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}
