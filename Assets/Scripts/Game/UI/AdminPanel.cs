using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AdminPanel : MonoBehaviour
{
    [SerializeField]
    private MyDataBase dataBase;
    [SerializeField]
    private GameObject scrollViewContent;
    [SerializeField]
    private GameObject userInfoItemPrefab;

    [SerializeField]
    private List<Account> accounts = new List<Account>();
    [SerializeField]
    private List<Character> characters = new List<Character>();
    [SerializeField]
    List<CharacterInventoryItem> characterInventoryItems = new List<CharacterInventoryItem>();
    // Start is called before the first frame update
    void Start()
    {
        
        dataBase.GetAccountsAndCharactersData();
       
    }
    public void FillAccountsAndCharactersDataLists(List<Account> _accounts , List<Character> _characters)
    {
        _characters.Sort();
        accounts= _accounts;
        characters = _characters;
        dataBase.GetCharactersInventoryData();
        for (int i = 0; i < characters.Count; i++)
        {
            accounts[characters[i].accountID-1].characters.Add(characters[i]);
        }
        for (int i = 0; i <accounts.Count; i++)
        {
               
              GameObject userInfoItem =   Instantiate(userInfoItemPrefab, scrollViewContent.transform);
               userInfoItem.GetComponent<UserInfoItem>().account = _accounts[i];
               userInfoItem.GetComponent<UserInfoItem>().FillInfo(characters , characterInventoryItems);
        }
    }
    public void FillCharactersInventory(List<CharacterInventoryItem> _characterInventoryItems)
    {
        characterInventoryItems = _characterInventoryItems;
        for (int i = 0; i < characterInventoryItems.Count; i++)
        {
            characters[Convert.ToInt32(characterInventoryItems[i]._characterID) - 1]._characterInventoryItems.Add(characterInventoryItems[i]);
        }
    }
}
