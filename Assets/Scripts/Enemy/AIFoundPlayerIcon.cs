using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFoundPlayerIcon : MonoBehaviour
{

    private GameObject childObject;
    private SpriteRenderer sprite;

    Vector2 AIFoundPlayerSavedPosition;

    void Start()
    {
        this.childObject = GameObject.FindGameObjectWithTag("AIFoundPlayer");
        this.sprite = this.childObject.GetComponent<SpriteRenderer>();
    }

    public void SetAIFoundPlayerIconStatus(bool status)
    {
        this.sprite.enabled = status;
    }

    public void savePosition()
    {
        this.AIFoundPlayerSavedPosition = new Vector2(this.childObject.transform.position.x, this.childObject.transform.position.y);
    }

    public void setIconToDefaultPosition() 
    {
        this.childObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.childObject.transform.rotation.z * -1.0f);
        this.childObject.transform.position = this.AIFoundPlayerSavedPosition;
    }

}
