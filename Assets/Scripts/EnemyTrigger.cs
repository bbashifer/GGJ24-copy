using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyTrigger : MonoBehaviour
{
    public bool EnemyInRange;

    [SerializeField]
    private float timer;
    [SerializeField]
    private float secondsStuck;
    private EnemyMovement enemyMovement;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        EnemyInRange = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= secondsStuck && EnemyInRange)//Timer to generate new sequence 
        {
            navMeshAgent.enabled = true;
            enemyMovement.enabled = true;
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("trigger");

            timer = 0;
            EnemyInRange = true;
            enemyMovement = other.GetComponent<EnemyMovement>();
            enemyMovement.enabled = false;
            navMeshAgent = other.GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyInRange = false;
    }
}
