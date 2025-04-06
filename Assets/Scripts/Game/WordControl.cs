using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordControl : MonoBehaviour
{
    public string Word;
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private Animator selectedAnim;
    public bool Selected;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        Word = wordText.text;
    }
    public void Select()
    {
        selectedAnim.SetTrigger("Select");
        Debug.Log(Word + " this word selected");
    }
}
