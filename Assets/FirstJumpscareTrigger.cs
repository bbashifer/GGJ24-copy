using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstJumpscareTrigger : MonoBehaviour
{
    public Animator animator;
    private bool playedOnce = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && !playedOnce)
        {
            playedOnce = true;
            animator.SetTrigger("PlayAnim");
        }
    }
}
