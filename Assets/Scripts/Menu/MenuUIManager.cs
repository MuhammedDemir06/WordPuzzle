using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [Header("Levels Screen Anim")]
    [SerializeField] private AnimatedPanel levelsScreen;
    [Header("Level Manager")]
    [SerializeField] private Transform levelsContent;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private LevelData[] levelsData;
    [SerializeField] private int currentLevel;
    public int DesiredLevel;
    [Header("Scene Transition")]
    [SerializeField] private SceneTransitionEffect sceneTransition;
    [Header("Cash Manager")]
    [SerializeField] private SlideTextEffect slideCashEffect;
    [SerializeField] private TextMeshProUGUI cashText;
    public int CashAmount = 0;
    [Header("Sound Manager")]
    public bool isSoundOpen;
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioSource clickSoundLevels;
    private int soundIndex;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        GetData();
        LevelSpawner();
        UpdateCash();
        UpdateSound();
    }
    private void GetData()
    {
        currentLevel = PlayerPrefs.GetInt("Current Level");
        CashAmount = PlayerPrefs.GetInt("Cash Amount");

        soundIndex = PlayerPrefs.GetInt("Sound Index");
        if (soundIndex == 0)
            isSoundOpen = true;
        else
            isSoundOpen = false;
    }
    private void SetData()
    {
        PlayerPrefs.SetInt("Cash Amount", CashAmount);

        PlayerPrefs.SetInt("Desired Level", DesiredLevel);

        PlayerPrefs.SetInt("Sound Index", soundIndex);
    }
    public void UpdateCash()
    {
        cashText.text = CashAmount.ToString();
    }
    public void UpdateSound()
    {
        if (isSoundOpen)
        {
            soundButtonText.text = "SOUND :ON";
            soundManager.SoundsActive(true);
            soundIndex = 0;
        }
        else
        {
            soundButtonText.text = "SOUND :OFF";
            soundManager.SoundsActive(false);
            soundIndex = 1;
        }
    }
    private void LevelSpawner()
    {
        for (int i = 0; i < levelsData.Length; i++)
        {
            var spawnedButton = Instantiate(levelButtonPrefab, levelsContent);
            spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{i + 1}";

            int levelIndex = i;
            spawnedButton.GetComponent<Button>().onClick.AddListener(() => OpenLevelButton(levelIndex));
            if (i==0)
            {           
                Transform lockImg = spawnedButton.transform.Find("Lock Image");
                if (lockImg != null)
                    lockImg.gameObject.SetActive(false);
            }
            else if (i <= currentLevel)
            {    
                Transform lockImg = spawnedButton.transform.Find("Lock Image");
                if (lockImg != null)
                    lockImg.gameObject.SetActive(false);
            }
            else
                spawnedButton.GetComponent<Button>().interactable = false;
        }
    }
    //Buttons
    private void OpenLevelButton(int index)
    {
        soundManager.ClickSoundButton(clickSoundLevels);
        sceneTransition.LoadScene("Game");
        DesiredLevel = index;
        SetData();
    }
    public void SocialMediaButton(string link)
    {
        Application.OpenURL(link);
    }
    public void SoundButton()
    {
        isSoundOpen = !isSoundOpen;

        UpdateSound();
        SetData();
    }
    public void GiftButton(int index)
    {
        CashAmount += index;
        slideCashEffect.PlaySlideEffect($"{index}+");
        UpdateCash();
        SetData();
    }
    public void PlayButton()
    {
        levelsScreen.Show();
    }
    public void LevelsBackButton()
    {
        levelsScreen.Hide();
    }
}
