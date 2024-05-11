using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CaughtScreen1 : MonoBehaviour
{
    private bool inputEnabled = false;
    [SerializeField] private GameObject textprompt;

    void Start()
    {
        Invoke("EnableInput", 2f);
    }

    void EnableInput()
    {
        inputEnabled = true;
        textprompt.SetActive(true);
        Debug.Log("Input enabled! Press any key to load the scene.");
    }

    void Update()
    {
        if (inputEnabled)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
