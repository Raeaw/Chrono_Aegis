using UnityEngine;

public class AutoShooterWeapon : Weapon
{
    [SerializeField] private GameObject prefab;     // prefab peluru atau efek tembakan
    [SerializeField] private float detectionRange = 10f;  // jangkauan mencari musuh
    private float shootCounter;

    void Update()
    {
        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0)
        {
            shootCounter = stats[weaponLevel].cooldown;
            Enemy target = FindNearestEnemy();

            if (target != null)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;

                float randomZ = Random.Range(0f, 360f);
                Quaternion random2DRotation = Quaternion.Euler(0f, 0f, randomZ);

                //instantiate peluru dan arahkan ke enemy
                GameObject bullet = Instantiate(prefab, transform.position, random2DRotation, transform);
                bullet.GetComponent<AutoShooterWeaponPrefab>().Initialize(this, direction);
            }
        }
    }

    private Enemy FindNearestEnemy()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Enemy e in enemies)
        {
            float dist = Vector2.Distance(transform.position, e.transform.position);
            if (dist < minDist && dist <= detectionRange)
            {
                minDist = dist;
                nearest = e;
            }
        }

        return nearest;
    }
}
