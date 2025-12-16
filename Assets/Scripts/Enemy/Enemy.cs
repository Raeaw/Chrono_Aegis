using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject destroyEffect;

    [SerializeField] public float moveSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float destroyTime;
    [SerializeField] public float health;
    [SerializeField] public float pushTime;
    [SerializeField] public int experienceToGive;

    public Vector2 knockbackDirection;
    public Vector3 direction;
    public float pushCounter;
    public bool isDead = false;
    [SerializeField] protected bool canMove = true;

    protected virtual void Start()
    {
        
    }

    public void FixedUpdate()
    {
        if (isDead) return;

        if (PlayerController.Instance.gameObject.activeSelf)
        {
            // face the player
            spriteRenderer.flipX = (PlayerController.Instance.transform.position.x < transform.position.x);

            if (canMove)
            {
                if (pushCounter > 0)
                {
                    pushCounter -= Time.deltaTime;
                    rb.velocity = knockbackDirection * moveSpeed;
                }
                else
                {
                    direction = (PlayerController.Instance.transform.position - transform.position).normalized;
                    rb.velocity = direction * moveSpeed;
                }
            }
            else
            {
                rb.velocity = Vector2.zero; // tower diam
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            PlayerController.Instance.TakeDamage(damage);
        }
    }

    public void SpawnDestroyEffect()
    {
        Instantiate(destroyEffect, transform.position, transform.rotation);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        DamageNumberController.Instance.CreateNumber(damage, transform.position);
        AudioController.Instance.PlayModifiedSound(AudioController.Instance.enemyTakeDamage);

        // Hitstun + knockback
        pushCounter = pushTime;
        knockbackDirection = (transform.position - PlayerController.Instance.transform.position).normalized;

        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("alive", false);
            rb.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            PlayerController.Instance.GetExperience(experienceToGive);
            AudioController.Instance.PlaySound(AudioController.Instance.enemyDie);

            Invoke(nameof(SpawnDestroyEffect), destroyTime);
            Destroy(gameObject, destroyTime);
        }
    }
}
