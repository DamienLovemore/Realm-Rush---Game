using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Makes the script run in the play mode and in the editor mode as well.
[ExecuteAlways]
public class CoordinateLabel : MonoBehaviour
{
    private TextMeshPro displayLabel;
    private Vector2Int coordinates = new Vector2Int();

    void Awake() 
    {
        this.displayLabel = GetComponent<TextMeshPro>();
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
        }
    }

    //Calculates the coordinates for this tile object and display it
    private void DisplayCoordinates()
    {
        //Calculates the coordinate for this tile (Divides by the snap move value,
        //so it display 0,1 instead of 0, 10 with 10 in the move for snap settings)
        coordinates.x = Mathf.RoundToInt(this.transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        //Uses z instead of y, because y is up and down not front or back
        coordinates.y = Mathf.RoundToInt(this.transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        this.displayLabel.text = $"{this.coordinates.x},{this.coordinates.y}";
    }

    //Updates the tile name to reflect its current position
    private void UpdateObjectName()
    {
        this.transform.parent.name = this.coordinates.ToString();
    }
}
