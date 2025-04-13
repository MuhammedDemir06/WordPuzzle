using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public string tileLetter;
    [SerializeField] private TextMeshProUGUI tileText;
    [SerializeField] private Color selectColor,unSelectedColor;
    public Color SelectedColor;
    public bool IsSelected;
    public bool ColorChange;
    public bool RandomLetter;
    public bool IsActive;
    private Image tileImage;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        tileImage = GetComponent<Image>();

        tileText.text = tileLetter.ToString();

        ColorChange = true;
    }
    public void SetColor(bool isSelect)
    {
        if (ColorChange)
        {
            if (isSelect)
                tileImage.color = selectColor;
            else
                tileImage.color = unSelectedColor;
        }
        else
            tileImage.color = SelectedColor;
    }
    public void OnPointerEnter()
    {
        if (GridManager.Instance.CanSelect && GridManager.Instance.IsPointerDown)
        {
            TrySelect();
        }
    }

    public void OnPointerDown()
    {
        GridManager.Instance.IsPointerDown = true;

        if (GridManager.Instance.CanSelect)
        {
            TrySelect();
        }
    }
    public void OnPointerUp()
    {
        GridManager.Instance.IsPointerDown = false;
        GridManager.Instance.EndSelection();
    }
    private void TrySelect()
    {
        if (!IsSelected)
        {
            SetColor(true);
            GridManager.Instance.AddNewChar(tileLetter.ToUpper(), this);
            IsSelected = true;
        }
    }
}
