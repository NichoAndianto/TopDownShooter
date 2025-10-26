using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    private EnemyFollowing enemyFollow;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyFollow = GetComponent<EnemyFollowing>();
    }

    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (animator != null)
            animator.SetTrigger("Hit"); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        
        if (enemyFollow != null)
            enemyFollow.Die();

        
        if (animator != null)
            animator.SetTrigger("Die");

        
        Destroy(gameObject, 2f);
    }
}
