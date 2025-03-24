using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Transform parent;
    [Header("x and y  value : min = 80 -100 ,max = 300")]
    [SerializeField] private int cellSize;
    [Header("Number of words in each row && longest letter count")]
    [SerializeField] private int constraintCount;
    [SerializeField] private int spawnNumber;
    [SerializeField] private GameObject tilePrefab;
    [Header("Lists")]
    [SerializeField] private List<GameObject> spawnedLetters;
    [SerializeField] private string[] letters;
    [SerializeField] private string[] alphabetLetters;
    [SerializeField] List<string> selectedLetters;
    public bool CanSelect;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CanSelect = true;
        Init();
    }
    public void AddNewChar(string ch)
    {
        selectedLetters.Add(ch);
    }
    private void Default()
    {
        grid.constraintCount = constraintCount;

        var gridCell = grid.cellSize;
        gridCell.x = cellSize;
        gridCell.y = cellSize;
        grid.cellSize = gridCell;

        var gridSpacing = grid.spacing;
        gridSpacing.x = cellSize / 4;
        gridSpacing.y = cellSize / 4;
        grid.spacing = gridSpacing;
    }
    private void Init()
    {
        Default();
        StartCoroutine(Spawner());
    }
    private System.Collections.IEnumerator Spawner()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            yield return new WaitForSeconds(0.2f);
            var spawnedObject = Instantiate(tilePrefab, parent);

            spawnedLetters.Add(spawnedObject);

            if (letters[i] == "")
            {
                var randomIndex = Random.Range(0, alphabetLetters.Length);
                spawnedObject.GetComponent<Tile>().tileLetter = alphabetLetters[randomIndex].ToUpper();
                print("Not equal");
            }
            else
                spawnedObject.GetComponent<Tile>().tileLetter = letters[i].ToUpper();
        }
    }
    private void SelectLetter()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // CanSelect = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
           // CanSelect = false;

            selectedLetters.Clear();

            for (int i = 0; i < spawnedLetters.Count; i++)
            {
                spawnedLetters[i].GetComponent<Image>().color = Color.white;
                spawnedLetters[i].GetComponent<Tile>().IsSelected = false;
            }
        }

        if (selectedLetters.Count == constraintCount)
        {
          //  CanSelect = false;
        }
    }    
    private void Update()
    {
        SelectLetter();
    }
}
