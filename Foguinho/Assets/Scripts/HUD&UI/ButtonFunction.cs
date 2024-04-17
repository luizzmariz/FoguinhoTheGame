using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonFunction : MonoBehaviour
{
    // private GameManager gameManager;
    // public string buttonFunction;
    public Button button;
    public TMP_Text buttonText;
    public Color highlightColor;
    public Color normalColor;


    // Start is called before the first frame update
    void Start()
    {
        // if(gameManager == null)
        // {
        //     gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // }

        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        normalColor = buttonText.color;
    }

    // public void SendButtonFunction() {
    //     gameManager.ButtonFunction(buttonFunction);
    // }

    public void SetHighlightText()
    {
        buttonText.color = highlightColor;
    }

    public void SetNormalText()
    {
        buttonText.color = normalColor;
    }
}

