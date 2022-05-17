using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level")]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public string name;
        public int sceneIndex;
        public Texture2D thumb;
    }
    public Level level;
}
