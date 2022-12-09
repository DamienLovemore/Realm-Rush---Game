using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Tower towerHandler;
    [Tooltip("Determines if this tile is allowed to build a tower")]
    [SerializeField] private bool isPlaceable;
    //Property that is used to return or set a private variable
    public bool IsPlaceable{get{return isPlaceable;}}

    //Event that happens when the player clicks on the game object that
    //this script is attached to
    void OnMouseDown()
    {
        if(this.isPlaceable)
        {
            bool isPlaced = towerHandler.CreateTower(towerHandler, this.transform.position);            
            //Prevents from placing the tower twice in the same location
            this.isPlaceable = !isPlaced;
        }        
    }
}
