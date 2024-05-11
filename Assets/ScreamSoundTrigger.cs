using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamSoundTrigger : MonoBehaviour
{
    public AudioClip SoundToPlay;
    public AudioSource audioSource;

    private bool playedOnce = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && !playedOnce)
        {
            audioSource.PlayOneShot(SoundToPlay);
            playedOnce = true;
        }
    }
}
