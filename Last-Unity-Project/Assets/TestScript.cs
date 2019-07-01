using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
