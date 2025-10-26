using UnityEngine;
using System.Collections;

public class EnemyFollowing : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float stopDistance = 1.5f;

    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;

    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private bool isAttacking = false;
    private float lastAttackTime;

    void Start()
    {
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

       
        if (distance <= attackRange)
        {
            if (Time.time >= lastAttackTime + attackCooldown && !isAttacking)
            {
                StartCoroutine(Attack());
            }
            return; 
        }

        
        if (!isAttacking)
        {
            MoveTowardsPlayer();
        }

        
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            if (animator != null)
                animator.SetBool("isMoving", true);
        }
        else
        {
            if (animator != null)
                animator.SetBool("isMoving", false);
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        if (animator != null)
        {
            animator.SetBool("isMoving", false);
            animator.SetTrigger("Attack");
        }

        yield return new WaitForSeconds(0.5f); 

        // Damage ke player
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (animator != null)
        {
            animator.SetBool("isMoving", false);
            animator.SetTrigger("Die");
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        Destroy(gameObject, 2f);
    }
}
