using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigSpider : MonoBehaviour
{

    public GameObject TheDestination;
    NavMeshAgent TheAgent;

    void Start()
    {
        TheAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        TheAgent.SetDestination(TheDestination.transform.position);
    }
}

  