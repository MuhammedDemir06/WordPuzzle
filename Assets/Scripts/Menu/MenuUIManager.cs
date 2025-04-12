using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    [Header("Scene Transition")]
    [SerializeField] private SceneTransitionEffect sceneTransition;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        GetData();
        LevelSpawner();
    }
    private void GetData()
    {
        currentLevel = PlayerPrefs.GetInt("Current Level");
    }
    private void LevelSpawner()
    {
        for (int i = 0; i < levelsData.Length; i++)
        {
            var spawnedButton = Instantiate(levelButtonPrefab, levelsContent);
            spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{i + 1}";
            if (i==0)
            {
                spawnedButton.GetComponent<Button>().onClick.AddListener(OpenLevelButton);
                Transform lockImg = spawnedButton.transform.Find("Lock Image");
                if (lockImg != null)
                    lockImg.gameObject.SetActive(false);
            }
            else if (i <= currentLevel)
            {    
                spawnedButton.GetComponent<Button>().onClick.AddListener(OpenLevelButton);

                Transform lockImg = spawnedButton.transform.Find("Lock Image");
                if (lockImg != null)
                    lockImg.gameObject.SetActive(false);
            }
            else
                spawnedButton.GetComponent<Button>().interactable = false;
        }
    }
    //Buttons
    private void OpenLevelButton()
    {
        sceneTransition.LoadScene("Game");
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
