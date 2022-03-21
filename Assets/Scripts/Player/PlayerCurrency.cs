using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurrency : MonoBehaviour
{
    public int gold;
    public Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = gold.ToString();
    }

    public void AddGold(int goldToAdd)
    {
        gold += goldToAdd;
    }
}
