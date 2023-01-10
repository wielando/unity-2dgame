using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<GameObject> _items = new List<GameObject>();
    private GameObject[] _places = new GameObject[3];


    // Start is called before the first frame update
    void Start()
    {
        _places[0] = GameObject.Find("Place_1");
        _places[1] = GameObject.Find("Place_2");
        _places[2] = GameObject.Find("Place_3");
    }


    public void StoreItemInInventory(GameObject item)
    {
        if (IsItemAlreadyInInventar(item))
            if (!IsItemStackable(item))
                return;


        _items.Add(item);
        
        foreach(GameObject currentPlace in _places)
        {
            if(currentPlace.GetComponent<InventoryPlace>()._currentItem == null)
            {
                Debug.Log("Item placed in Inventory!");
                //currentPlace.GetComponent<InventoryPlace>()._currentItem = item.GetComponent<Item>();

                currentPlace.GetComponent<InventoryPlace>().PlaceItemToInventoryPlace(item);

                return;
            }
        }

    }

    private bool IsItemStackable(GameObject item)
    {
        return item.GetComponent<Item>()._isItemStackable;
    }

    public bool IsItemAlreadyInInventar(GameObject item)
    {
        foreach(GameObject currentItem in _items)
        {
            if(ReferenceEquals(currentItem, item))
            {
                return true;
            }
        }

        return false;
    }

    public void DestroyItemInInventory(GameObject item)
    {
        return;
    }
    
}
