using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController Instance;
    public DamageNumber prefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void CreateNumber(float value, Vector3 location)
    {
        // Offset acak di sekitar lokasi
        float offsetX = Random.Range(-0.05f, 0.05f); // sekitar 5 pixel kiri/kanan
        float offsetY = Random.Range(-0.05f, 0.05f); // sekitar 5 pixel atas/bawah

        Vector3 randomOffset = new Vector3(offsetX, offsetY, 0f);

        // Instantiate dengan offset
        DamageNumber damageNumber = Instantiate(prefab, location + randomOffset, Quaternion.identity, transform);
        damageNumber.SetValue(Mathf.RoundToInt(value));
    }

}
