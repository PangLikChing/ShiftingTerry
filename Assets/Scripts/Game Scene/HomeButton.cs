using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void Home()
    {
        //Load title scene
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
