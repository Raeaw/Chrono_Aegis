using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;
    public float lifeTime = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
