using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonFunction : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;
    public Color highlightColor;
    public Color normalColor;


    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        normalColor = buttonText.color;
    }

    public void SetHighlightText()
    {
        if(button.interactable)
        {
            buttonText.color = highlightColor;
        }
    }

    public void SetNormalText()
    {
        buttonText.color = normalColor;
    }
}

