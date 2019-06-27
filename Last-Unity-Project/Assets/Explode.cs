using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private ParticleSystem explosionParticle;
    private SphereCollider explosionCollider;
    
    // Start is called before the first frame update
    void Awake()
    {
        explosionParticle = GetComponent<ParticleSystem>();
        explosionParticle.Stop();
        explosionCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isExploding())
        {
            explosionCollider.radius += 2f;
            
        }

        else
        {
            explosionCollider.radius = .1f;
            gameObject.SetActive(false);
        }
        
    }

    public bool isExploding()
    {
        return explosionParticle.isPlaying;
    }

    void OnEnable()
    {
        explosionParticle.Play();
        
    }
}
