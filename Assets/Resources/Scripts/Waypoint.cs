using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [Tooltip("Determines if this tile is allowed to build a tower")]
    [SerializeField] private bool isPlaceable;

    //Event that happens when the player clicks on the game object that
    //this script is attached to
    void OnMouseDown()
    {
        if(this.isPlaceable)
        {
            Instantiate(towerPrefab, this.transform.position, Quaternion.identity);
            //Prevents from placing the tower twice in
            //the same location
            this.isPlaceable = false;
        }        
    }
}
