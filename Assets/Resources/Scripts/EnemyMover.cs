using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Travel Path")]
    [Tooltip("The list of tiles that this enemy can move (path)")]
    [SerializeField] private List<Waypoint> path = new List<Waypoint>();

    [Header("Movement")]
    [Tooltip("How fast this enemy can move")]
    [SerializeField][Range(0, 5f)] private float movementSpeed = 1f;

    void Start()
    {
        StartCoroutine(this.FollowPath());
    }

    //Function used for the enemy movement between the tiles
    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            //Calculates where the enemy is going to move into
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = waypoint.transform.position;

            //Where in the movement is the enemy starting (Between the two
            //points;0 is start and 1 is the end position)
            float travelPercent = 0f;

            //Makes the enemy rotates so its forward part points
            //to the tile that it is going into
            this.transform.LookAt(endPosition);

            //While it still did not arrive at its destination, it
            //should keep moving
            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * this.movementSpeed;
                //Makes the enemy goes into the destination gradatively
                //and not at once
                this.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }            
        }
    }
}
