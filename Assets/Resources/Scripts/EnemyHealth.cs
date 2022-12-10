using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes the component to be added automatically if
//it is not found
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    [SerializeField] private int difficultyRamp = 1;

    private int currentHitPoints;
    private  Enemy enemyPenalty;
    
    void OnEnable() 
    {
        //Makes the current hit points value to be adjusted to
        //to the max health value that was set in the editor
        this.currentHitPoints = this.maxHitPoints;
    }
    
    void Start()
    {
        this.enemyPenalty = GetComponent<Enemy>();
    }

    //When a arrow collides with this enemy, it takes hit until
    //it dies (is destroyed)
    void OnParticleCollision(GameObject other)
    {
        this.TakeHit();
    }

    //Takes the hit from the bolts, and triggers death when
    //health is zero
    private void TakeHit()
    {
        //Takes damage
        this.currentHitPoints -= 1;

        //the enemy is set back to the object pool, and the
        //enemies health is slightly increased to increase
        //the difficulty
        if (this.currentHitPoints == 0)
        {
            this.gameObject.SetActive(false);
            maxHitPoints += this.difficultyRamp;
            this.enemyPenalty.RewardGold();
        }
    }
}
