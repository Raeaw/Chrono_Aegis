using System.Collections;
using UnityEngine;

public class BossGolem : BossEnemy
{
    [Header("Golem Special Skill")]
    public GameObject laserPrefab;

    [Tooltip("Jarak minimum spawn dari player agar tidak langsung kena")]
    public float minSpawnDistanceX = 1.5f;

    [Tooltip("Jarak maksimum spawn dari player")]
    public float maxSpawnDistanceX = 3.5f;

    public float laserSpawnDelay = 0.4f; // Delay sebelum prefab muncul

    protected override void DoSpecialAbility()
    {
        StartCoroutine(SpawnLaserRoutine());
    }

    private IEnumerator SpawnLaserRoutine()
    {
        Transform player = PlayerController.Instance.transform;

        // Pilih kiri atau kanan player
        float side = (Random.value < 0.5f) ? -1f : 1f;

        // Random jarak dari player
        float offsetX = Random.Range(minSpawnDistanceX, maxSpawnDistanceX) * side;

        // Posisi spawn mengikuti Y player
        Vector3 spawnPos = new Vector3(
            player.position.x + offsetX,
            player.position.y,      // Y sama dengan player
            0
        );

        // Tunggu delay
        if (laserSpawnDelay > 0)
            yield return new WaitForSeconds(laserSpawnDelay);

        // Spawn laser
        Instantiate(laserPrefab, spawnPos, Quaternion.identity);
    }
}