using System.Collections.Generic;
using UnityEngine;

public class SpawnWeaponPrefab : MonoBehaviour
{
    private SpawnWeapon weapon;
    private float lifeTimer;
    private float damageCounter;               // counter lokal untuk interval damage
    public List<Enemy> enemiesInRange = new List<Enemy>();

    // Optional: apakah mau pause saat Time.timeScale == 0
    [SerializeField] private bool pauseOnTimeScaleZero = true;

    // Panggil ini saat instantiate (lebih aman daripada mencari object di scene)
    public void Initialize(SpawnWeapon weaponRef)
    {
        weapon = weaponRef;

        // Durasi hidup sesuai stats (jangan gunakan Destroy(gameObject, ...) yang bergantung pada timeScale; kita tetap pakai Destroy di Start atau Update)
        lifeTimer = weapon.stats[weapon.weaponLevel].duration;

        // Inisialisasi damageCounter dari speed stat (clamp minimal supaya tidak jadi 0)
        float speed = Mathf.Max(0.0001f, weapon.stats[weapon.weaponLevel].speed);
        damageCounter = speed;
    }

    void Start()
    {
        // Jika kamu juga memanggil Destroy(gameObject, lifeTimer) di sini,
        // jangan lupa bahwa Destroy memakai scaled time.
        // Kita gunakan Destroy di Update agar konsisten dengan pause jika diinginkan:
        // Destroy(gameObject, lifeTimer);
    }

    void Update()
    {
        // Jika prefab belum di-initialize, skip (atau cari referensi fallback)
        if (weapon == null) return;

        // Jika ingin pause saat timescale 0
        if (pauseOnTimeScaleZero && Time.timeScale == 0f) return;

        // Hitung mundur life
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        // Damage periodik: gunakan damageCounter lokal — JANGAN ubah weapon.stats!
        damageCounter -= Time.deltaTime;
        if (damageCounter <= 0f)
        {
            // Reset counter ke nilai speed saat ini (ambil lagi dari stats, jika weaponLevel berubah di runtime)
            float speed = Mathf.Max(0.0001f, weapon.stats[weapon.weaponLevel].speed);
            damageCounter = speed;

            // Iterasi aman: gunakan loop mundur dan bersihkan null entry
            for (int i = enemiesInRange.Count - 1; i >= 0; i--)
            {
                Enemy e = enemiesInRange[i];
                if (e == null)
                {
                    enemiesInRange.RemoveAt(i);
                    continue;
                }

                // Pastikan enemy masih valid (misal dalam range)
                e.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
                enemiesInRange.Remove(enemy);
        }
    }
}
