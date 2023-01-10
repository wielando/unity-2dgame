using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private ArrayList _acceptedQuests = new ArrayList();
    private ArrayList _solvedQuests = new ArrayList();

    private Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public Inventory GetInventory()
    {
        return _inventory;
    }

    public void InsertNewQuest(int questId)
    {
        _acceptedQuests.Add(questId);

        return;
    }

    public void RemoveAcceptedQuestInsert(int questId)
    {
        int currentCount = 0;

        foreach(int acceptedQuestId in _acceptedQuests)
        {
            if(acceptedQuestId == questId)
            {
                _acceptedQuests.RemoveAt(currentCount);
            }

            currentCount += 1;
        }
    }

    public void AddSolvedQuestInsert(int questId)
    {
        _solvedQuests.Add(questId);
    }

    public ArrayList GetAcceptedQuests()
    {
        return _acceptedQuests;
    }

    public ArrayList GetSolvedQuests()
    {
        return _solvedQuests;
    }

    public void AddItemToInventory(Item item)
    {
        _inventory.StoreItemInInventory(item.gameObject);
    }
}


