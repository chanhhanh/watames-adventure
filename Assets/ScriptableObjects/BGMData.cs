using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BGMData", menuName ="ScriptableObjects/BGM")]
public class BGMData : ScriptableObject
{
  [System.Serializable]
  public class BGM
    {
        public AudioClip main;
        public AudioClip transition;
        public AudioClip boss;
    }
    public BGM bgm;
}
