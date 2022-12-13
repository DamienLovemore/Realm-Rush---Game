using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    void Update()
    {
        this.OnProcessShortcuts();
    }

    //Process the key press of the player, listening for shortcuts
    //to be ativated
    private void OnProcessShortcuts()
    {
        //Shortcut for closing the game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            this.CloseGame();
        }
        //Shortcut for toggle window mode (Fullscreen)
        else if(Input.GetKeyDown(KeyCode.F11))
        {
            this.ToggleWindowMode();
        }
    }

    //End the game
    private void CloseGame()
    {
        Application.Quit();
    }

    //Toggle the window mode of the game between windowed and
    //exclusive fullscreen mode
    private void ToggleWindowMode()
    {
        FullScreenMode currentScreenMode = Screen.fullScreenMode;

        //If it is in fullscreen mode, then it switches to windowed mode
        if((currentScreenMode != FullScreenMode.Windowed) && (currentScreenMode != FullScreenMode.MaximizedWindow))
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        //If it is in windowed mode then it must switch to fullscreen mode
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
    }
}
