using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCamera : MonoBehaviour
{

    public Transform Player;
    public Vector3 offset;

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
            transform.position = Player.transform.position + offset;
        }
    }

    public IEnumerator ShakeOnce(float magnitude, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.z);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
