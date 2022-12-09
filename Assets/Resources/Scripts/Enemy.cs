using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;

    private Bank bankSystem;

    void Start()
    {
        this.bankSystem = FindObjectOfType<Bank>();
    }

    //Rewards the player with gold for deating this
    //enemy
    public void RewardGold()
    {
        //If it does not find the bank then do nothing
        if (this.bankSystem == null)
            return;

        //Deposit the gold amount into the bank
        this.bankSystem.Deposit(this.goldReward);
    }

    //Penalize the player with gold for letting this
    //enemy pass
    public void StealGold()
    {
        if (this.bankSystem == null)
            return;

        //Withdraw the gold amount from the bank
        this.bankSystem.Withdraw(this.goldPenalty);
    }
}
