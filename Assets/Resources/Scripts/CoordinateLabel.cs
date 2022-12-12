using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Makes the script run in the play mode and in the editor mode as well.
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour
{
    [SerializeField] private Color32 defaultColor = Color.white;
    [SerializeField] private Color32 blockedColor = Color.gray;
    [SerializeField] private Color32 exploredColor = Color.yellow;
    [SerializeField] private Color32 pathColor = new Color32(255, 127, 0, 255);

    private TextMeshPro displayLabel;
    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;

    void Awake() 
    {
        this.gridManager = FindObjectOfType<GridManager>();

        this.displayLabel = GetComponent<TextMeshPro>();
        this.displayLabel.enabled = false;
        
        this.DisplayCoordinates();
    }

    void Update()
    {
        //Runs this code only when we are not playing the game
        //(Editor mode)
        if(!Application.isPlaying)
        {
            this.DisplayCoordinates();
            this.UpdateObjectName();
            this.displayLabel.enabled = true;
        }

        this.SetLabelColor();
        this.ToggleCoordinatesLabels();
    }

    //When the player press the debug key (letter C) it should toggle
    //coordinates labels visibility
    private void ToggleCoordinatesLabels()
    {        
        if(Input.GetKeyDown(KeyCode.C))
        {
            this.displayLabel.enabled = !this.displayLabel.IsActive();
        }
    }

    private void SetLabelColor()
    {
        //If there is no grid(tile) manager then it cannot
        //change the labels colors
        if(this.gridManager == null)
            return;

        //Gets the node(tile) of this coordinate that
        //contains its info
        Node node = gridManager.GetNode(this.coordinates);
        //If it did not find the coordinate then leave
        if (node == null)
            return;

        //Sets the color of the tile accordinly to the
        //to its property following a set priority

        //If the tile is not walkable
        if(!node.isWalkable)
        {
            this.displayLabel.color = this.blockedColor;
        }
        //If the tile is part of the path
        else if(node.isPath)
        {
            this.displayLabel.color = this.pathColor;
        }
        //If the tile has already been explored by enemies
        else if (node.isExplored)
        {
            this.displayLabel.color = this.exploredColor;
        }
        //Normal color if it all the other priorities did
        //not match
        else
        {
            this.displayLabel.color = this.defaultColor;
        }
    }

    //Calculates the coordinates for this tile object and display it
    private void DisplayCoordinates()
    {
        if(this.gridManager == null)
            return;

        //Calculates the coordinate for this tile (Divides by the snap move value,
        //so it display 0,1 instead of 0, 10 with 10 in the move for snap settings)
        coordinates.x = Mathf.RoundToInt(this.transform.parent.position.x / this.gridManager.UnityGridSize);
        //Uses z instead of y, because y is up and down not front or back
        coordinates.y = Mathf.RoundToInt(this.transform.parent.position.z / this.gridManager.UnityGridSize);

        this.displayLabel.text = $"{this.coordinates.x},{this.coordinates.y}";
    }

    //Updates the tile name to reflect its current position
    private void UpdateObjectName()
    {
        this.transform.parent.name = this.coordinates.ToString();
    }
}
