using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        EnemyFollowing follow = GetComponent<EnemyFollowing>();
        if (follow != null)
            follow.Die();  // 🔥 memanggil fungsi dari script EnemyFollowing

        Destroy(gameObject, 2f);
    }
}
