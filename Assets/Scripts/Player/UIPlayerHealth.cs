using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    private PlayerHealthManager playerHealthManager;
    public Slider healthBar;
    public Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>();

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = playerHealthManager.playerMaxHealth;
        healthBar.value = playerHealthManager.playerCurrentHealth;
        hpText.text = "HP : " + playerHealthManager.playerCurrentHealth + "/" + playerHealthManager.playerMaxHealth;
    }
}
