using UnityEngine;

public class SpawnWeapon : Weapon
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform minNode;
    [SerializeField] private Transform maxNode;

    private float spawnCounter;

    void Update()
    {
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = stats[weaponLevel].cooldown;
            int spawnCount = Mathf.Max(1, stats[weaponLevel].spawnCount); // default minimal 1

            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 spawnPos = GetRandomPosition();
                GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity);

                // berikan data ke prefab
                var prefabScript = instance.GetComponent<SpawnWeaponPrefab>();
                if (prefabScript != null)
                {
                    prefabScript.Initialize(this);
                }
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        if (minNode == null || maxNode == null)
        {
            Debug.LogWarning("Min/Max node belum di-assign di inspector!");
            return transform.position;
        }

        float randX = Random.Range(minNode.position.x, maxNode.position.x);
        float randY = Random.Range(minNode.position.y, maxNode.position.y);
        return new Vector3(randX, randY, 0);
    }
}
