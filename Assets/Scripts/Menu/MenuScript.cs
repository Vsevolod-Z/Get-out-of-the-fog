using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class MenuScript : MonoBehaviour
{
    [Header("DataBase")]
    [SerializeField]
    MenuDataBase dataBase;
    [Header("LoginPanel")]
    [SerializeField]
    private GameObject loginPanel;
    [SerializeField]
    private TMP_InputField loginText;
    [SerializeField]
    private TMP_InputField passwordText;
    [SerializeField]
    private string[] userData = new string[2];
    [Header("CharacterSelectPanel")]
    [SerializeField]
    GameObject characterPanel;
    [SerializeField]
    GameObject[] charactersIcons;
    [Header("Settings Panel")]
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private Slider globalVolumeSlider;
    [SerializeField]
    private Slider MusicVolumeSlider;
    [SerializeField]
    private Slider EffectsVolumeSlider;
    [SerializeField]
    private TMP_Dropdown graphicDropdown;



    [Header("Load Scene")]
    [SerializeField]
    private GameObject loadBackground;
    [SerializeField]
    private Image[] loadBar = new Image[3];
    [SerializeField]
    private TMP_Text loadText;
    AsyncOperation asyncOperation;

    public void OpenCloseLoginPanel()
    {
        settingsPanel.SetActive(false);
        loginPanel.SetActive((loginPanel.activeInHierarchy) ? false : true);
        if (!loginPanel.activeInHierarchy)
        {
            passwordText.text = string.Empty;
            loginText.text = string.Empty;
        }
    }
    private void OpenCharacterSelectMenu()
    {
        loginPanel.SetActive(false);
        loginText.text = string.Empty;
        passwordText.text = string.Empty;
        characterPanel.SetActive(true);
    }
    public void CloseCharacterPanel()
    {
        TMP_Text[] characterTexts;
        characterPanel.SetActive(false);
        foreach (GameObject characterIcon in charactersIcons)
        {
            characterIcon.transform.GetChild(0).GetChild(3).gameObject.SetActive(false );
            characterTexts = characterIcon.transform.GetChild(0).GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text text in characterTexts)
            {
                text.text = string.Empty;
            }
        }
    }
    public void OpenCloseSettingsPanel()
    {
        settingsPanel.SetActive((settingsPanel.activeInHierarchy) ? false : true);
    }
    public void LoginAttempt()
    {
        bool loginComplete = false;
        loginComplete = dataBase.TryLoginAttempt(loginText.text, passwordText.text);
        if (loginComplete)
        {
            userData = dataBase.GetAccountData(loginText.text);
        
            PlayerPrefs.SetString("playerID", userData[0]);
            PlayerPrefs.SetString("playerPrivilegess", userData[1]);
            OpenCharacterSelectMenu();
            dataBase.GetCharactersData(userData[0], charactersIcons);
            foreach (GameObject characterIcon in charactersIcons)
            {
                if (characterIcon.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>().text == "empty" || characterIcon.transform.GetChild(0).GetChild(3).GetComponent<TMP_Text>().text == string.Empty)
                {
                    characterIcon.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = string.Empty;
                    characterIcon.GetComponent<Button>().enabled = false;
                    characterIcon.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("Incorrect account data");
        }
    }
    public void CreateNewCharacter(TMP_InputField nameInputField)
    {
        dataBase.AddNewChatacter(nameInputField.text, userData[0]);
        nameInputField.transform.parent.gameObject.SetActive(false);
        nameInputField.transform.parent.parent.parent.GetComponent<Button>().enabled = true;
        dataBase.GetCharactersData(userData[0], charactersIcons);
    }
    public void CreateNewAccount()
    {
        dataBase.CreateNewAccount(loginText.text, passwordText.text);
        LoginAttempt();
    }
    public void LoadGameScene(TMP_Text characterID)
    {
        PlayerPrefs.SetInt("characterID", Convert.ToInt32(characterID.text));
        StartCoroutine(LoadGameScene());
    }
    IEnumerator LoadGameScene()
    {

        loadBackground.SetActive(true);
        asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        while (asyncOperation.progress != 0.9f)
        {
            float progress = asyncOperation.progress / 0.9f;
            foreach(Image image in loadBar)
            {
                image.fillAmount = progress;
            }
            loadText.text = "Loading...   " + string.Format("{0:0}%", progress * 100f);
            yield return new WaitForSeconds(0.01f);
        }
        foreach (Image image in loadBar)
        {
            image.fillAmount = 1;
        }

        asyncOperation.allowSceneActivation = true;
    }
    public void checkdropdown()
    {
        QualitySettings.SetQualityLevel(graphicDropdown.value, true);//Изменяем уровен графики
    }
    public void Exit()
    {
        Application.Quit();
    }
}
