using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitefffect;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitefffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
