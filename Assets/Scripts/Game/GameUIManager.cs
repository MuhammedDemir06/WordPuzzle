using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Finish Screen Animation")]
    [SerializeField] private AnimatedPanel finishScreenPanel;
    [SerializeField] private TextMeshProUGUI finishRewardText;
    [SerializeField] private Image[] finishScreenStarsImage;
    private int rewardAmount;
    [Header("Pause Screen Animation")]
    [SerializeField] private AnimatedPanel pauseScreenPanel;
    private bool isOpenPauseScreen; //For pc
    [Header("Sound")]
    [SerializeField] private TextMeshProUGUI soundButtonText;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioSource deniedClickSound;
    private bool isSoundOpen;
    private void OnEnable()
    {
        GridManager.LevelFinish += LevelFinish;
    }
    private void OnDisable()
    {
        GridManager.LevelFinish -= LevelFinish;
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        GetSound();
        UpdateSound();
    }
    private void GetSound()
    {
        if (GameManager.Instance.SoundIndex == 0)
        {
            isSoundOpen = true;
            soundManager.SoundsActive(true);
        }      
        else
        {
            isSoundOpen = false;
            soundManager.SoundsActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpenPauseScreen = !isOpenPauseScreen;
            PauseScreenButton(isOpenPauseScreen);
        }
    }
    private void LevelFinish(int numberOfIncorrectWords)
    {
        if (numberOfIncorrectWords <= 1)
        {
            FinishScreenStars(2);
            rewardAmount = 1000;
        }
        else if (numberOfIncorrectWords == 2)
        {
            FinishScreenStars(1);
            rewardAmount = 500;
        }           
        else
        {
            FinishScreenStars(0);
            rewardAmount = 100;
        }

        NextCurrentLevel();

        if (GameManager.Instance.CanEarnReward)
            GameManager.Instance.UpdateCash(rewardAmount, true);
        else
            rewardAmount = 0;

        finishRewardText.text = rewardAmount.ToString();
        GameManager.Instance.Reward();
        
    }
    private void FinishScreenStars(int index)
    {
        for (int i = 0; i <= index; i++)
        {
            finishScreenStarsImage[i].gameObject.SetActive(true);   
        }
    }
    public void FinishScreenShow()
    {
        finishScreenPanel.Show();
    }
    public void FinishScreenHide()
    {
        finishScreenPanel.Hide();
    }

    public void UpdateSound()
    {
        if (isSoundOpen)
        {
            soundButtonText.text = "SOUND :ON";
            GameManager.Instance.SoundIndex = 0;
        }
        else
        {
            soundButtonText.text = "SOUND :OFF";
            GameManager.Instance.SoundIndex = 1;
        }
    }
    private void NextCurrentLevel()
    {
        if (GameManager.Instance.CurrentLevel <= GameManager.Instance.DesiredLevel)
            GameManager.Instance.CurrentLevel += 1;
    }
    //Buttons
    public void SoundButton()
    {
        isSoundOpen = !isSoundOpen;
        UpdateSound();
        GameManager.Instance.SetData();
    }
    public void PauseScreenButton(bool isOpen)
    {
        if (isOpen)
            pauseScreenPanel.Show();
        else
            pauseScreenPanel.Hide();
    }
    public void ActiveNewWordButton(int index)
    {
        if (GameManager.Instance.CashAmount > index)
            GridManager.Instance.NewWordActive();
        else
        {
            if (deniedClickSound != null)
                deniedClickSound.Play();
        }
        //
        GameManager.Instance.UpdateCash(index, false); 
    }
    public void NextLevelButton()
    {
        NextCurrentLevel();

        GameManager.Instance.DesiredLevel += 1;
        GameManager.Instance.SetData();
    }
}
