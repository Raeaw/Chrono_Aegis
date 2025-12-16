using UnityEngine;

public class BossEnemy : Enemy
{
    [Header("Boss Settings")]
    public float specialCooldown = 5f;
    protected float specialTimer;

    protected override void Start()
    {
        base.Start();
        specialTimer = specialCooldown;
    }

    protected virtual void Update()
    {
        if (isDead) return;

        specialTimer -= Time.deltaTime;
        if (specialTimer <= 0)
        {
            DoSpecialAbility();
            specialTimer = specialCooldown;
        }
    }

    protected virtual void DoSpecialAbility()
    {
        // akan dioverride pada boss tertentu
    }
}
