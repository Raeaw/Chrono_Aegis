using UnityEngine;

public class LaserPrefab : MonoBehaviour
{
    public float duration = 2f;
    public float damageInterval = 0.5f;
    public float damageAmount = 10f;
    private float damageTimer;
    private Transform player;

    private void Start()
    {
        damageTimer = damageInterval;
        Destroy(gameObject, duration);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        FlipTowardPlayer();
    }

    private void Update()
    {   
        damageTimer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Hitung mundur timer
            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
                return;
            }

            // Timer habis → berikan damage
            PlayerController.Instance.TakeDamage(damageAmount);

            // Reset timer setelah memberi damage
            damageTimer = damageInterval;
        }
    }


    void FlipTowardPlayer()
    {
        if (player == null) return;

        // Jika prefab berada di kiri player → tidak flip
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        else
        {
            // Jika prefab berada di kanan player → flip
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }
}
