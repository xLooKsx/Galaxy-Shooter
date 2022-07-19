using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void playSingle()
    {
        Debug.Log("Singleplay mode selected");
        SceneManager.LoadScene("SinglePlayer");
    }

    public void playcoOp()
    {
        Debug.Log("co-op mode selected");
        SceneManager.LoadScene("Co-opMode");
    }
}
