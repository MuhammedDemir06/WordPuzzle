﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static System.Action<int> LevelFinish;
    public static GridManager Instance;

    [SerializeField] private GridLayoutGroup letterGrid;
    [SerializeField] private Transform wordParent;
    [SerializeField] private float spawningTime = 0.2f;
    [Header("x and y  value : min = 80 -100 ,max = 300")]
    private int cellSize;
    [Header("Number of words in each row && longest letter count")]
    private int constraintCount;
    private int spawnNumber;
    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject wordPrefab;
    [Header("Lists")]
    [SerializeField] private List<GameObject> spawnedLetters;
    [SerializeField] private List<string> letters;
    [SerializeField] private string[] alphabetLetters;

    public bool CanSelect;
    [Header("Selected Letters")]
    [SerializeField] private GameObject writtenWordImage;
    [SerializeField] private TextMeshProUGUI writtenWord;
    [SerializeField] private List<string> selectedLetters;
    [SerializeField] private List<Tile> selectedLettersPrefab;
    private bool isClick;
    private bool isEqual;
    [Header("Word")]
    [SerializeField] private List<WordControl> currentWords;
    [SerializeField] private int wordIndex;
    [SerializeField] private List<string> words;
    public bool IsPointerDown;
    [Header("Effects")]
    [SerializeField] private SlideTextEffect slideTextEffect;
    [SerializeField] private PopupTextEffect popupTextEffect;
    [SerializeField] private string[] levelFinishText;
    [SerializeField] private string[] levelWordText;
    [Header("UI")]
    [SerializeField] private GameUIManager uIManager;
    [SerializeField] private float finishScreenTime = 1f;
    [SerializeField] private AudioSource chooseWordSound;
    [SerializeField] private AudioSource letterSelectSound;

    public int numberOfIncorretWords;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Init();
    }
    public void AddNewChar(string letter,Tile selectedLetterPrefab)
    {
        letterSelectSound.Play();

        selectedLettersPrefab.Add(selectedLetterPrefab);
        selectedLetters.Add(letter);

        writtenWord.text += letter.ToUpper();
        isClick = true;
    }
    private void Init()
    {
        numberOfIncorretWords = 0;
        CanSelect = true;

        Default();
        StartCoroutine(TileSpawner());
        StartCoroutine(WordSpawner());
    }
    public void GetLevelData(LevelData levelData)
    {
        cellSize = levelData.CellSize;
        constraintCount = levelData.ConstraintCount;
        spawnNumber = levelData.SpawnNumber;
        letters = levelData.Letters;
        words = levelData.Words;
    }
    private void Default()
    {
        letterGrid.constraintCount = constraintCount;

        var gridCell = letterGrid.cellSize;
        gridCell.x = cellSize;
        gridCell.y = cellSize;
        letterGrid.cellSize = gridCell;

        var gridSpacing = letterGrid.spacing;
        gridSpacing.x = cellSize / 4;
        gridSpacing.y = cellSize / 4;
        letterGrid.spacing = gridSpacing;    
    }
    
    private System.Collections.IEnumerator TileSpawner()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            yield return new WaitForSeconds(spawningTime);
            var newTile = Instantiate(tilePrefab, letterGrid.gameObject.transform);

            spawnedLetters.Add(newTile);

            if (letters[i] == "")
            {
                var randomIndex = Random.Range(0, alphabetLetters.Length);
                newTile.GetComponent<Tile>().tileLetter = alphabetLetters[randomIndex].ToUpper();
                newTile.GetComponent<Tile>().RandomLetter = true;
                print("Not equal");
            }
            else
                newTile.GetComponent<Tile>().tileLetter = letters[i].ToUpper();
        }
    }
    private System.Collections.IEnumerator WordSpawner()
    {
        for (int i = 0; i < words.Count; i++)
        {
            yield return new WaitForSeconds(spawningTime);

            var newWord = Instantiate(wordPrefab, wordParent);
            newWord.GetComponentInChildren<TextMeshProUGUI>().text = words[i];
            currentWords.Add(newWord.GetComponent<WordControl>());
        }
    } 
    public void EndSelection()
    {
        if (selectedLetters.Count > 0)
        {
            Word();

            CanSelect = true;
            selectedLetters.Clear();
            writtenWord.text = "";
            isClick = false;

            if (isEqual)
            {
                for (int i = 0; i < selectedLettersPrefab.Count; i++)
                {
                    selectedLettersPrefab[i].IsSelected = false;
                    selectedLettersPrefab[i].ColorChange = false;
                    selectedLettersPrefab[i].SetColor(false);
                }
                int newIndex = Random.Range(0, levelWordText.Length);
                slideTextEffect.PlaySlideEffect(levelWordText[newIndex]);
                
                Debug.Log("Equal");
                if (chooseWordSound != null)
                    chooseWordSound.Play();
            }
            else
            {
                for (int i = 0; i < spawnedLetters.Count; i++)
                {
                    spawnedLetters[i].GetComponent<Tile>().SetColor(false);
                    spawnedLetters[i].GetComponent<Tile>().IsSelected = false;
                    spawnedLetters[i].GetComponent<Tile>().IsActive = false;
                }
                numberOfIncorretWords += 1;
            }
            selectedLettersPrefab.Clear();
            
        }
    }
    private void Word()
    {
        for (int i = 0; i < currentWords.Count; i++)
        {
            if (writtenWord.text == currentWords[i].Word && !currentWords[i].Selected)
            {
                currentWords[i].Select();
                currentWords[i].Selected = true;
                wordIndex += 1;
                isEqual = true;
                break;
            }
            else
                isEqual = false;
                
        }
        if (wordIndex == currentWords.Count)
        {
            Invoke(nameof(RunFinishScreen), finishScreenTime);
            Debug.Log("Level Finished");
            //Finished Level
            LevelFinish?.Invoke(numberOfIncorretWords);
            //Random Effect
            int newIndex = Random.Range(0, levelFinishText.Length);
            popupTextEffect.PlayPopupEffectForText(levelFinishText[newIndex]);
        }
    }
    private void RunFinishScreen()
    {
        uIManager.FinishScreenShow();
    }
    private void SelectLetter()
    {
        writtenWordImage.SetActive(isClick);
        
        if (selectedLetters.Count == constraintCount)
        {
            CanSelect = false;
        }
    }
    public void NewWordActive()
    {
        for (int i = 0; i < spawnedLetters.Count; i++)
        {
            if (spawnedLetters[i].GetComponent<Tile>().ColorChange && !spawnedLetters[i].GetComponent<Tile>().RandomLetter && !spawnedLetters[i].GetComponent<Tile>().IsActive)
            {
                spawnedLetters[i].GetComponent<Image>().color = Color.gray;
                spawnedLetters[i].GetComponent<Tile>().IsActive = true;
                break; 
            }
        }
    }
    private void Update()
    {
        SelectLetter();
    }
}
