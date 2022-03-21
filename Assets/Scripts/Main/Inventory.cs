using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory;
    public Image[] imageInvent;
    public string[] inventoryType;
    public bool itemAdded = false;

    private PlayerHealthManager playerHealthManager;
    // Start is called before the first frame update
    void Start()
    {
        inventory = new GameObject[6];
        inventoryType = new string[6];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(GameObject item, string itemType)
    {
        // Find the first open slot in the inventory
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                inventoryType[i] = itemType;
                DontDestroyOnLoad(inventory[i]);
                SpriteRenderer spriteRenderer = inventory[i].GetComponent<SpriteRenderer>();
                imageInvent[i].sprite = spriteRenderer.sprite;
                // Do something with the object
                //item.SendMessage("DoInteraction");
                break;
            }
            else
            {
                if(i == inventory.Length - 1)
                {
                    itemAdded = true;
                }
            }
        }
    }

    public void UseItem()
    {
        itemAdded = false;
        int index = int.Parse(EventSystem.current.currentSelectedGameObject.name) - 1;
        playerHealthManager = (PlayerHealthManager)FindObjectOfType(typeof(PlayerHealthManager));
        if (inventoryType[index] == "Health Potion")
        {
            if (playerHealthManager.playerCurrentHealth <= playerHealthManager.playerMaxHealth)
            {
                playerHealthManager.playerCurrentHealth += Random.Range(25, 50);

                if (playerHealthManager.playerCurrentHealth > playerHealthManager.playerMaxHealth)
                {
                    playerHealthManager.SetMaxHealth();
                }
            }
        }
        else if(inventoryType[index] == "Medipack")
        {
            if (playerHealthManager.playerCurrentHealth <= playerHealthManager.playerMaxHealth)
            {
                playerHealthManager.SetMaxHealth();
            }
        }
        else if(inventoryType[index] == "Milk")
        {
            if (playerHealthManager.playerCurrentHealth <= playerHealthManager.playerMaxHealth)
            {
                playerHealthManager.playerCurrentHealth += 50;

                if (playerHealthManager.playerCurrentHealth > playerHealthManager.playerMaxHealth)
                {
                    playerHealthManager.SetMaxHealth();
                }
            }
        }
        else if(inventoryType[index] == "Buff Potion")
        {
            PlayerController.instance.speed = 6f;
        }
        inventory[index] = null;
        inventoryType[index] = "";
        imageInvent[index].sprite = null;
    }
}
