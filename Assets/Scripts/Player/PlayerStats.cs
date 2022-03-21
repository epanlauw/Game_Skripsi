using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int currentLevel;
    public int currentExp;
    public int currentHP;
    public int baseExp;

    public int maxLevel = 100;

    public int[] toLevelUp;

    public Text textLevel;

    public PlayerHealthManager playerHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        toLevelUp = new int[maxLevel];
        toLevelUp[1] = baseExp;

        for(int i = 2; i < toLevelUp.Length; i++)
        {
            toLevelUp[i] = Mathf.FloorToInt(toLevelUp[i - 1] * 1.1f);
        }

        playerHealthManager = FindObjectOfType<PlayerHealthManager>();

        textLevel.text = "Level :" + (int)(currentLevel + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentExp > toLevelUp[currentLevel])
        {
            currentExp -= toLevelUp[currentLevel];
            currentLevel++;
            textLevel.text = "Level :" + (int)(currentLevel + 1);
            playerHealthManager.playerMaxHealth = Mathf.FloorToInt(playerHealthManager.playerMaxHealth * 1.1f);
            playerHealthManager.SetMaxHealth();
        }
    }

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
    }
}
