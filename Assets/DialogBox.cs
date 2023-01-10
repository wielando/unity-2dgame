using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{

    TextMeshProUGUI _textMesh;


    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        _textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void PrintTextToDialogBox(string sentence)
    {
        _textMesh.text = sentence;
    }

    public void PrintButtonsToDialogBox(bool exploreButton = false, bool itemButton = false)
    {

    }

    public void SetRenderStatus(bool renderStatus, string type)
    {
        if(type == "SimpleDialogBox")
        {
            GameObject.FindGameObjectWithTag(type).GetComponent<Canvas>().enabled = renderStatus;
            return;
        }

        if (type == "ChoiceDialogBox")
        {
            GameObject.FindGameObjectWithTag(type).GetComponent<Canvas>().enabled = renderStatus;
            return;
        }
    }

}
