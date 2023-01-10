using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    Vector2 movement;
    public Rigidbody2D rb;


    private List<Item> _collectableItems = new List<Item>();
    private List<Item> _itemsToInteract = new List<Item>();


    private void Start()
    {
        foreach(Item item in FindObjectsOfType<Item>())
        {
            _itemsToInteract.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameTime.GetIsPaused()) return;
        
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Get every Item
        foreach(Item item in _itemsToInteract)
        {
            // Check nearest Item to the player within a Radius
            if(Vector3.Distance(gameObject.transform.position, item.GetComponent<BoxCollider2D>().transform.position) <= item._enterRadius) 
            {
                // If player press KeyCode K on the nearest Item execute Item Behaviour
                if (Input.GetKeyDown(KeyCode.K))
                {
                    if (item.GetCurrentItem() != null)
                        item.SetCurrentItemToDefault();

                    item.ExecuteItemBehaviour();
                }
            }
        }                                                                        

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * GameTime._deltaTime);
    }
}
