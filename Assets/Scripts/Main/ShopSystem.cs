using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSystem : MonoBehaviour
{
    public GameObject[] itemBuy;
    public int[] priceItem;
    public GameObject shopPanel;
    public Text errorSuccessText;

    private PlayerCurrency playerCurrency;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem()
    {
        playerCurrency = (PlayerCurrency)FindObjectOfType(typeof(PlayerCurrency));
        inventory = (Inventory)FindObjectOfType(typeof(Inventory));
        for (int i = 0; i < itemBuy.Length; i++)
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            if (itemBuy[i].name == EventSystem.current.currentSelectedGameObject.name)
            {
                Debug.Log("asd");
                if (playerCurrency.gold >= priceItem[i])
                {
                   if(inventory.itemAdded == false)
                   {
                        if (itemBuy[i].GetComponent<InteractionObject>().itemType == "Health Potion" || itemBuy[i].GetComponent<InteractionObject>().itemType == "Buff Potion" || itemBuy[i].GetComponent<InteractionObject>().itemType == "Medipack" || itemBuy[i].GetComponent<InteractionObject>().itemType == "Milk")
                        {
                            inventory.AddItem(itemBuy[i], itemBuy[i].GetComponent<InteractionObject>().itemType);
                        }

                        if (itemBuy[i].GetComponent<InteractionObject>().itemType == "Weapon")
                        {
                            GameObject weapon = Resources.Load<GameObject>("Prefabs/Projectile/" + itemBuy[i].name);
                            PlayerController.instance.bulletPrefab = weapon;
                        }
                        playerCurrency.gold -= priceItem[i];
                        errorSuccessText.text = "Success Buy Item!";
                        errorSuccessText.color = new Color(0, 19, 255);
                        errorSuccessText.enabled = true;
                   }
                   else if(inventory.itemAdded == true)
                   {
                        errorSuccessText.text = "Inventory Full!!";
                        errorSuccessText.color = new Color(255, 0, 0);
                        errorSuccessText.enabled = true;
                   }
                }
                else
                {
                    errorSuccessText.text = "Gold Not Enough!!";
                    errorSuccessText.color = new Color(255, 0, 0);
                    errorSuccessText.enabled = true;
                }
            }
        }
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }
}
