using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject currentInterObj;
    private InteractionObject interObjScript;
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentInterObj)
        {
            // Check to see if this object is to be stored in inventory
            if (interObjScript.inventory)
            {
                inventory.AddItem(currentInterObj, interObjScript.itemType);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "InterObject")
        {
            currentInterObj = other.gameObject;
            interObjScript = other.GetComponent<InteractionObject>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "InterObject")
        {
            if(other.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }
    }
}
