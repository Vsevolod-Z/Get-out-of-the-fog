using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    [SerializeField]
    private UIController uIController;
    [SerializeField]
    private int money;
    [SerializeField]
    private int ammo;
    [SerializeField]
    private int grenade;
    [SerializeField]
    private int inventoryWeight;
    [SerializeField]
    private MyDataBase dataBase;
    private void Start()
    {
        dataBase = Camera.main.GetComponent<MyDataBase>();
    }
    public int _money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }
    public int _ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo += value;
        }
    }
    public int _grenade
    {
        get
        {
            return grenade;
        }
        set
        {
            grenade = value;
        }
    }

    public int _inventoryWeight
    {
        get
        {
            return inventoryWeight;
        }
        set
        {
            inventoryWeight += value;
        }
    }

    public bool AddItem ( Item item , int amount = 1)
    {
       
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.isEmpty)
            {
                slot.FillSlot(item, amount);
                inventoryWeight += Convert.ToInt32(item._itemWeight) * amount;
                uIController.UpdateUI();
                return true;
            }
            if (slot._itemID == item._itemID && !slot.isFull)
            {
                slot.AddExistingItem(amount);
                inventoryWeight += Convert.ToInt32( item._itemWeight )* amount;
                uIController.UpdateUI();
                return true;
            }
        }
            StartCoroutine(uIController.MakeNotification("Inventory full"));
            return false;
    }
    public bool RemoveItem(Item item, int amount = 1) 
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot._itemID == item._itemID && !slot.isEmpty)
            {
                if (slot.RemoveItem(amount)) 
                {
                    inventoryWeight -= Convert.ToInt32(item._itemWeight) * amount;
                    uIController.UpdateUI();
                    return true;
                }
                else
                {
                    return false;
                }
            }
           
        }
        return false;
    }
    public int[] GetInventoryValues()
    {
        int[] values = new int[inventorySlots.Count + 2];
        foreach (InventorySlot inventorySlot in inventorySlots)
        {
            if (!inventorySlot.isEmpty)
            {
                if (inventorySlot._itemID == 1 || inventorySlot._itemID == 2)
                    values[inventorySlot._itemID - 1] = inventorySlot._amount;
            }
        }
        values[2] = money;
        values[3] = inventoryWeight;

        return values;
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveData();
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
        }
    }
    public void SaveData()
    {
        dataBase.SaveCharacterData();
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].isEmpty)
            {
                if (dataBase.GetCharactersInventoryItem(inventorySlots[i]))
                {
                    dataBase.SaveExistCharacterInventoryData(inventorySlots[i]);
                }
                else
                {
                    dataBase.SaveCharacterInventoryData(inventorySlots[i]);
                }
            }
        }
    }
}
