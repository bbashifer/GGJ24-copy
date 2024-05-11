using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MinigamePomPom : MonoBehaviour
{
    public MinigameManager minigameManager;
    

    
    // Flags to indicate key presses
    private bool isQPressed;
    private bool isWPressed;
    private bool isEPressed;
    private bool isRPressed;

    // Flag to indicate if Left or Right Arrow keys are being pressed
    public bool isArrowLeftPressed;
    public bool isArrowRightPressed;

    // Getter for Left or Right Arrow key press status
    public bool IsArrowKeyPressed => isArrowLeftPressed || isArrowRightPressed;

    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private Transform EnemySpawnPositionOnLoss;

    [SerializeField]
    private char KeyToPress;
    [SerializeField]
    private bool ToPressLeft;
    [SerializeField]
    private bool ToPressRight;

    [SerializeField]
    private float timer = 0f;
    [SerializeField]
    private float interval = 5f;

    public RawImage rawImage;
    [SerializeField]
    public List<Texture2D> imagesList;
    [SerializeField]
    public int targetImageNumber;

    public float timeRemaining = 30f;
    public bool timerIsRunning = false;
    [SerializeField]
    private int maxTries;
    [SerializeField]
    private int currentTries;
    [SerializeField]
    private int maxMisses;
    [SerializeField]
    private int currentMisses;
    [SerializeField]
    private int goalHits;
    [SerializeField]
    private int currentHits;

    public AudioSource audioSource;
    public AudioClip TenSecondsAlert;
    public AudioClip MissedHit;
    public AudioClip Hit;
    public AudioClip PoseChange;

    private bool tenSecondPlayed;

    public GameObject PlayerCamera;
    public GameObject GameCamera;

    public GameObject Pompom1;
    public GameObject Pompom2;
    public GameObject Pompom3;
    public GameObject Pompom4;

    [SerializeField]
    public List<MeshRenderer> PompomsLeft;
    [SerializeField]
    public List<MeshRenderer> PompomsRight;

    public Material RedMaterial;
    public Material BlueMaterial;


    public bool LeftPompomRed;
    public bool RightPompomRed;

    public GameObject PressE;
    public GameObject Instructions;

    public Endgame endgame;

    public GameObject minigameTrigger;





    private void Update()
    {
        DetectArrowKeyInput();

        if (isArrowLeftPressed && !LeftPompomRed)
        {
            LeftPompomRed = true;
            foreach(MeshRenderer n in PompomsLeft)
            {
                n.material = RedMaterial;
            }
        }
        else if(!isArrowLeftPressed && LeftPompomRed)
        {
            LeftPompomRed = false;
            foreach (MeshRenderer n in PompomsLeft)
            {
                n.material = BlueMaterial;
            }
        }

        if (isArrowRightPressed && !RightPompomRed)
        {
            RightPompomRed = true;
            foreach (MeshRenderer n in PompomsRight)
            {
                n.material = RedMaterial;
            }
        }
        else if (!isArrowRightPressed && RightPompomRed)
        {
            RightPompomRed = false;
            foreach (MeshRenderer n in PompomsRight)
            {
                n.material = BlueMaterial;
            }
        }

        if (isQPressed)
        {
            Pompom1.SetActive(false);
            Pompom2.SetActive(false);
            Pompom3.SetActive(false);
            Pompom4.SetActive(true);
        }
        if (isWPressed)
        {
            Pompom1.SetActive(false);
            Pompom2.SetActive(false);
            Pompom3.SetActive(true);
            Pompom4.SetActive(false);
        }
        if (isEPressed)
        {
            Pompom1.SetActive(false);
            Pompom2.SetActive(true);
            Pompom3.SetActive(false);
            Pompom4.SetActive(false);
        }
        if (isRPressed)
        {
            Pompom1.SetActive(true);
            Pompom2.SetActive(false);
            Pompom3.SetActive(false);
            Pompom4.SetActive(false);
        }

        if (timeRemaining <= 10 && !tenSecondPlayed)
        {
            tenSecondPlayed = true;
            audioSource.PlayOneShot(TenSecondsAlert);
        }
        // Detect Q, W, E, R key presses
        DetectKeyInput(KeyCode.Q, ref isQPressed);
        DetectKeyInput(KeyCode.W, ref isWPressed);
        DetectKeyInput(KeyCode.E, ref isEPressed);
        DetectKeyInput(KeyCode.R, ref isRPressed);

        timer += Time.deltaTime;
        if (timer >= interval)//Timer to generate new sequence 
        {
            currentMisses += 1;
            KeyToPressGenerator();
            timer = 0f;
        }

        if (isQPressed || isWPressed || isEPressed || isRPressed)//Key pressed
        {

            if (GameLogic())
            {
                currentHits += 1;
                audioSource.PlayOneShot(Hit);


                if (currentHits >= goalHits)
                {

                    MinigameEnd(true);
                }
                KeyToPressGenerator();
                timer = 0f;
            }
            else
            {
                currentMisses += 1;
                Debug.Log("PLay missedHit");
                audioSource.PlayOneShot(MissedHit);

                if (currentTries >= maxTries)
                {
                    KeyToPressGenerator();
                    currentTries = 0;
                }
                else
                {
                    currentTries += 1;

                }
            }

        }

        if (currentMisses >= maxMisses)
        {
            MinigameEnd(false);
        }


        if (timerIsRunning)//Minigame time countdown
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                MinigameEnd(false);
                timerIsRunning = false;
            }
        }
    }

    public void MinigameStart()
    {
        PressE.SetActive(false);
        Instructions.SetActive(true);
        PlayerCamera.SetActive(false);
        GameCamera.SetActive(true);

        Pompom1.SetActive(true);

        timerIsRunning = true;
        currentHits = 0;
        timeRemaining = 30f;
        currentMisses = 0;
        currentTries = 0;
        KeyToPressGenerator();
        tenSecondPlayed = false;
    }

    public void MinigameEnd(bool gameCompleted)
    {

        Pompom1.SetActive(false);
        Pompom2.SetActive(false);
        Pompom3.SetActive(false);
        Pompom4.SetActive(false);

        Instructions.SetActive(false);

        PlayerCamera.SetActive(true);
        GameCamera.SetActive(false);
        timerIsRunning = false;
        minigameManager.PlayerController.enabled = true;
        if (gameCompleted)
        {
            endgame.TurnOnCloset();
            minigameTrigger.SetActive(false);
        }
        else
        {
            Enemy.SetActive(true);
            Enemy.transform.position = EnemySpawnPositionOnLoss.position;
        }
        this.enabled = false;
    }

    private void KeyToPressGenerator()
    {

        bool[] boolArray = new bool[] { isQPressed, isWPressed, isEPressed, isRPressed };

        int randomValue1 = Random.Range(0, 4);

        switch (randomValue1)
        {
            case 0:
                KeyToPress = 'Q';
                break;
            case 1:
                KeyToPress = 'W';
                break;
            case 2:
                KeyToPress = 'E';
                break;
            case 3:
                KeyToPress = 'R';
                break;
            default:
                break;
        }

        int randomValue2 = Random.Range(0, 3);

        switch (randomValue2)
        {
            case 0:
                ToPressLeft = true;
                ToPressRight = true;
                break;
            case 1:
                ToPressLeft = false;
                ToPressRight = false;
                break;
            case 2:
                ToPressLeft = Random.Range(0, 2) == 0;
                ToPressRight = !ToPressLeft;
                break;
            default:
                break;
        }
        ImageNumberUpdate();
    }

    private bool GameLogic()
    {
        if(Logic1() && Logic2())
        {
            return true;
        }
        else
        {
            return false;
        }

        bool Logic1()
        {
            if (isQPressed || isWPressed || isEPressed || isRPressed)
            {
                if (KeyToPress == 'Q' && isQPressed)
                {
                    return true;
                }

                if (KeyToPress == 'W' && isWPressed)
                {
                    return true;
                }

                if (KeyToPress == 'E' && isEPressed)
                {
                    return true;
                }

                if (KeyToPress == 'R' && isRPressed)
                {
                    return true;
                }
            }
            return false;
        }

        bool Logic2()
        {
            if(isArrowLeftPressed == ToPressLeft && isArrowRightPressed == ToPressRight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void DetectKeyInput(KeyCode key, ref bool keyFlag)
    {
        keyFlag = Input.GetKeyDown(key);
    }

    private void DetectArrowKeyInput()
    {
        isArrowLeftPressed = Input.GetKey(KeyCode.Mouse0);
        isArrowRightPressed = Input.GetKey(KeyCode.Mouse1);
    }

    private void ImageNumberUpdate()
    {
        audioSource.PlayOneShot(PoseChange);

        if (KeyToPress == 'Q')
        {
            if (ToPressLeft && ToPressRight)
            {
                targetImageNumber = 0;
            }
            else if (!ToPressLeft && ToPressRight)
            {
                targetImageNumber = 1;

            }
            else if (!ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 2;

            }
            else if (ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 3;

            }

        }
        if (KeyToPress == 'W')
        {
            if (ToPressLeft && ToPressRight)
            {
                targetImageNumber = 4;

            }
            else if (!ToPressLeft && ToPressRight)
            {
                targetImageNumber = 5;

            }
            else if (!ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 6;

            }
            else if (ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 7;

            }

        }
        if (KeyToPress == 'E')
        {
            if (ToPressLeft && ToPressRight)
            {
                targetImageNumber = 8;

            }
            else if (!ToPressLeft && ToPressRight)
            {
                targetImageNumber = 9;

            }
            else if (!ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 10;

            }
            else if (ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 11;

            }

        }
        if (KeyToPress == 'R')
        {
            if (ToPressLeft && ToPressRight)
            {
                targetImageNumber = 12;

            }
            else if (!ToPressLeft && ToPressRight)
            {
                targetImageNumber = 13;

            }
            else if (!ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 14;

            }
            else if (ToPressLeft && !ToPressRight)
            {
                targetImageNumber = 15;

            }
        }
        rawImage.texture = imagesList[targetImageNumber];

    }

}
