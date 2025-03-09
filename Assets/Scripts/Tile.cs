using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer rendererColor;
    public void Init(bool isOffset)
    {
     //   rendererColor.color = isOffset ? baseColor : offsetColor;
    }
}
