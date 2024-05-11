using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReaperTrigger : MonoBehaviour
{
    public int SceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("LoadScene 1");

            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
