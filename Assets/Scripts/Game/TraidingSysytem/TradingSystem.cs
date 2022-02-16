using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ScrollRect))]
public class TradingSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject scrollView;
    [SerializeField]
    private GameObject scrollViewContent;
    [Header("DataBase")]
    [SerializeField]
    private MyDataBase dataBase;
    [Header("Items")]
    [SerializeField]
    private GameObject shopItemPrefab;
    [SerializeField]
    private List<Item> items = new List<Item>();
    [Header("Player")]
    [SerializeField]
    private Inventory userInventory;
    [SerializeField]
    private UIController uIController;
   

    private void Start()
    {
        items = dataBase.items;
        FillScrollViewContent();
    }

    private void FillScrollViewContent()
    {
        for (int i = 0; i < items.Count; i++)
        {
            shopItemPrefab.GetComponent<ShopItem>()._item = items[i];
            Instantiate(shopItemPrefab, scrollViewContent.transform);
        }
    }

    public void BuyItem(ShopItem shopItem) 
    { 
        if(userInventory._money - shopItem._buyItemPrice >= 0)
        {

            if (userInventory.AddItem(shopItem._item, 1))
            {
                userInventory._money -= (int)shopItem._buyItemPrice;
                uIController.UpdateUI();
            }
        }
    }
    public void SellItem(ShopItem shopItem)
    {

            if (userInventory.RemoveItem(shopItem._item, 1))
            {
                userInventory._money += (int)shopItem._sellItemPrice;
                uIController.UpdateUI();
            }

    }
}
