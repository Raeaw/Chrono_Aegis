using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public float moveSpeed;

    public Vector2 playerMoveDirection;
    public float playerMaxHealth;
    public float playerHealth;

    public int experience;
    public int currentLevel;
    public int maxLevel;
    public List<int> playerLevels;

    public bool isImmune;
    [SerializeField] float immunityDuration;
    [SerializeField] float immunityTimer;

    [SerializeField] public Weapon activeWeapon;
    [SerializeField] public SpawnWeapon spawnWeapon;
    [SerializeField] public DashWeapon dashWeapon;
    [SerializeField] public AutoShooterWeapon autoShooterWeapon;

    public List<Weapon> allWeapons = new List<Weapon>();


    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        allWeapons.Clear();
        allWeapons.Add(activeWeapon);
        allWeapons.Add(spawnWeapon);
        allWeapons.Add(dashWeapon);
        allWeapons.Add(autoShooterWeapon);

        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }

        playerHealth = playerMaxHealth;
        UIController.Instance.UpdateHealthSlider();
        UIController.Instance.UpdateExperienceSlider();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        playerMoveDirection = new Vector2(inputX, inputY).normalized;

        // Kirim nilai ke Animator (untuk Blend Tree arah gerak)
        animator.SetFloat("moveX", inputX);
        animator.SetFloat("moveY", inputY);

        // Tentukan apakah player sedang bergerak atau idle
        animator.SetBool("move", playerMoveDirection.magnitude > 0.1f);

        if (inputX >= 0)
            spriteRenderer.flipX = false; // menghadap kanan
        else if (inputX < 0)
            spriteRenderer.flipX = true;

        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
        } else
        {
            isImmune = false;
        }

        UIController.Instance.UpdateImmuneCooldownSlider(immunityTimer, immunityDuration);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(playerMoveDirection.x * moveSpeed, playerMoveDirection.y * moveSpeed);
    }

    public void TakeDamage(float damage)
    {
        if (!isImmune)
        {
            isImmune = true;
            immunityTimer = immunityDuration;
            AudioController.Instance.PlaySound(AudioController.Instance.playerTakeDamage);
            playerHealth -= damage;
            UIController.Instance.UpdateHealthSlider();
            if (playerHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }

    public void GetExperience(int experienceToGet)
    {
        experience += experienceToGet;
        UIController.Instance.UpdateExperienceSlider();
        if (experience >= playerLevels[currentLevel - 1])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentLevel - 1];
        currentLevel++;
        UIController.Instance.UpdateExperienceSlider();

        // --- RANDOM 3 WEAPON TANPA DUPLIKASI ---

        List<Weapon> pool = new List<Weapon>(allWeapons);

        // shuffle simple method
        for (int i = 0; i < pool.Count; i++)
        {
            int rand = Random.Range(i, pool.Count);
            Weapon temp = pool[i];
            pool[i] = pool[rand];
            pool[rand] = temp;
        }

        // Ambil 3 pertama
        Weapon option1 = pool[0];
        Weapon option2 = pool[1];
        Weapon option3 = pool[2];

        // Tampilkan di kartu UI
        UIController.Instance.LevelUpButtons[0].ActivateButton(option1);
        UIController.Instance.LevelUpButtons[1].ActivateButton(option2);
        UIController.Instance.LevelUpButtons[2].ActivateButton(option3);

        UIController.Instance.LevelUpPanelOpen();
    }

}
