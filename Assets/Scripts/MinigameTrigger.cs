using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public bool PlayerInRange;
    public GameObject PressE;
    private void Start()
    {
        PlayerInRange = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PressE.SetActive(true);
            PlayerInRange = true;
            Debug.Log("Entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PressE.SetActive(false);
            PlayerInRange = false;
            Debug.Log("Exited");

        }
    }
}
