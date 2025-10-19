using UnityEngine;

public class ShootingAim : MonoBehaviour
{
    public Transform firepoint1;
    public Transform firepoint2;
    public GameObject bulletPrefabs;

    public float bulletForce = 20f;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Fire(firepoint1);
        Fire(firepoint2);
    }

    void Fire(Transform firepoint)
    {
        GameObject bullet = Instantiate(bulletPrefabs, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
