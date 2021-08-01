using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public void save()
    {
        PlayerPrefs.SetInt("highscore", gameManager.score);
        PlayerPrefs.Save();
    }

    public int load()
    {
        int highscore;
        highscore = PlayerPrefs.GetInt("highscore");
        return highscore;
    }
}