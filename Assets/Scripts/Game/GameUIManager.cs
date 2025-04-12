using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [Header("Finish Screen Animation")]
    [SerializeField] private AnimatedPanel finishScreenPanel;
    [Header("Pause Screen Animation")]
    [SerializeField] private AnimatedPanel pauseScreenPanel;
    private bool isOpenPauseScreen; //For pc
    [Header("Sound")]
    [SerializeField] private TextMeshProUGUI soundButtonText;
    private bool IsSound;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpenPauseScreen = !isOpenPauseScreen;
            PauseScreenButton(isOpenPauseScreen);
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
    //Buttons
    public void SoundButton()
    {
        IsSound = !IsSound;
        if (IsSound)
            soundButtonText.text = "SOUND :ON";
        else
            soundButtonText.text = "SOUND :OFF";
    }
    public void PauseScreenButton(bool isOpen)
    {
        if (isOpen)
            pauseScreenPanel.Show();
        else
            pauseScreenPanel.Hide();
    }
    //public void ActiveNewWordButton()
    //{
    //    if (!GameManager.Instance.UpdateCash(100))
    //        return;

    //    GridManager.Instance.NewWordActive();
    //}
    public void NextLevelButton()
    {
        GameManager.Instance.CurrentLevel += 1;
        GameManager.Instance.SaveLevelData();
    }
}
