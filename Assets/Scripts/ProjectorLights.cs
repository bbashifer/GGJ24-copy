using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectorLights : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Material[] mats;
    private int rng;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(MatAnimation());
    }

    private IEnumerator MatAnimation()
    {
        while (true)
        {
            rng = Random.Range(0, 2);
            mesh.material = mats[rng];
            yield return new WaitForSeconds(.05f);
        }
        
    }
}
