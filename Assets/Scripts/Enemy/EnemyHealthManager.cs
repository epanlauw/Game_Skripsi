using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int expToGive;
    public int goldToGive;

    private PlayerStats playerStats;
    private PlayerCurrency playerCurrency;
    private AudioPlay audioPlay;

    private bool flashActive;
    [SerializeField]
    private float flashLength = 0f;
    private float flashCounter = 0f;
    private SpriteRenderer enemySprite;
    // Start is called before the first frame update
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        enemySprite = GetComponent<SpriteRenderer>();
        playerStats = FindObjectOfType<PlayerStats>();
        playerCurrency = FindObjectOfType<PlayerCurrency>();
        audioPlay = FindObjectOfType<AudioPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashActive)
        {
            if (flashCounter > flashLength * .99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 0f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        enemyCurrentHealth -= damageToGive;
        flashActive = true;
        flashCounter = flashLength;

        if (enemyCurrentHealth <= 0)
        {
            if (gameObject.name == "Cyclope 1")
            {
                gameObject.SetActive(false);
                PlayerController.instance.canMove = false;
                Invoke("BossEnemyDeath", 2f);
                Invoke("LoadSceneCredit", 7f);
            }
            else
            {
                Destroy(gameObject);
            }
            playerStats.AddExperience(expToGive);
            playerCurrency.AddGold(goldToGive);
            audioPlay.PlaySFX(28);
        }
    }
    public void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }

    void BossEnemyDeath()
    {
        PlayerController.instance.canControl = false;
        audioPlay.PlaySong(11);
    }

    void LoadSceneCredit()
    {
        SceneManager.LoadScene("Credit");
    }
}
