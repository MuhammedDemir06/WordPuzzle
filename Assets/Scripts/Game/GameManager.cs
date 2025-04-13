using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Cash Manager")]
    public int CashAmount = 200;
    [SerializeField] private SlideTextEffect slideTextEffect;
    [SerializeField] private TextMeshProUGUI cashText;
    [Header("Level Manager")]
    public int CurrentLevel = 0;
    [SerializeField]private LevelData[] levelsData;
    public int DesiredLevel;
    public bool CanEarnReward;
    [Header("Background")]
    [SerializeField] private SpriteRenderer levelBackground;
    [Header("Sound")]
    public int SoundIndex;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        GetData();
        SetLevelBackground();
        UpdateCash(0, true);
    }
    //can take reward..
    public void Reward()
    {
        levelsData[DesiredLevel].CanEarnReward = false;
    }
    private void GetData()
    {
        CashAmount = PlayerPrefs.GetInt("Cash Amount");
        CurrentLevel = PlayerPrefs.GetInt("Current Level");
        DesiredLevel = PlayerPrefs.GetInt("Desired Level");
        SoundIndex = PlayerPrefs.GetInt("Sound Index");

        GridManager.Instance.GetLevelData(levelsData[DesiredLevel]);
        CanEarnReward = levelsData[DesiredLevel].CanEarnReward;
    }
    private void SetLevelBackground()
    {
        if (levelsData[DesiredLevel].LevelBackground != null)
            levelBackground.sprite = levelsData[DesiredLevel].LevelBackground;
    }
    public void SetData()
    {
        PlayerPrefs.SetInt("Current Level", CurrentLevel);

        PlayerPrefs.SetInt("Cash Amount", CashAmount);

        PlayerPrefs.SetInt("Sound Index", SoundIndex);

        PlayerPrefs.SetInt("Desired Level", DesiredLevel);
    }
    public void UpdateCash(int index,bool isIncrease)
    {
        if (isIncrease)
            CashAmount += index;
        else if (!isIncrease && CashAmount > index)
            CashAmount -= index;
        else
            slideTextEffect.PlaySlideEffect("insufficient cash");

        if (CashAmount <= 0)
            CashAmount = 0;

        cashText.text = CashAmount.ToString();
        SetData();
    }
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Game Delete Current Data")]
    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogWarning(" Deleted All Data");
    }
#endif
}
