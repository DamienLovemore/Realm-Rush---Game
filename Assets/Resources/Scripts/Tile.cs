using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower towerHandler;
    [Tooltip("Determines if this tile is allowed to build a tower")]
    [SerializeField] private bool isPlaceable;
    //Property that is used to return or set a private variable
    public bool IsPlaceable{get{return isPlaceable;}}

    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;
    private Pathfinder pathfinder;
    
    void Awake() 
    {
        this.gridManager = FindObjectOfType<GridManager>();
        this.pathfinder = FindObjectOfType<Pathfinder>();    
    }

    void Start() 
    {
        if(this.gridManager != null)
        {
            //Converts the position in the world from the unity to coordinates
            //used in the game (every 10 unity units is 1)
            this.coordinates = this.gridManager.GetCoordinatesFromPosition(this.transform.position);

            //If this Node(tile) is not walkable, then block it in the grid
            //manager script
            if(!isPlaceable)
            {
                this.gridManager.BlockNode(this.coordinates);
            }
        }
    }

    //Event that happens when the player clicks on the game object that
    //this script is attached to
    void OnMouseDown()
    {
        //Verifies if this Node(tile) is not blocked, and that it will
        //not block the path of the enemies if a tower is placed here
        if((this.gridManager.GetNode(this.coordinates).isWalkable) && (!this.pathfinder.WillBlockPath(this.coordinates)))
        {
            bool isSuccesful = towerHandler.CreateTower(towerHandler, this.transform.position);            
            
            if(isSuccesful)
            {
                //Mark that this Node is not available to place towers
                this.gridManager.BlockNode(this.coordinates);
                this.pathfinder.NotifyReceivers();
            }                
        }        
    }
}
