using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Data")]
    public EnemyData data;

    private int currentHealth;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform player;   
    private bool isDead = false;
    private bool isAttacking = false;
    private float lastAttackTime;
    private float attackHitDelay = 0.4f;
    private float destroyDelay = 2f;

    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) 
            player = playerObj.transform;

        currentHealth = data.maxHealth;

        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= data.attackRange)
        {
            TryAttack();
            return;
        }

        if (!isAttacking)
            FollowPlayer();
    }

    
    void FollowPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > data.stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * data.moveSpeed * Time.deltaTime;

            if (animator != null)
                animator.SetBool("isMoving", true);
        }
        else
        {
            if (animator != null)
                animator.SetBool("isMoving", false);
        }

        if (spriteRenderer != null)
            spriteRenderer.flipX = player.position.x < transform.position.x;
    }

  
    void TryAttack()
    {
        if (Time.time >= lastAttackTime + data.attackCooldown && !isAttacking)
            StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        if (animator != null)
            animator.SetBool("isMoving", false);

        if (animator != null)
            animator.SetTrigger("Attack");

        // Default delay 0.4f
        yield return new WaitForSeconds(attackHitDelay);

        if (player != null)
        {
            PlayerHealth hp = player.GetComponent<PlayerHealth>();
            if (hp != null)
                hp.TakeDamage(data.attackDamage);
        }

        yield return new WaitForSeconds(0.2f);

        isAttacking = false;
    }

    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        StartCoroutine(FlashRed());

        if (animator != null)
            animator.SetTrigger("Hit");

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator FlashRed()
    {
        if (rend != null)
        {
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            rend.material.color = originalColor;
        }
    }

    
    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"{data.enemyName} died!");

        if (ScoreManager.instance != null)
            ScoreManager.instance.AddScore(data.scoreValue);

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (animator != null)
            animator.SetTrigger("Die");

       
        Destroy(gameObject, destroyDelay);
    }
}
