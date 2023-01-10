using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    private Queue<string> _sentences;
    private string[] _itemSentences;
    private string[] _exploreSentences;

    private DialogBox _dialogBox;

    private bool _dialogState = false;
    private bool _dialogChoiceState = false;
    private bool _collectableNow = false;

    private string _dialogTypeBox;
    private string _dialogType;

    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
        _dialogBox = gameObject.GetComponentInChildren<DialogBox>();
    }

    void Update()
    {
        
        if(_dialogChoiceState)
        {
            GameTime.SetIsPaused(true);

            if (Input.GetKeyDown(KeyCode.A))
            {
                StartExploreDialoge(_exploreSentences);
                _dialogBox.SetRenderStatus(false, "ChoiceDialogBox");
                _dialogChoiceState = false;
            }

            if(Input.GetKeyDown(KeyCode.B))
            {
                StartItemDialog(_itemSentences);
                _dialogBox.SetRenderStatus(false, "ChoiceDialogBox");
                _dialogChoiceState = false;
            }

            return;
        }

        if (!_dialogState) 
        {
            _dialogBox.SetRenderStatus(false, _dialogTypeBox);

            if (GameTime.GetIsPaused())
                GameTime.SetIsPaused(false);

            return;
        }

        GameTime.SetIsPaused(true);


        if (Input.GetKeyDown(KeyCode.A))
            DisplayNextSentence();
       
    }

    public void OpenDialogBox(string[] text)
    {

    }

    public void OpenSelectDialogBox(string[] itemText, string[] exploreText)
    {
        if (_dialogState)
            return;

        _itemSentences = itemText;
        _exploreSentences = exploreText;

        _dialogChoiceState = true;
        _dialogTypeBox = "ChoiceDialogBox";
        _dialogBox.SetRenderStatus(true, _dialogTypeBox);
    }

    public void StartItemDialog(string[] itemText)
    {
        foreach (string sentence in itemText)
        {
            _sentences.Enqueue(sentence);
        }

        _dialogState = true;
        _dialogTypeBox = "SimpleDialogBox";
        _dialogType = "CollectingItem";
        _dialogBox.SetRenderStatus(true, _dialogTypeBox);
        DisplayNextSentence();
    }

    public void StartExploreDialoge(string[] exploreText)
    {
        if (_dialogState)
            return;

        foreach (string sentence in exploreText)
        {
           _sentences.Enqueue(sentence);
        }

        _dialogState = true;
        _dialogTypeBox = "SimpleDialogBox";
        _dialogType = "Explore";
        _dialogBox.SetRenderStatus(true, _dialogTypeBox);
        DisplayNextSentence();
    } 

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentences.Dequeue();

        _dialogBox.PrintTextToDialogBox(sentence);
    }

    private void EndDialogue()
    {
        _dialogState = false;

        if (_dialogType == "CollectingItem")
            _collectableNow = true;

        Debug.Log("Dialog end");
    }

    public bool GetDialogState()
    {
        return _dialogState;
    }

    public string GetDialogType()
    {
        return _dialogType;
    }

    public bool GetCollectableNow()
    {
        return _collectableNow;
    }

    public void SetCollectableNow(bool status)
    {
        _collectableNow = status;
    }

    public void SetDialogType(string type)
    {
        _dialogType = type;
    }
}
