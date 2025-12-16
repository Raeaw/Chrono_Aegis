using System.Collections.Generic;
using UnityEngine;

public class AreaWeaponPrefab : MonoBehaviour
{
    public AreaWeapon weapon;
    private Vector3 originalSize;
    private Vector3 targetSize;
    private float timer;
    public float counter;
    public List<Enemy> enemiesInRange;
    private bool hasPlayedDespawnSound = false;

    void Start()
    {
        weapon = GameObject.Find("Area Weapon").GetComponent<AreaWeapon>();
        Destroy(gameObject, weapon.stats[weapon.weaponLevel].duration);

        // Simpan ukuran asli prefab (misalnya sudah kamu set ke 7.5 di Inspector)
        originalSize = transform.localScale * weapon.stats[weapon.weaponLevel].range;

        // Set scale awal = 0, nanti membesar ke ukuran asli
        transform.localScale = Vector3.zero;
        targetSize = originalSize;

        timer = weapon.stats[weapon.weaponLevel].duration;
        AudioController.Instance.PlaySound(AudioController.Instance.areaWeaponSpawn);
    }

    void Update()
    {
        // Grow
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 8f);

        // Shrink setelah durasi habis
        timer -= Time.deltaTime;
        if (timer <= 0.5f && !hasPlayedDespawnSound)
        {
            hasPlayedDespawnSound = true; // tandai sudah dimainkan
            AudioController.Instance.PlaySound(AudioController.Instance.areaWeaponDespawn);
        }

        if (timer <= 0)
        {
            targetSize = Vector3.zero;
            if (transform.localScale.x <= 0f)
            {
                Destroy(gameObject);
            }
        }

        //Periodic damage
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            counter = weapon.stats[weapon.weaponLevel].speed;
            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i] != null)
                {
                    enemiesInRange[i].TakeDamage(weapon.stats[weapon.weaponLevel].damage);
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        { 
            enemiesInRange.Add(collider.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collider.GetComponent<Enemy>());
        }
    }
}
