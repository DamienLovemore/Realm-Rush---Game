using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path = new List<Waypoint>();
    [SerializeField] private float secondsEnemyMovement = 1f;
    
    void Start()
    {
        StartCoroutine(this.FollowPath());
    }

    //Function used for the enemy movement between the tiles
    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {            
            this.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(this.secondsEnemyMovement);
        }
    }
}
