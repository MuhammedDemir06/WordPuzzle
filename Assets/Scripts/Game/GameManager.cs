using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Cash Manager")]
    public int CashAmount = 200;
    [SerializeField] private TextMeshProUGUI cashText;
    [Header("Level Manager")]
    public int CurrentLevel = 0;
    [SerializeField]private LevelData[] levelsData;

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
      //  UpdateCash(0);
        GetLevelData();
    }
    private void GetLevelData()
    {
        CashAmount = PlayerPrefs.GetInt("Cash Amount");
        CurrentLevel = PlayerPrefs.GetInt("Current Level");
        GridManager.Instance.GetLevelData(levelsData[CurrentLevel]);
    }
    public void SaveLevelData()
    {
        PlayerPrefs.SetInt("Current Level", CurrentLevel);
        PlayerPrefs.SetInt("Cash Amount", CashAmount);
    }
    //public bool UpdateCash(int amount)
    //{
    //    if (CashAmount < amount)
    //    {
    //        Debug.Log("Not Enough Cash!!");
    //        return false;
    //    }

    //    CashAmount -= amount;

    //    if (CashAmount <= 0)
    //        CashAmount = 0;

    //    cashText.text = CashAmount.ToString();
    //    return true;
    //}
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Game Delete Current Data")]
    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        Debug.LogWarning(" Deleted All Data");
    }
#endif
}
