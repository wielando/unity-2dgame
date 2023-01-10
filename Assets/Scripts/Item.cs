using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public int _itemId;

    private static GameObject _currentItem;

    public bool _isItemCollectable;
    public bool _isItemExploreable;
    public bool _isItemStackable;

    private bool _isItemAlreadyExplored;

    [TextArea(1, 10)]
    public string[] _itemText;
    [TextArea(1, 10)]
    public string[] _exploreText;

    public GameObject _player;
    public double _enterRadius = 0.32f;
    
    private DialogManager _dialogManager;
    private Inventory _inventory;

    // Start is called before the first frame update
    void Start()
    {
        // If there is no player object given try to get one with Tag "Player"
        // if there is still no player found end the quest
        if (_player == null)
        {
            _player = (GameObject.FindGameObjectWithTag("Player") != null) ? _player = GameObject.FindGameObjectWithTag("Player") : null;

            if (_player == null)
                return;
        }


        _dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        if(_isItemExploreable)
            _isItemAlreadyExplored = false;
    }

    private void Update()
    {
        if (_currentItem != null)
        {

            // Add the Item into Inventory If the previous Dialogue for collecting an Item is done and is now collectable
            if (!_dialogManager.GetDialogState() && _dialogManager.GetDialogType() == "CollectingItem" && _dialogManager.GetCollectableNow())
            {
                CollectItem();

                // Set DialogManager Members to default
                _dialogManager.SetCollectableNow(false);
                _dialogManager.SetDialogType(null);
                _isItemAlreadyExplored = false;
            }
        }
    }

    public void ExecuteItemBehaviour()
    {
        SetCurrentItem(gameObject);

        // If Item is Exploreable and collectable execute a Choice DialogBox
        if(_isItemExploreable && _isItemCollectable)
        {
            if(_inventory.IsItemAlreadyInInventar(gameObject))
            {
                if(_isItemStackable)
                {
                    //TODO
                    return;
                }
                
                // If Item is already in Inventory and not Stackable just display the explore dialog
                _dialogManager.StartExploreDialoge(_exploreText);

                return;
            }

            if (_isItemExploreable && _isItemAlreadyExplored)
            {
                // If Item is not in Inventory and is Collectable and has a ExploreText open the Choice DialogBox
                _dialogManager.OpenSelectDialogBox(_itemText, _exploreText);
            }
            else
            {
                _dialogManager.StartExploreDialoge(_exploreText);
                _isItemAlreadyExplored = true;
            }
            
            return;
        }

        if(_isItemCollectable && !_isItemExploreable)
        {
            _dialogManager.StartItemDialog(_itemText);

            return;
        }

        if(_isItemExploreable && !_isItemCollectable)
        {
            _dialogManager.StartExploreDialoge(_exploreText);
            return;
        }
    }

    private void SetCurrentItem(GameObject currentItem)
    {
        // Make sure that the there is only 1 currentItem at this point
        SetCurrentItemToDefault();
       

        _currentItem = currentItem;
    }

    public void SetCurrentItemToDefault()
    {
        if (_currentItem != null)
            _currentItem = null;
    }

    public GameObject GetCurrentItem()
    {
        return _currentItem;
    }

    public void CollectItem()
    {
        Debug.Log("Collecting Item function called!");
        _inventory.StoreItemInInventory(_currentItem);
    }

    public void ExecuteItemEffect()
    {
        return;
    }

    public int GetItemId()
    {
        return _itemId;
    }
}
