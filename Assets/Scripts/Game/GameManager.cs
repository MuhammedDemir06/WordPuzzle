using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int CashAmount = 200;
    [SerializeField] private TextMeshProUGUI cashText;
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
        UpdateCash(0);
    }
    public bool UpdateCash(int amount)
    {
        if (CashAmount < amount)
        {
            Debug.Log("Not Enough Cash!!");
            return false;
        }

        CashAmount -= amount;

        if (CashAmount <= 0)
            CashAmount = 0;

        cashText.text = CashAmount.ToString();
        return true;
    }
}
