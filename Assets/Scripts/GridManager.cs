using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Transform parent;
   // [Header("Cellsize/4")]
   // [SerializeField] private float spacingX, spacingY;
    [Header("x and y  value : min = 80 -100 ,max = 300")]
    [SerializeField] private float cellSizeX, cellSizeY;
    [Header("Number of words in each row")]
    [SerializeField] private int constraintCount;
    [SerializeField] private int spawnNumber;
    [SerializeField] private GameObject tilePrefab;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        grid.constraintCount = constraintCount;

        var gridCell = grid.cellSize;
        gridCell.x = cellSizeX;
        gridCell.y = cellSizeY;
        grid.cellSize = gridCell;

         //spacingX = ;
       //  spacingY = ;

        var gridSpacing = grid.spacing;
        gridSpacing.x = cellSizeX / 4;
        gridSpacing.y = cellSizeY / 4;
        grid.spacing = gridSpacing;

        StartCoroutine(Spawner());
    }
    private IEnumerator Spawner()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            yield return new WaitForSeconds(0.2f);
            var spawnedObject = Instantiate(tilePrefab, parent);
        }
    }
}
