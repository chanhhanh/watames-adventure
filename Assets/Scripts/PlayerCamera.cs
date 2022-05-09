using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{

    public Transform Player;
    public float smoothing;
    public Vector3 offset;
    Vector3 oldPos = new Vector3(0,0,0);

    #region Singleton
    public static PlayerCamera Instance;
    void Awake()
    {
        Instance = this;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if(Player)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, Player.transform.position + offset, smoothing);
            transform.position = newPos;
            oldPos = transform.position;
        }
        else if (!Player)
        { transform.position = oldPos; }
    }

    public IEnumerator ShakeOnce(float magnitude, float duration)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
