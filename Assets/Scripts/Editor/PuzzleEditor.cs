using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PuzzleEditor : EditorWindow
{
   // [Header("value : min = 80 -100 ,max = 300")]
    private int cellSize = 50;
    private int gridX = 3;

    private int gridY = 3;
    private int currentLevel = 0;
    private List<string> words;
    private List<string> letters;
    private List<int> selectedIndices = new List<int>();
    private string inputWord = "";
    private Sprite LevelBackground;

    private Vector2 scrollPos;

    [MenuItem("Tools/Puzzle Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<PuzzleEditor>("Puzzle Level Editor");
    }
    private void OnGUI()
    {
        GUILayout.Space(15);
        GUILayout.Label("Level Creator", EditorStyles.boldLabel);
        GUILayout.Label("Notice: the number you write is multiplied by 4 for cell size");
        cellSize = EditorGUILayout.IntField("Cell Size ", cellSize);
        gridX = EditorGUILayout.IntField("grid X", gridX);
        gridY = EditorGUILayout.IntField("grid Y", gridY);
        currentLevel = EditorGUILayout.IntField("Current Level", currentLevel);
        LevelBackground = (Sprite)EditorGUILayout.ObjectField("Sprite Icon", LevelBackground, typeof(Sprite), false);

        int totalCells = gridX * gridY;
        while (letters.Count < totalCells) letters.Add("");
        while (letters.Count > totalCells) letters.RemoveAt(letters.Count - 1);

        GUILayout.Space(20);

        CreateGrid();

        GUILayout.Label("Write a word to insert into selected grid cells:");
        inputWord = EditorGUILayout.TextField(inputWord);

        //Buttons
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Word", GUILayout.Width(100)))
        {
            AddWord();
        }

        if (GUILayout.Button("Reset All", GUILayout.Width(100)))
        {
            ResetAll();
        }
        if(GUILayout.Button("How Create Level ?",GUILayout.Width(150)))
        {
            Application.OpenURL("https://www.youtube.com/watch?v=uC1YI-QppBM");
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add New Level", GUILayout.Width(100), GUILayout.Height(50)))
        {
            AddNewLevel();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(30);
    }
    private void AddWord()
    {
        if (inputWord.Length > 1)
        {
            FillSelectedCells(inputWord);
            inputWord = "";
            selectedIndices.Clear();
            GUI.FocusControl(null);
            GUI.changed = true;
        }
        else
            Debug.LogError("Please fill in the empty field");
    }
    private void AddNewLevel()
    {
        if (words.Count > 1)
        {
            currentLevel += 1;
            CreateNewLevel();
        }
        else
            Debug.LogError("Please Add some words");
    }
    private void ResetAll()
    {
        for (int i = 0; i < letters.Count; i++)
            letters[i] = "";

        selectedIndices.Clear();
        words.Clear();
        inputWord = "";
        GUI.FocusControl(null);
        GUI.changed = true;
        Debug.Log("Everything is reset");
    }
    private void CreateGrid()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        float totalGridWidth = gridX * cellSize;
        float panelWidth = EditorGUIUtility.currentViewWidth;
        float startX = (panelWidth - totalGridWidth) / 2f;
        if (startX < 0) startX = 0;

        Event e = Event.current;
        Rect lastRect = new Rect();

        for (int y = 0; y < gridY; y++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(startX);

            for (int x = 0; x < gridX; x++)
            {
                int index = y * gridX + x;
                bool isSelected = selectedIndices.Contains(index);

                GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
                btnStyle.alignment = TextAnchor.MiddleCenter;
                btnStyle.normal.textColor = isSelected ? Color.green : Color.white;
                btnStyle.fontStyle = isSelected ? FontStyle.Bold : FontStyle.Normal;

                Color oldColor = GUI.backgroundColor;
                GUI.backgroundColor = isSelected ? Color.yellow : Color.white;

                if (GUILayout.Button(letters[index], btnStyle, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    ToggleSelection(index);
                }

                GUI.backgroundColor = oldColor;

                lastRect = GUILayoutUtility.GetLastRect();

                if (e.type == EventType.MouseDrag || e.type == EventType.MouseDown)
                {
                    if (lastRect.Contains(e.mousePosition))
                    {
                        if (!selectedIndices.Contains(index))
                            selectedIndices.Add(index);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }
    private void ToggleSelection(int index)
    {
        if (selectedIndices.Contains(index))
            selectedIndices.Remove(index);
        else
            selectedIndices.Add(index);
    }
    private void FillSelectedCells(string word)
    {
        int wordLength = word.Length;
        int selectedCount = selectedIndices.Count;

        int minLength = Mathf.Min(wordLength, selectedCount);

        if (wordLength == selectedCount)
            words.Add(word.ToUpper());

        for (int i = 0; i < minLength; i++)
        {
            int index = selectedIndices[i];
            letters[index] = word[i].ToString().ToUpper();
        }

        for (int i = minLength; i < selectedCount; i++)
        {
            int index = selectedIndices[i];
            letters[index] = ""; 
        }
    }
    private void CreateNewLevel()
    {
        LevelData newLevel = CreateInstance<LevelData>();

        SaveData(newLevel);

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newLevel;

        Debug.Log("New Level Created");
    }
    private void SaveData(LevelData level)
    {
        level.SpawnNumber = letters.Count;
        level.CellSize = cellSize * 4;
        level.ConstraintCount = gridX;
        level.Letters = new List<string>(letters);
        level.Words = new List<string>(words);
        level.LevelBackground = LevelBackground;

        string path = "Assets/Levels/Level_" + $"{currentLevel}" + ".asset";
        AssetDatabase.CreateAsset(level, path);
        AssetDatabase.SaveAssets();
    }
}