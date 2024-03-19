using UnityEngine;

[System.Serializable]
public struct Level
{
    [SerializeField] public float experience;
    [SerializeField] public float experience_threshold;
    [SerializeField] public int level;
    public Level(float Experience, float Level_threshold, int Level)
    {
        experience = Experience;
        experience_threshold = Level_threshold;
        level = Level;

    }
}