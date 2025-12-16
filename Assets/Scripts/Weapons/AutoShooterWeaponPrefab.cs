using UnityEngine;

public class AutoShooterWeaponPrefab : MonoBehaviour
{
    private AutoShooterWeapon weapon;
    private Vector3 direction;
    private float timer;

    public void Initialize(AutoShooterWeapon weaponRef, Vector3 dir)
    {
        weapon = weaponRef;
        direction = dir;
        timer = weapon.stats[weapon.weaponLevel].duration;

        // Optional: suara tembakan
        if (AudioController.Instance != null)
            AudioController.Instance.PlaySound(AudioController.Instance.areaWeaponSpawn);
    }

    void Update()
    {
        // gerakkan peluru
        float speed = weapon.stats[weapon.weaponLevel].speed;
        transform.position += direction * speed * Time.deltaTime;

        // hilangkan setelah durasi habis
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            collider.GetComponent<Enemy>().TakeDamage(weapon.stats[weapon.weaponLevel].damage);
            // Optional: efek suara / ledakan
            Destroy(gameObject);
        }
    }
}
