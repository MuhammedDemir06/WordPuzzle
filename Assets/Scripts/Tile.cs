using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public string tileLetter;
    [SerializeField] private TextMeshProUGUI tileText;
    [SerializeField] private Color selectedColor;
    private Image tileImage;
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        tileImage = GetComponent<Image>();

        tileText.text = tileLetter.ToString();
    }
    public void OnPointerEnter()
    {
        print(tileLetter);
        tileImage.color = selectedColor;
    }
}
