using UnityEngine;
using TMPro;

public class TutorialFloatingText : MonoBehaviour
{
    public float duration = 10f;       // waktu bertahan
    public float floatSpeed = 0.5f;    // kecepatan naik-turun
    public float floatAmount = 10f;    // jarak naik-turun
    public float blinkSpeed = 2f;      // kecepatan berkedip

    private TMP_Text tmp;
    private CanvasGroup canvasGroup;
    private Vector3 startPos;
    private float timer;

    void Start()
    {
        tmp = GetComponent<TMP_Text>();

        // Agar bisa fade in/out dengan mudah
        canvasGroup = gameObject.AddComponent<CanvasGroup>();

        startPos = transform.localPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ===== FLOATING EFFECT (naik turun halus) =====
        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.localPosition = startPos + new Vector3(0, offsetY, 0);

        // ===== BLINK EFFECT (kelap kelip) =====
        canvasGroup.alpha = 0.7f + Mathf.Sin(Time.time * blinkSpeed) * 0.3f;

        // ===== Auto destroy setelah 10 detik =====
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
