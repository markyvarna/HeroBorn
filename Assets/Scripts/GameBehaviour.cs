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
    public const int maxItems = 4;
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

    // MARK: Example of Delegate Method (DM)
    /*DM: declare a public delegate type name DebugDelegate to hold a method that
    takes in a string parameter and returns void*/
    public delegate void DebugDelegate(string newText);
    //DM: create a new DebugDelegate instance named debug
    public DebugDelegate debug = Print;



    private void Start()
    {

        Initialize();
        //implimenting our custom generic class InventoryList
        //all the InventroyList does is print out when a new InventoryList is initialized
        InventoryList<string> inventoryList = new InventoryList<string>();
        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);

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
                // MARK: Error Handling with Try-Catch blocks (TC)
                /*TC: declares the try block. Declares the catch block, and defines
                System.ArgutmentException as the exception type it will handle
                 and e as the local variable name, restarts the game at the default
                 scene index if exception is thrown which can be accessed at e which
                 is converted to a string using the .ToString() method. Add a finally
                 block  to signal the end of the exception handling code*/
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }
                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting to scene: 0" + e.ToString());
                }
                finally
                {
                    debug("Restart handled...");
                }

            }
        }
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        //this is our custom extension method 
        _state.FancyDebug();
        //Debug.Log(_state);
        //DM: replace Debug.Log() with call to the debug delegate instance
        debug(_state);

        // MARK: Subscribing to Firing Events (SFE)
        //SFE: Find player object in scene and stores its game object in local var
        GameObject player = GameObject.Find("Player");
        /*SFE: Uses GetComponent() to retrieve a ref to the PlayerBehaviour class
        attached to the player and stores it in local var*/
        PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();
        //SFE: Subscribes to the playerJump event declared in PlayerBehaviour with
        //attached method HandlePlayerJump()
        playerBehaviour.playerJump += HandlePlayerJump;
    }
    //SFE: Declares HandlePlayerJump() with signature that matches the events type
    public void HandlePlayerJump()
    {
        debug("player has jumped");
    }
    //DM: declares print as a static method that takes in a string param and logs it
    //to the console.
    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

}
