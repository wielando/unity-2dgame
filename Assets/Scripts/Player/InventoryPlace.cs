using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlace : MonoBehaviour
{
    public KeyCode _activationKey;
    public Item _currentItem;

    private float _itemAmount;
    
    public void PlaceItemToInventoryPlace(GameObject item)
    {
        GameObject duplicatedItem = Instantiate(item, gameObject.transform);

        Debug.Log(duplicatedItem);
    }

}
