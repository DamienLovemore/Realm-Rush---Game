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

    private GridManager gridManager;
    private Vector2Int coordinates = new Vector2Int();

    void Awake() 
    {
        this.gridManager = FindObjectOfType<GridManager>();    
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
        if(this.isPlaceable)
        {
            bool isPlaced = towerHandler.CreateTower(towerHandler, this.transform.position);            
            //Prevents from placing the tower twice in the same location
            this.isPlaceable = !isPlaced;
        }        
    }
}
