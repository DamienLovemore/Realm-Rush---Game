using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("How fast this enemy can move")]
    [SerializeField][Range(0, 5f)] private float movementSpeed = 1f;

    private List<Waypoint> path = new List<Waypoint>();

    void Start()
    {
        //Finds all the path to follow
        this.FindPath();
        //Teleport them to the first tile of the path
        this.ReturnToStart();
        //Starts following the path until the end
        StartCoroutine(this.FollowPath());
    }

    //Find the path for the enemy to follow whithout us having to set it
    //manually in the editor, by SerializeField.
    private void FindPath()
    {
        //The object that have all the children path inside it
        //(Instead of finding all paths with the same tag cause 
        //you cannot be sure if it will return in the same order)
        GameObject pathFather = GameObject.FindGameObjectWithTag("Path");
        Waypoint[] waypointsChildren = pathFather.GetComponentsInChildren<Waypoint>();
        
        //Adds all the path found for the enemy to follow later
        foreach(Waypoint waypoint in waypointsChildren)
        {            
            path.Add(waypoint);
        }
    }

    //Makes the enemy to appear in the first path position
    private void ReturnToStart()
    {
        transform.position = this.path[0].transform.position;
    }

    //Function used for the enemy movement between the tiles
    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in this.path)
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

        //After following all the path, the enemy disappears
        Destroy(this.gameObject);
    }
}
