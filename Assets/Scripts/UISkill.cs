using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkill : MonoBehaviour
{
    public static UISkill Instance;

    [Header("Weapon List")]
    [SerializeField] private TMP_Text areaWeapon;
    [SerializeField] private TMP_Text spawnWeapon;
    [SerializeField] private TMP_Text shooterWeapon;
    [SerializeField] private TMP_Text dashWeapon;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    void Start()
    {
        UpdateSkillLevel();
    }

    public void UpdateSkillLevel()
    {
        areaWeapon.text = "Lv " + (PlayerController.Instance.activeWeapon.weaponLevel + 1);
        spawnWeapon.text = "Lv " + (PlayerController.Instance.spawnWeapon.weaponLevel + 1);
        shooterWeapon.text = "Lv " + (PlayerController.Instance.autoShooterWeapon.weaponLevel + 1);
        dashWeapon.text = "Lv " + (PlayerController.Instance.dashWeapon.weaponLevel + 1);
    }
}
