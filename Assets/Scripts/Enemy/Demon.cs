using UnityEngine;

public class DemonEnemy : Enemy
{
    [Header("Demon Teleport Settings")]
    [SerializeField] private float teleportRange = 5f;
    [SerializeField] private float teleportCooldown = 5f;
    [SerializeField] private float fadeDuration = 0.2f;

    private float teleportTimer;

    private void Update()
    {
        if (isDead) return;

        teleportTimer -= Time.deltaTime;

        if (teleportTimer <= 0)
        {
            teleportTimer = teleportCooldown;
            StartCoroutine(TeleportRoutine());
        }
    }

    private System.Collections.IEnumerator TeleportRoutine()
    {
        // Fade Out
        yield return StartCoroutine(Fade(1f, 0f));

        // lakukan teleport
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 newPosition = transform.position + (Vector3)(randomDirection * teleportRange);
        transform.position = newPosition;

        // Fade In
        yield return StartCoroutine(Fade(0f, 1f));
    }

    private System.Collections.IEnumerator Fade(float start, float end)
    {
        float timer = 0f;
        Color c = spriteRenderer.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            c.a = Mathf.Lerp(start, end, t);
            spriteRenderer.color = c;

            yield return null;
        }

        // pastikan alpha final tepat
        c.a = end;
        spriteRenderer.color = c;
    }
}
