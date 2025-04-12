using System.Collections.Generic;
using UnityEngine;
public class LevelData : ScriptableObject
{
    [Header("x and y  value : min = 80 -100 ,max = 300")]
    public int CellSize;
    [Header("Number of words in each row && longest letter count")]
    public int ConstraintCount;
    public int SpawnNumber;
    [Header("Letter")]
    public List<string> Letters;
    [Header("Words")]
    public List<string> Words;
}

