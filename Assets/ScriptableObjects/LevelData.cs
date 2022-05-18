using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level")]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class Level
    {
        public string name;
        public int sceneIndex;
        public Sprite thumb;
    }
    public Level level;
}
