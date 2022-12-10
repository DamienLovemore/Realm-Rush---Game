using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGoldBalance;
    [SerializeField] private int startingBalance = 150;

    private int currentBalance;
    public int CurrentBalance{get{return this.currentBalance;}}

    //When the game starts, the player need to have
    //some money to start building two towers
    void Awake()
    {
        this.currentBalance = this.startingBalance;
        //Shows the current amount of gold in the game
        //HUD
        this.UpdateGoldDisplay();
    }

    private void UpdateGoldDisplay()
    {
        this.textGoldBalance.text = "Gold: " + this.currentBalance;
    }

    public void Deposit(int amount)
    {
        //If a negative value is passed, make it positive
        amount = Mathf.Abs(amount);

        this.currentBalance += amount;

        this.UpdateGoldDisplay();
    }

    private void GameOver()
    {
        //Gets the actual scene that the game is
        Scene currentScene = SceneManager.GetActiveScene();

        //Realoads this scene (realoads the game)
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void Withdraw(int amount)
    {
        amount = Mathf.Abs(amount);

        this.currentBalance -= amount;

        this.UpdateGoldDisplay();

        //If the player reaches a negative balance,
        //triggers game over
        if(this.currentBalance < 0)
        {
            this.GameOver();
        }        
    }

    public int GetCurrentBalance()
    {
        int returnValue = this.currentBalance;

        return returnValue;
    }
}
