using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [SerializeField]
    public Animator DoorAnimator;

    public AudioSource audioSource;
    public AudioClip DoorClose;

    private bool doorClosed;

    private void Start()
    {
        doorClosed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !doorClosed)
        {
            doorClosed = true;
            DoorAnimator.SetTrigger("Close");
            audioSource.PlayOneShot(DoorClose);
        }
    }
}
