using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainButton : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource gameBackgroundMusic, defeatMusic;

    void Start()
    {
        //Fix the UI
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -(gameManager.canvasHeight / 3));
        GetComponent<RectTransform>().sizeDelta = new Vector2(gameManager.canvasWidth / 3 * 2, gameManager.canvasHeight / 10);

        //Disable gameObject
        gameObject.SetActive(false);
    }

    public void TryAgain()
    {
        //Stop playing victory music
        defeatMusic.Stop();
        //Start playing BGM
        gameBackgroundMusic.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
