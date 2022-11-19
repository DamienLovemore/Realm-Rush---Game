using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    private int currentHitPoints;
    
    void Start() 
    {
        //Makes the current hit points value to be adjusted to
        //to the max health value that was set in the editor
        this.currentHitPoints = this.maxHitPoints;
    }
    
    //When a arrow collides with this enemy, it takes hit until
    //it dies (is destroyed)
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Sim @");
        this.TakeHit();
    }

    //Takes the hit from the bolts, and triggers death when
    //health is zero
    private void TakeHit()
    {
        //Takes damage
        this.currentHitPoints -= 1;

        //Destroyes the enemy
        if (this.currentHitPoints == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
