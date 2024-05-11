using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyKillTrigger : MonoBehaviour
{
    public Darkener Darkener;
    public int SceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Darkener.StartFadeOut();
            Invoke("LoadSceneMode", 1.1f);

        }
    }

    private void LoadSceneMode()
    {
        SceneManager.LoadScene(SceneToLoad);

    }
}
