using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractebleItem : MonoBehaviour , Iinteracteble
{
    [SerializeField]
    private MyDataBase dataBase;
    [SerializeField]
    private Item item;
    [SerializeField]
    private int amount;

    void Start()
    {
        dataBase = Camera.main.GetComponent<MyDataBase>();
        item = dataBase.items[Random.Range(0,dataBase.GetItemsCount())];
        amount = Random.Range(1, 100);
    }
    public  void  Interact(GameObject user)
    {
        Inventory userInventory = user.GetComponent<Inventory>();
        if (userInventory)
        {
            if(userInventory.AddItem(item, amount))
            Destroy(gameObject);
            
        }
    }
}
