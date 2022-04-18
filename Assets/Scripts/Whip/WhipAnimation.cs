using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipAnimation : MonoBehaviour
{
    public float maxSize = 3.5f;
    public float sizeMultiplier = 1f;
    public float growFactor = 1.2f;
    public float shrinkFactor = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleWhip());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ScaleWhip()
    {
        while(transform.localScale.x < maxSize * sizeMultiplier)
        {
            transform.localScale += new Vector3(1f, 1f, 1f) * growFactor;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        while (transform.localScale.x > 0f)
        {
            transform.localScale -= new Vector3(1f, 1f, 1f) * shrinkFactor;
            yield return new WaitForSeconds(0.01f);
        }
    } 
}
