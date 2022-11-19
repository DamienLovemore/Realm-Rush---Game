using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("Determines if this tile is allowed to build a tower")]
    [SerializeField] private bool isPlaceable;

    //Event that happens when the player clicks on the game object that
    //this script is attached to
    void OnMouseDown()
    {
        if(this.isPlaceable)
        {
            Debug.Log(transform.name);
        }        
    }
}
