using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyQuest : MonoBehaviour
{

    private int _questId = 1;

    public GameObject[] _questItems;

    public GameObject _questTrigger;
    public bool _returnToQuestTrigger;

    private float _enterRadius = 0.32f;
    private Vector3 _questTriggerRadius;

    public GameObject _player;
    private PlayerActions _playerActions;

    public GameObject _followQuest;

    // Start is called before the first frame update
    void Start()
    {
    
        // Prevent Quest start for this rule when there is no Items that could fullfile the quest
        if(_questItems == null)
            return;

        // If there is no player object given try to get one with Tag "Player"
        // if there is still no player found end the quest
        if (_player == null)
        {
            _player = (GameObject.FindGameObjectWithTag("Player") != null) ? _player = GameObject.FindGameObjectWithTag("Player") : null;

            if (_player == null)
                return;
        }

        _playerActions = _player.GetComponent<PlayerActions>();
        _questTriggerRadius = _questTrigger.GetComponent<BoxCollider2D>().transform.position;

    }

    private void Update()
    {

        // Check if Player is near the questItem
        // If so triggering the event with Key "K" is possible
        if (Vector3.Distance(_player.transform.position, _questTriggerRadius) <= _enterRadius)
        {

            if (Input.GetKeyDown(KeyCode.K))
            {

                Debug.Log("Key K is pressed");

                if(IsQuestAlreadySolved()) {
                    return;
                }

                if (IsQuestAlreadyAccepted())
                {
                    if(EndQuest())
                    {
                        Debug.Log("Quest successfull. TODO: Dialog!");
                        return; 
                    }
                } else
                {
                    StartQuest();
                }

            }
                
        }
    }

    private void StartQuest()
    {
        if (!IsQuestAlreadySolved() && IsQuestAlreadyAccepted())
            return;

        _playerActions.InsertNewQuest(_questId);
    }

    private bool EndQuest()
    {
        if (!IsQuestAlreadySolved() && !IsQuestAlreadyAccepted())
            return false;

        if (!CheckPlayerInventoryForQuestItem())
            return false;

        if(_questItems.Length > 1)
        {
            foreach(GameObject questItem in _questItems)
            {
                _playerActions.GetInventory().DestroyItemInInventory(questItem);
            }
        } else
        {
            _playerActions.GetInventory().DestroyItemInInventory(_questItems[0]);
        }

        this.SetQuestSolved();
        this.RemoveQuestInAccepted();

        if(_followQuest != null)
        {
            _followQuest.SetActive(true);
        }

        return true;
    }

    private void SetQuestSolved()
    {
        _playerActions.AddSolvedQuestInsert(_questId);
    }

    private void RemoveQuestInAccepted()
    {
        _playerActions.RemoveAcceptedQuestInsert(_questId);
    }

    private bool IsQuestAlreadySolved()
    {
        foreach(int questId in _playerActions.GetSolvedQuests())
        {
            if(questId == _questId)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsQuestAlreadyAccepted()
    {
        foreach (int questId in _playerActions.GetAcceptedQuests())
        {
            if (questId == _questId)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckPlayerInventoryForQuestItem()
    {

        
        return false;
    }
}
