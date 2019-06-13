using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public GameBehaviour gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();

    }
    /*When another object runs into Pickup_Item with its isTrigger turned
    off, Unity automatically calls OnCollisionEnter method.
    OnCollisionEnter() comes with a parameter that stores a reference to
    the Collider that ran into it, this reference is found with the collision
    class property collision.gameObject. This prop can be used to get the game
    objects name as seen below.  
    */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item Collected!");
            gameManager.Items += 1;
        }
    }

}
