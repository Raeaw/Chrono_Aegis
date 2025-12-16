using UnityEngine;

public class DashWeapon : Weapon
{
    private float dashCooldownTimer;
    private bool isDashing;
    private float dashTimer;

    private PlayerController player;

    void Start()
    {
        player = PlayerController.Instance;
    }

    void Update()
    {
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                player.moveSpeed /= 3.5f;
                player.animator.SetBool("dash", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0)
        {
            StartDash();
        }

        UIController.Instance.UpdateDashCooldownSlider(dashCooldownTimer, stats[weaponLevel].cooldown);
    }

    private void StartDash()
    {
        if (player.playerMoveDirection == Vector2.zero)
            return;

        dashTimer = stats[weaponLevel].duration;
        dashCooldownTimer = stats[weaponLevel].cooldown;
        isDashing = true;

        player.moveSpeed *= 3.5f;
        AudioController.Instance.PlaySound(AudioController.Instance.playerDash);
        player.animator.SetBool("dash", true);
    }
}
