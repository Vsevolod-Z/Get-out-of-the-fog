using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.UI;

public class MyDataBase : MonoBehaviour
{

    [SerializeField]
    UIController uiController;
    [SerializeField]
    private List<Sprite> itemSprites = new List<Sprite>();
    [SerializeField]
    public List<Item> items = new List<Item>();
    [SerializeField]
    public SqliteConnection dbconnection;
    [SerializeField]
    private string path;
    [SerializeField]
    Inventory userInventory;
    [SerializeField]
    private AdminPanel adminPanel;
    [SerializeField]
    public bool isAdmin = false;
    List<Account> accounts = new List<Account>();
    List<Character> characters = new List<Character>();
    List<CharacterInventoryItem> charactersInventory= new List<CharacterInventoryItem>();
    // Start is called before the first frame update
    void Awake()
    {

        if (PlayerPrefs.GetString("playerPrivilegess").ToString() == "true")
            isAdmin = Convert.ToBoolean(PlayerPrefs.GetString("playerPrivilegess"));
        if (isAdmin)
            uiController.adminPanelButton.SetActive(true);
        setConnection();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setConnection()
    {
        path = Application.dataPath + "/StreamingAssets/GOOTFdb.db";
        dbconnection = new SqliteConnection("URI=file:" + path);
        dbconnection.Open();
        if (dbconnection.State == ConnectionState.Open)
        {
            FillItemsList();
            FillInventory();
            SetCharacterValues();
        }
        else
        {
            Debug.Log("error connection");
        }
        
    }

    private void FillItemsList()
    {

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT * FROM Items";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Item item = new Item(reader[0], reader[1], reader[2], reader[3], itemSprites[Convert.ToInt32(reader[0].ToString()) - 1]);
            items.Add(item);
        }
    }
    private void FillInventory()
    {
       
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT item_id,item_count FROM CharacterInventory WHERE character_id =" + "\"" + PlayerPrefs.GetInt("characterID") + "\"";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            userInventory.AddItem(items[Convert.ToInt32(reader[0].ToString()) - 1], Convert.ToInt32(reader[1].ToString()));
        }
     
    }
    private void SetCharacterValues()
    {
        string[] pos;
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT character_money,character_health,character_pos FROM Character WHERE character_id =" + "\"" + PlayerPrefs.GetInt("characterID") + "\"";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            userInventory._money = Convert.ToInt32(reader[0].ToString());
            
            pos = reader[2].ToString().Split(' ');
            Debug.Log(reader[2].ToString());
            userInventory.gameObject.transform.position = new Vector3(Convert.ToInt32(pos[0]), Convert.ToInt32(pos[1]), Convert.ToInt32(pos[2]));
        }
    }
        public Item GetItem(int index)
    {
        return items[index];
    }
    public int GetItemsCount()
    {
        return items.Count;
    }
    public void SaveCharacterData()
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "UPDATE Character SET character_money = \""+ userInventory._money+ "\", character_health = \"" + 100 + "\", character_pos = \"" + Convert.ToInt32(userInventory.gameObject.transform.position.x) +" " + Convert.ToInt32(userInventory.gameObject.transform.position.y) +" " + Convert.ToInt32(userInventory.gameObject.transform.position.z)+ "\" WHERE character_id LIKE \'" + PlayerPrefs.GetInt("characterID") + "\'";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
        }
      
    }public void SaveExistCharacterInventoryData(InventorySlot inventorySlot)
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "UPDATE CharacterInventory SET item_count =\"" + inventorySlot._amount + "\" WHERE character_id LIKE \'" + PlayerPrefs.GetInt("characterID") + "\' AND item_id LIKE \'" + inventorySlot._itemID + "\'";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader[0].ToString());
        }
      
    }
    public void SaveCharacterInventoryData(InventorySlot inventorySlot)
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "INSERT INTO CharacterInventory (\'character_id\', \'item_id\', \'item_count\') VALUES(" + "\'" + PlayerPrefs.GetInt("characterID") + "\',"+ "\'" + inventorySlot._itemID+ "\'," + "\'" + inventorySlot._amount + "\'" + ")";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
        }
      
    }

    public void GetAccountsAndCharactersData()
    {

        string accountName = "";
        Account newAccount = new Account("", "", "", "");
        int counter = 0;

        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT acc.account_id,acc.account_name,acc.account_password,acc.account_privilegess,cha.character_id,cha.character_name,cha.character_money,cha.character_health,cha.character_pos FROM Account acc LEFT JOIN Character cha ON cha.account_id = acc.account_id";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Character newCharacter = new Character(reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[0].ToString());

            if (reader[1].ToString() != accountName)
            {
                if(counter!=0)
                accounts.Add(newAccount);
                newAccount = new Account(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                accountName = reader[1].ToString();
            }
            characters.Add(newCharacter);

          
            counter++;
        }
        accounts.Add(newAccount);
        adminPanel.FillAccountsAndCharactersDataLists(accounts, characters);
    }
    public void GetCharactersInventoryData()
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT character_id,item_id,item_count FROM CharacterInventory";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            CharacterInventoryItem newItem = new CharacterInventoryItem(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            charactersInventory.Add(newItem);
        }
        adminPanel.FillCharactersInventory(charactersInventory);
    }
    public bool GetCharactersInventoryItem(InventorySlot inventorySlot)
    {
        string count = string.Empty;
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT item_count FROM CharacterInventory WHERE character_id =" + "\'" + PlayerPrefs.GetInt("characterID") + "\' AND item_id =" + "\'" + inventorySlot._itemID + "\'";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            count = reader.ToString();
            if(count != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
