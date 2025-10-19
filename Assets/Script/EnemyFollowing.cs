using UnityEngine;

public class EnemyFollowing : MonoBehaviour
{
    public float speed = 2f;               // Kecepatan jalan musuh
    public float stopDistance = 0.2f;      // Jarak minimum sebelum berhenti (opsional)
    private Transform player;              // Target player
    private Animator animator;             // Referensi ke Animator
    private bool isDead = false;           // Cek apakah musuh sudah mati
    private SpriteRenderer spriteRenderer; // Untuk flip arah

    void Start()
    {
        // Cari player berdasarkan tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Ambil komponen animator dan sprite renderer
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            // Kalau SpriteRenderer ada di child object
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (isDead || player == null) return;

        // Hitung arah menuju player
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        // Gerak ke arah player kalau belum terlalu dekat
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

        // 🔥 Bagian baru: Flip arah musuh sesuai posisi player
        if (spriteRenderer != null)
        {
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // Menghadap kanan
            }
            else if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;  // Menghadap kiri
            }
        }
    }

    
    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (animator != null)
        {
            animator.SetBool("isMoving", false);
            animator.SetTrigger("isDead");
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
}
