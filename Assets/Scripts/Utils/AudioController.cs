using JetBrains.Annotations;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [Header("UI element sfx")]
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource selectUpgrade;
    public AudioSource areaWeaponSpawn;
    public AudioSource areaWeaponDespawn;
    public AudioSource gameOver;

    [Header("Player sfx")]
    public AudioSource playerTakeDamage;
    public AudioSource playerReadyToDash;
    public AudioSource playerDash;

    [Header("Enemy sfx")]
    public AudioSource enemyTakeDamage;
    public AudioSource enemyDie;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }

    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }
}
