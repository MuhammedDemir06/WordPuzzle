using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    //Buttons
    public void ActiveNewWordButton()
    {
        if (!GameManager.Instance.UpdateCash(100))
            return;

        GridManager.Instance.NewWordActive();
    }
}
