using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareEvent : MonoBehaviour
{
    public AudioClip fartSound;
    public AudioSource audioSource;

    public ParticleSystem fartSystem;

    public void PlayFartSound()
    {
        audioSource.PlayOneShot(fartSound);
        fartSystem.Play();
    }
}
