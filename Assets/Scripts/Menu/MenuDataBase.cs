using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
public class MenuDataBase : MonoBehaviour
{
   
    [SerializeField]
    public SqliteConnection dbconnection;
    [SerializeField]
    private string path;

    private string[] userData = new string[2];
    // Start is called before the first frame update

    private void Awake()
    {
        setConnection();
    }
    public void setConnection()
    {
        path = Application.dataPath + "/StreamingAssets/GOOTFdb.db";
        dbconnection = new SqliteConnection("URI=file:" + path);
        dbconnection.Open();
        if (dbconnection.State == ConnectionState.Open)
        {
           
        }
        else
        {
            Debug.Log("error connection");
        }
    }

    public bool TryLoginAttempt(string login , string password)
    {
        
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;

        cmd.CommandText = "SELECT account_id FROM Account WHERE account_name =" + "\"" + login + "\" AND account_password =" + "\"" + password+ "\"";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log(reader[0].ToString());
            if (reader[0].ToString() != string.Empty)
            {
                GetAccountData(login);
                return true;
            }
            
        }
        
        return false;
    }

    public string[] GetAccountData(string login)
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT account_id,account_privilegess FROM Account WHERE account_name =" + "\"" + login + "\"";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            userData[0] = reader[0].ToString();
            userData[1] = (Convert.ToInt32(reader[1].ToString()) == 2) ? "true" : "false";
        }
        return userData;
    } 

    public void GetCharactersData(string playerID , GameObject[] characterIcons)
    {
        int counter = 0;
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT character_id, character_name, character_money, character_health FROM Character WHERE account_id =" + "\"" + playerID + "\"";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            
                characterIcons[counter].transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Name: " + reader[1].ToString();
                characterIcons[counter].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "Money: " + reader[2].ToString();
                characterIcons[counter].transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = "Health: " + reader[3].ToString();
                characterIcons[counter].transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>().text = reader[0].ToString();
            
            counter++;
            
        }
    }
    public void AddNewChatacter(string characterName, string accountID)
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "INSERT INTO Character (\"account_id\", \"character_name\", \"character_money\", \"character_health\", \"character_pos\") VALUES(" + "\'" + accountID + "\'," + "\'" + characterName + "\'" + ",1000, 100, '0,0,0'); ";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
           
        }
    }

    public void CreateNewAccount(string accountName , string accountPassword)
    {
        string accountID = string.Empty;
        accountID = CheckAccountExist(accountName);
        if (accountID == string.Empty)
        {


            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = dbconnection;
            cmd.CommandText = "INSERT INTO Account (\"account_name\", \"account_password\") VALUES(" + "\'" + accountName + "\'," + "\'" + accountPassword + "\'" + "); ";
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

            }
        }
        else
        {
            Debug.Log("Account is already exist");
        }
    }

    public string CheckAccountExist(string login)
    {
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = dbconnection;
        cmd.CommandText = "SELECT account_id FROM Account WHERE account_name =" + "\"" + login + "\"";
        SqliteDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            return reader[0].ToString();
        }
        return string.Empty;
    }
    
}


