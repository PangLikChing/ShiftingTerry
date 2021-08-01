using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject victoryImage;
    [SerializeField] AudioSource gameBackgroundMusic, victoryMusic;
    [SerializeField] Box7Collision box7Collision;
    float stageLocalScaleX;
    GameObject listOfBox, stupidGuy, stage;
    StupidGuyCollision stupidGuyCollision;
    StupidGuyMovement stupidGuyMovement;
    Vector3[] boxesPosition;
    Vector3 stupidGuyPosition, stageScaleDownFirstStage, stageScaleDownSecondStage;
    int nobox;

    void Awake()
    {
        //Initialize
        listOfBox = gameManager.transform.GetChild(1).gameObject;
        nobox = listOfBox.transform.childCount;
        boxesPosition = new Vector3[nobox];
        stupidGuy = gameManager.transform.GetChild(3).gameObject;
        stage = gameManager.transform.GetChild(4).gameObject;
        stupidGuyPosition = stupidGuy.transform.position;
        stageLocalScaleX = stage.transform.localScale.x;
        stupidGuyCollision = stupidGuy.GetComponent<StupidGuyCollision>();
        stupidGuyMovement = stupidGuyCollision.gameObject.GetComponent<StupidGuyMovement>();
        
        for (int i = 0; i < nobox; i++)
        {
            boxesPosition[i] = listOfBox.transform.GetChild(i).gameObject.GetComponent<Transform>().position;
        }

        stageScaleDownFirstStage = new Vector3(-stageLocalScaleX / 10, 0, 0);
        stageScaleDownSecondStage = new Vector3(-stageLocalScaleX / 30, 0, 0);
    }

    void Start()
    {
        //UI
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -(gameManager.canvasHeight / 3));
        GetComponent<RectTransform>().sizeDelta = new Vector2(gameManager.canvasWidth / 3 * 2, gameManager.canvasHeight / 10);

        //Disable gameObject
        gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        //Reset
        stupidGuyCollision.isGameOver = false;
        box7Collision.box7OnFloor = false;
        Time.timeScale = 1;
        gameManager.boxCount = 0;
        gameManager.time += 10f;
        stupidGuy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        stupidGuyMovement.timeIntervalJump = 0f;
        stupidGuyMovement.timeIntervalMove = 0f;
        gameObject.SetActive(false);
        victoryImage.SetActive(false);

        //Scale down the stage
        //If the stage is larger half of its original size
        if(stage.transform.localScale.x > stageLocalScaleX / 2)
        {
            stage.transform.localScale += stageScaleDownFirstStage;
        }
        //If the stage is too small to scale anymore
        else if (stage.transform.localScale.x <= stageLocalScaleX / 30)
        {
            //Do nothing
            //Just in case
        }
        //If the stage is smaller than or equal to half of its original size
        else
        {
            stage.transform.localScale += stageScaleDownSecondStage;
        }

        //Reset the boxes
        for(int i = 0; i < nobox; i++) {
            listOfBox.transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            listOfBox.transform.GetChild(i).gameObject.GetComponent<Transform>().position = boxesPosition[i];
            listOfBox.transform.GetChild(i).gameObject.GetComponent<Transform>().eulerAngles = Vector3.zero;
        }

        //Reset the stupid guy
        stupidGuy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        stupidGuy.GetComponent<Transform>().position = stupidGuyPosition;
        stupidGuy.GetComponent<Transform>().eulerAngles = Vector3.zero;

        //Stop playing victory music
        victoryMusic.Stop();
        //Start playing BGM
        gameBackgroundMusic.Play();
    }
}
