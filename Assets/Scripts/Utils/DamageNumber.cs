using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    private float floatSpeed = 1;
    private Vector3 floatDirection;
    void Start()
    {
        floatSpeed = Random.Range(0.1f, 1.5f);
        floatDirection = (Random.value > 0.5f) ? Vector3.right : Vector3.left;
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * floatSpeed;
        transform.position += floatDirection * Time.deltaTime * Random.Range(0.1f, 1f);
    }

    public void SetValue(int value)
    {
        damageText.text = value.ToString();
    }
}
