using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Spawn : MonoBehaviour
{
    //FireballManagement fireballManagement;
    private Vector3 offsetPosition;
    private Quaternion offsetRotation;
    private Rigidbody rb;
    private Hand hand;
    public float THRUST;
    public float OFFSET;
    private Vector3 scale;
    private float lifetime;
    private bool beingShot;
    private Transform fireballManager;
    private ParticleSystem fireballParticle;
    private ParticleSystem fireballTrails;
    private ParticleSystem explosionParticle;
    private bool exploding;
    private CapsuleCollider fireballCollider;
    private SphereCollider explosionCollider;

    // Start is called before the first frame update

    void Awake()
    {
        Debug.Log("Calling Awake");
        fireballManager = transform.parent;
        //fireballManagement = transform.parent.gameObject.GetComponent<FireballManagement>();
        scale = new Vector3(2, 2, 2);
        rb = gameObject.GetComponent<Rigidbody>();
        
        fireballParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        
        fireballTrails = transform.GetChild(1).GetComponent<ParticleSystem>();
        
        explosionParticle = transform.GetChild(2).GetComponent<ParticleSystem>();
        
        fireballParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        fireballTrails.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        explosionParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        fireballCollider = GetComponent<CapsuleCollider>();
        
        explosionCollider = GetComponent<SphereCollider>();
        
        explosionCollider.enabled = false;
        
        fireballCollider.enabled = false;
        exploding = false;
        beingShot = false;
        rb.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (beingShot)
        {
            lifetime -= 1 * Time.deltaTime;
            Vector3 target = rb.velocity.normalized;
            transform.forward = target;
            if (lifetime <= 0)
            {
               Explode();
            }
        }

        if (exploding)
        {
            if (!explosionParticle.isPlaying)
            {
                ResetFireball();
            }

            explosionCollider.radius += 2f;
        }
    }

    public void SpawnAttachableFireball(Transform tf)
    {
       // Debug.Log("Calling SpawnFireball");
        Debug.Log("FireballParticle:" + fireballParticle);

        lifetime = FireballManagement.instance.fireBallLifetime;
        fireballParticle.Play();
        fireballTrails.Play();

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        PositionInFrontOfWand(tf);

        transform.localScale = scale;
        
    }
    

    public void SpawnFireball(Transform tf)
    {
        Debug.Log("Calling SpawnFireball");

        lifetime = FireballManagement.instance.fireBallLifetime;
        gameObject.SetActive(true);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        PositionInFrontOfWand(tf);
        Debug.DrawRay(transform.position, transform.forward * THRUST);

        transform.localScale = scale;
        rb.AddForce(transform.forward * THRUST);
        beingShot = true;
    }

    public void Explode()
    {
        
        Debug.Log("exploding");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        Debug.Log("stopping Particles");
        fireballParticle.Stop(false,ParticleSystemStopBehavior.StopEmittingAndClear);
        fireballTrails.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        beingShot = false;
        exploding = true;
        explosionParticle.Clear();
        explosionParticle.gameObject.SetActive(true);
        explosionParticle.Play();
        Debug.Log("playing explosion:" + explosionParticle.isPlaying);
        fireballCollider.enabled = false;
        explosionCollider.enabled = true;


    }

  

    void ResetFireball()
    {
        rb.useGravity = false;
        beingShot = false;
        exploding = false;
        explosionCollider.radius = .1f;
        fireballCollider.enabled = false;
        explosionCollider.enabled = false;
        FireballManagement.instance.Push(gameObject);
        transform.localScale = scale;
    }

    public void PositionInFrontOfWand(Transform hand)
    {
        SetPosition(hand.position + (hand.forward * OFFSET));
        SetRotation(hand.rotation);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetLocalPosition(Vector3 localPosition)
    {
        transform.localPosition = localPosition;
    }

    public void SetLocalRotation(Quaternion localRotation)
    {
        transform.localRotation = localRotation;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
        Explode();
    }

    public void SetFireballManager(Transform go)
    {
        fireballManager = go;
    }

    public void SetBeingShot()
    {
        rb.useGravity = true;
        fireballCollider.enabled = true;
        transform.parent = fireballManager;
        //Debug.Log("Being Shot");
        beingShot = true;
    }
}