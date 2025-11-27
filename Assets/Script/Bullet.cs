using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject); 
        }
        Destroy(gameObject,2f);
    }

}
