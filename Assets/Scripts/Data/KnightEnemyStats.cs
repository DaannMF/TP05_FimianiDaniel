using UnityEngine;

[CreateAssetMenu(fileName = "KnightEnemyStats", menuName = "KnightEnemy/Stats")]
public class KnightEnemyStats : ScriptableObject {
    public float walkAcceleration = 50f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
}
