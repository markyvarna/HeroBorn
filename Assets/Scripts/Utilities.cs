using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//if an entire class is marked as static, all methods and properties must be static as well
public static class Utilities
{
    public static int playerDeaths = 0;

    public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return "Next time you'll be at number" + countReference;
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
        Debug.Log("Player Deaths: " + playerDeaths);
        string message = UpdateDeathCount(ref playerDeaths);
        Debug.Log("Player Deaths: " + playerDeaths);
    }

    /*This is an example of Method Overloading, the parameters aka Method Signature
    is different which is how the compiler can tell the difference between the 2
    though they hold the same name.. */

    public static bool RestartLevel(int sceneIndex)
    {
        // MARK: Throwing Exceptions (TE)
        //TE: Declares if statemnt to check that sceneIndex is not less than zero
        if (sceneIndex < 0)
        {
            //TE: Throws an Argument Exception with a custom string message
            throw new System.ArgumentException("Scene index cannot be negative");
        }
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;
        return true;
    }


}
