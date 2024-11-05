using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
public class PlayerStats : ScriptableObject {
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airSpeed = 3f;
    public float jumpImpulse = 10f;
}
