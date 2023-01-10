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
        Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Quaternion newRotation = new Quaternion(0, 0, 0,0);

        Instantiate(item, newPosition, newRotation, gameObject.transform);

        SetCurrentItem(item.GetComponent<Item>());
    }

    private void SetCurrentItem(Item item)
    {
        _currentItem = item;
    }

}
