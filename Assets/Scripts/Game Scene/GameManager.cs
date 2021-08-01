using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Vector3 fp;   //First touch position
    Vector3 lp;   //Last touch position
    float dragDistance;  //minimum distance for a swipe to be registered

    [SerializeField] Box7Collision box7Collision;
    SaveAndLoad saveAndLoad;
    StupidGuyCollision stupidGuyCollision;
    AudioSource gameBackgroundMusic, swipeSound;
    [SerializeField] AudioSource victorySound, defeatSound;
    GameObject listOfBox, stupidGuy;
    GameObject[] boxes;
    float timeTextWidth, timeTextHeight;
    int nobox, highscore;
    [SerializeField] GameObject timeText, scoreText, gameOverText, nextLevelButton, tryAgainButton, gameOverUI, gameOverPenal, highScoreText, finalScoreText, homeButton;
    [SerializeField] RectTransform canvasRectTransform, timeTextRectTransform, ScoreTextRectTransform, victoryRectTransform, defeatRectTransform;
    public float canvasWidth, canvasHeight;
    public float time;
    public int boxCount, score;

    void Awake()
    {
        //Initializing
        saveAndLoad = transform.GetComponent<SaveAndLoad>();
        gameBackgroundMusic = transform.GetComponent<AudioSource>();
        stupidGuy = transform.GetChild(3).gameObject;
        stupidGuyCollision = stupidGuy.GetComponent<StupidGuyCollision>();
        time = 20f;
        listOfBox = transform.GetChild(1).gameObject;
        box7Collision = listOfBox.transform.GetChild(6).gameObject.GetComponent<Box7Collision>();
        boxCount = 0;
        nobox = listOfBox.transform.childCount;
        Time.timeScale = 1;
        boxes = new GameObject[nobox];

        for (int i = 0; i < nobox; i++)
        {
            boxes[i] = listOfBox.transform.GetChild(i).gameObject;
        }

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen

        //fixing the UI
        canvasWidth = canvasRectTransform.rect.width * canvasRectTransform.localScale.x;
        canvasHeight = canvasRectTransform.rect.height * canvasRectTransform.localScale.y;

        timeTextWidth = canvasWidth / 3;
        timeTextHeight = canvasHeight / 10;

        timeTextRectTransform.sizeDelta = new Vector2(timeTextWidth, timeTextHeight);
        timeTextRectTransform.anchoredPosition = new Vector2(timeTextHeight / 10, -timeTextHeight / 3);

        ScoreTextRectTransform.sizeDelta = new Vector2(timeTextWidth, timeTextHeight);
        ScoreTextRectTransform.anchoredPosition = new Vector2(-timeTextHeight / 10, -timeTextHeight / 3);

        defeatRectTransform.sizeDelta = new Vector2(canvasWidth / 10 * 9, canvasHeight / 10);
        defeatRectTransform.anchoredPosition = new Vector2(0, canvasHeight / 4);

        victoryRectTransform.sizeDelta = new Vector2(canvasWidth / 10 * 9, canvasHeight / 10);
        victoryRectTransform.anchoredPosition = new Vector2(0, canvasHeight / 4);
    }

    void Update()
    {
        Swiping();
    }

    void FixedUpdate()
    {
        TimeCounting();

        CheckWinLose();
    }

    void TimeCounting()
    {
        if(time >= 0 && !stupidGuyCollision.isGameOver)
        {
            time -= Time.deltaTime;
            timeText.GetComponent<Text>().text = "Time Left: \n\t" + time.ToString("0") + " Secs";
        }
        else if (time >= 0 && stupidGuyCollision.isGameOver) {
            timeText.GetComponent<Text>().text = "Time Left: \n\t" + time.ToString("0") + " Secs";
            //do nothing
        }
        else {
            GameOver();
            timeText.GetComponent<Text>().text = "Time Left: \n\t" + 0 + " Secs";
        }
    }

    void Swiping()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position; //first point
                lp = touch.position; //last point
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {
                            //Debug.Log("Right Swipe");
                            PushBox(new Vector2(1500, 0));
                        }
                        else
                        {
                            //Debug.Log("Left Swipe");
                            PushBox(new Vector2(-1500, 0));
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {
                            //Debug.Log("Up Swipe");
                        }
                        else
                        {
                            //Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    //Debug.Log("Tap");
                }
            }
        }
    }

    void CheckWinLose() {
        if (boxCount == nobox && stupidGuyCollision.isGameOver)
        {
            Victory();
        }
        else if (box7Collision.box7OnFloor == true && boxCount != nobox)
        {
            GameOver();
            //Debug.Log("Box 7 hits the floor somehow");
        }
        else if (boxCount == nobox)
        {
            //Do nothing
        }
        else if (!stupidGuyCollision.isGameOver) {
            //Do nothing
        }
        else if (stupidGuy.transform.GetComponent<Rigidbody2D>().velocity.y >= 10){
            GameOver();
            //Debug.Log("Flying too fast");
        }
        else if (stupidGuy.transform.GetComponent<Rigidbody2D>().velocity.x >= 3 || stupidGuy.transform.GetComponent<Rigidbody2D>().velocity.x <= -3)
        {
            GameOver();
            //Debug.Log("Flying too fast");
        }
        else
        {
            //The stupid guy hits ground / other boxes other than box7 before all boxes removed
            GameOver();
            //Debug.Log("Hits Game Over Zone");
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameBackgroundMusic.Stop();

        if (score > saveAndLoad.load())
        {
            saveAndLoad.save();
        }
        highscore = saveAndLoad.load();

        //Set current UI to inactive
        scoreText.SetActive(false);
        timeText.SetActive(false);

        //Set Game Over UI to active
        defeatRectTransform.gameObject.SetActive(true);
        highScoreText.GetComponent<Text>().text = "High Score: " + highscore;
        finalScoreText.GetComponent<Text>().text = "Score: " + score;
        gameOverUI.SetActive(true);
        tryAgainButton.SetActive(true);

        gameOverPenal.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, canvasHeight);

        finalScoreText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 10 * 9, canvasHeight / 10);
        finalScoreText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -finalScoreText.GetComponent<RectTransform>().sizeDelta.y / 2);

        highScoreText.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth / 10 * 9, canvasHeight / 10);
        highScoreText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, finalScoreText.GetComponent<RectTransform>().sizeDelta.y / 2);

        homeButton.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasHeight / 10, canvasHeight / 10);
        homeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -finalScoreText.GetComponent<RectTransform>().sizeDelta.y * (5 / 2));

        defeatSound.Play();
    }

    void Victory()
    {
        Time.timeScale = 0;
        victoryRectTransform.gameObject.SetActive(true);
        nextLevelButton.SetActive(true);

        victorySound.Play();
    }

    bool StupidGuyStable() {
        //See if the stupid guy is stable
        return stupidGuy.transform.GetComponent<Rigidbody2D>().velocity.y > -3f;
    }

    public bool LastBoxStable() {
        //See if the coming box is stable
        bool allBoxesStable = false;

        for (int i = boxCount; i < nobox; i++)
        {
            if (boxes[i].GetComponent<Rigidbody2D>().velocity.y < -0.3f && boxes[i].GetComponent<Rigidbody2D>().velocity.x <= 0.1f && boxes[i].GetComponent<Rigidbody2D>().velocity.x >= -0.1f && CheckAngle(boxes[i], 10f, 1f))
            {
                allBoxesStable = false;
            }
            else
            {
                allBoxesStable = true;
            }
        }
        if(allBoxesStable == true && CheckAngle(boxes[boxCount], 0f, 1f) && CheckYVelocity(boxes[boxCount], 0f, 0.01f))
        {
            return true;
        }
        else {
            return false;
        }
    }

    void PushBox(Vector2 force)
    {
        if (StupidGuyStable() && LastBoxStable())
        {
            swipeSound = boxes[boxCount].GetComponent<AudioSource>();
            boxes[boxCount++].GetComponent<Rigidbody2D>().AddForce(force);
            scoreText.GetComponent<Text>().text = "Score: \n\t" + ++score;
            swipeSound.Play();
        }
    }

    bool CheckAngle(GameObject gameObject, float angle, float interval)
    {
        return ((gameObject.transform.eulerAngles.z <= (angle + interval) || (gameObject.transform.eulerAngles.z - 360f) <= (angle + interval)) && gameObject.transform.eulerAngles.z >= (angle - interval));
    }

    bool CheckYVelocity(GameObject gameObject, float speed, float interval)
    {
        return (gameObject.GetComponent<Rigidbody2D>().velocity.y <= (speed + interval) && gameObject.GetComponent<Rigidbody2D>().velocity.y >= (speed - interval));
    }
}