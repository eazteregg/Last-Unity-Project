using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
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
    public float colliderGrowth;

    // Start is called before the first frame update


    //OB
    public AudioSource m_AudioSource;
    public AudioClip m_Charge;
    public AudioClip m_TravelFire;
    public AudioClip m_explotion;
    private bool is_traveling;
    private bool is_Attached;
    private bool is_exploted;
    public float explosiveForce;
    public float explosionRadius;

    void Awake()
    {
        fireballManager = transform.parent;

        scale = new Vector3(.5f, .5f, .5f);

        rb = gameObject.GetComponent<Rigidbody>();

        fireballParticle = transform.GetChild(0).GetComponent<ParticleSystem>();

        fireballTrails = transform.GetChild(1).GetComponent<ParticleSystem>();

        explosionParticle = transform.GetChild(2).GetComponent<ParticleSystem>();

        fireballParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        fireballTrails.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        explosionParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        fireballCollider = GetComponent<CapsuleCollider>();

        //explosionCollider = GetComponent<SphereCollider>();

        //explosionCollider.enabled = false;

        fireballCollider.enabled = false;

        exploding = false;

        beingShot = false;

        rb.useGravity = false;


        //OB
        is_traveling = false;
        is_Attached = false;
        is_exploted = false;
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //OB
        if (is_Attached)
        {
            m_AudioSource.PlayOneShot(m_Charge);


            is_Attached = false;
        }


        explosionParticle.gameObject.SetActive(true);
        if (beingShot)
        {
            //OB 
            if (is_traveling)
            {
                m_AudioSource.PlayOneShot(m_TravelFire);
                is_traveling = false;
                is_Attached = false;
            }

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
            //Debug.Log(explosionParticle.IsAlive());

            if (!explosionParticle.IsAlive())
            {
                ResetFireball();
            }

            //explosionCollider.radius += 1f * Time.deltaTime;
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
        //PositionInFrontOfWand(tf);

        transform.localScale = scale;

        //OB
        is_Attached = true;
    }


    public void SpawnFireball(Transform tf)
    {
        Debug.Log("Calling SpawnFireball");

        lifetime = FireballManagement.instance.fireBallLifetime;
        gameObject.SetActive(true);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //PositionInFrontOfWand(tf);
        Debug.DrawRay(transform.position, transform.forward * THRUST);

        transform.localScale = scale;
        rb.AddForce(transform.forward * THRUST);
        beingShot = true;

        //OB
        m_AudioSource.PlayOneShot(m_Charge);
    }

    public void Explode()
    {
        Debug.Log("exploding");

        rb.velocity = Vector3.zero;

        rb.angularVelocity = Vector3.zero;

        rb.useGravity = false;

        Debug.Log("stopping Particles");

        fireballParticle.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        fireballTrails.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        beingShot = false;
        exploding = true;

        //OB
        is_exploted = true;

        explosionParticle.Simulate(0f, false, true);
        //explosionParticle.gameObject.SetActive(true);
        explosionParticle.Play();


        if (is_exploted)
        {
            m_AudioSource.Stop();
            m_AudioSource.PlayOneShot(m_explotion);
            is_exploted = false;
        }


        Debug.Log("playing explosion:" + explosionParticle.isPlaying);
        fireballCollider.enabled = false;
        // explosionCollider.enabled = true;
    }


    void ResetFireball()
    {
        explosionParticle.Clear();
        explosionParticle.Stop();

        beingShot = false;
        exploding = false;
        explosionCollider.radius = .04f;
        fireballCollider.enabled = false;
        explosionCollider.enabled = false;
        FireballManagement.instance.Enqueue(gameObject);
        //transform.localScale = scale;
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
        GameObject other = collision.gameObject;


        if (!other.CompareTag("Player") && !other.CompareTag("Spell"))
        {
            Debug.Log("Collision: " + collision.gameObject);

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider collider in colliders)
            {
                if (!collider.gameObject.CompareTag("Player") && !collider.gameObject.CompareTag("Spell"))
                {
                    Debug.Log("Exploding: " + collider.gameObject);
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    rb.AddExplosionForce(explosiveForce, other.transform.position, explosionRadius, 3f);
                }
            }

            Explode();
        }
    }

    public void SetBeingShot()
    {
        rb.useGravity = true;
        fireballCollider.enabled = true;

        //Debug.Log("Being Shot");
        beingShot = true;

        //OB


        is_traveling = true;
    }

    public void CheckIfActive()
    {
        Debug.Log("isActive:" + explosionParticle.gameObject.activeSelf);
    }
}