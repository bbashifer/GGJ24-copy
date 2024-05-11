using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public GameObject endgameCloset, endgameTrigger, reaperSmoke;

    public void TurnOnCloset()
    {
        endgameCloset.SetActive(true);
        Invoke(nameof(EndgameTrigger), 7f);
        Invoke(nameof(ReaperSmoke), 4f);
    }

    private void EndgameTrigger()
    {
        endgameTrigger.SetActive(true);
    }

    private void ReaperSmoke()
    {
        reaperSmoke.SetActive(true);
    }
}
