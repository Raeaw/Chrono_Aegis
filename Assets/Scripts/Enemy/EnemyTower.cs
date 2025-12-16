using UnityEngine;

public class EnemyTower : Enemy
{
    [Header("Tower Shooting Settings")]
    public float attackRange = 8f;
    public float fireRate = 1.5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    private float fireCounter;

    void Start()
    {
        canMove = false; // Tower tidak jalan seperti Enemy biasa
    }

    void Update()
    {
        if (isDead) return;

        // face player
        spriteRenderer.flipX = (PlayerController.Instance.transform.position.x < transform.position.x);

        fireCounter -= Time.deltaTime;

        float distance = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distance <= attackRange && fireCounter <= 0f)
        {
            Shoot();
            fireCounter = fireRate;
        }
    }

    private void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector2 dir = (PlayerController.Instance.transform.position - transform.position).normalized;

        Rigidbody2D prb = proj.GetComponent<Rigidbody2D>();
        if (prb != null)
            prb.velocity = dir * projectileSpeed;
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        // Tower tidak memberikan collision damage
    }
}
