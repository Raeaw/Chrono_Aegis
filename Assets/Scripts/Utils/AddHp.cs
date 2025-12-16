using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHptoPlayer()
    {
        PlayerController.Instance.playerHealth += 50;
        AudioController.Instance.PlaySound(AudioController.Instance.selectUpgrade);
        UIController.Instance.LevelUpPanelClose();
    }
}
