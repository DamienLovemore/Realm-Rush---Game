using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("How fast this enemy can move")]
    [SerializeField][Range(0, 5f)] private float movementSpeed = 1f;

    private List<Node> path = new List<Node>();
    private Enemy enemyPenalty;
    private GridManager gridManager;
    private Pathfinder pathfinder;

    void OnEnable()
    {        
        //Finds all the path to follow
        this.FindPath();
        //Teleport them to the first tile of the path
        this.ReturnToStart();
        //Starts following the path until the end
        StartCoroutine(this.FollowPath());
    }

    void Awake()
    {
        this.enemyPenalty = this.gameObject.GetComponent<Enemy>();
        this.gridManager = FindObjectOfType<GridManager>();
        this.pathfinder = FindObjectOfType<Pathfinder>();
    }

    //Find the path for the enemy to follow whithout us having to set it
    //manually in the editor by SerializeField.
    private void FindPath()
    {
        //Clears the path, so the enemy can start to walk all over again
        this.path.Clear();
        this.path = this.pathfinder.GetNewPath();        
    }

    //Makes the enemy to appear in the start position
    private void ReturnToStart()
    {
        this.transform.position = this.gridManager.GetPositionFromCoordinates(this.pathfinder.StartCoordinates);
    }

    //Function that do the actions when the enemy has passed
    //the path
    private void FinishPath()
    {
        //Steals the player gold if the enemy has reached the
        //end of the path
        this.enemyPenalty.StealGold();
        //After following all the path, the enemy is set back
        //to the object pool
        this.gameObject.SetActive(false);
    }

    //Function used for the enemy movement between the tiles
    private IEnumerator FollowPath()
    {
        for(int cont = 0;cont < this.path.Count; cont++)
        {
            //Calculates where the enemy is going to move into
            Vector3 startPosition = this.transform.position;
            Vector3 endPosition = this.gridManager.GetPositionFromCoordinates(this.path[cont].coordinates);

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

        this.FinishPath();
    }
}
