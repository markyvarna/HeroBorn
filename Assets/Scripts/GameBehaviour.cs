using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomExtensions;

//The above line of code is importing or Custom Exntension which consists of a
//custom Debug.Log extension method we made
public class GameBehaviour : MonoBehaviour, IManager
{
    public string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public string labelText = "Collect all 4 items and win your freedom!";
    public const int maxItems = 1;
    private int _itemsCollected = 0;
    private int _playerHP = 10;
    public bool showWinScreen = false;
    public bool showLossScreen = false;


    public int Items
    {
        get
        {
            return _itemsCollected;
        }
        set
        {
            _itemsCollected = value;
            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                //this will pause the game and disable any extra game input
                Time.timeScale = 0f;
            } else
            {
                labelText = "Items Found, only" + (maxItems - _itemsCollected) +
                    "more to go!";
            }

            Debug.LogFormat("Items: {0}", _itemsCollected);
        }
    }

    public int HP
    {
        get
        {
            return _playerHP;
        }
        set
        {
            _playerHP = value;
            if (_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch! Thats gotta hurt...";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);

        }
    }

    private void Start()
    {

        Initialize();
    }

    //On Screen UI Code, Programatically developing Player HUD
    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);
        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "You Won! Want to play again?"))
            {
                //calls restartLevel() from Utilities static class, for this reason
                //it does not need to be instantiated
                Utilities.RestartLevel(0);
            }
        }
        //The exact same logic is applied to the Loss Button...
        if (showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
                Screen.height / 2 - 50, 200, 100), "You Lose..."))
            {
                Utilities.RestartLevel();
            }
        }
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        //this is our custom extension method 
        _state.FancyDebug();
        Debug.Log(_state);
    }

}
