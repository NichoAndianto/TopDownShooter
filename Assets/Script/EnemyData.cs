using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Info")]
    public string enemyName;
    public Sprite enemyIcon;

    [Header("Stats")]
    public int maxHealth = 100;

    [Header("Movement")]
    public float moveSpeed = 2.0f;
    public float stopDistance = 1.5f;

    [Header("Attack")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1.2f;
    public int attackDamage = 10;

    [Header("Reward")]
    public int scoreValue = 50;
}
