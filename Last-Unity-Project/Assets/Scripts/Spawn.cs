using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    FireballManagement fireballManager;
    Vector3 offsetPosition;
    Quaternion offsetRotation;
    Rigidbody rb;
    public Transform wandPivot;
    public float THRUST;
    public float OFFSET;
    Vector3 scale;
    float lifetime;
    bool beingShot;


    // Start is called before the first frame update

    void Start()
    {
        fireballManager = transform.parent.gameObject.GetComponent<FireballManagement>();
        scale = new Vector3(2, 2, 2);
       PositionInFrontOfWand();
       rb = GetComponent<Rigidbody>();
        
        Debug.Log("Calling Start");
    }

    public void Awake()
    {
        Debug.Log("Calling Awake");
        fireballManager = transform.parent.gameObject.GetComponent<FireballManagement>();
        scale = new Vector3(2, 2, 2);
        PositionInFrontOfWand();
        rb = GetComponent<Rigidbody>();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (beingShot)
        {
            lifetime -= 1 * Time.deltaTime;
            if (lifetime <= 0)
            {
                DestroyFireball();
            }
        }
       
    }

    public void SpawnFireball()
    {
        Debug.Log("Calling SpawnFireball");
        if (fireballManager == null)
        {
            fireballManager = transform.parent.gameObject.GetComponent<FireballManagement>();
        }
        lifetime = fireballManager.fireBallLifetime;
        gameObject.SetActive(true);
 
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        PositionInFrontOfWand();
        Debug.DrawRay(transform.position, transform.forward * THRUST);
        
        transform.localScale = scale;
        rb.AddForce(transform.forward * THRUST);
        beingShot = true;
       

    }

    public void DestroyFireball()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        fireballManager.Push(gameObject);
        gameObject.SetActive(false);
        transform.localScale = scale;
        beingShot = false;
    }

    public void PositionInFrontOfWand()
    {

        
        SetPosition(wandPivot.position + (wandPivot.forward * OFFSET));
        SetRotation(wandPivot.rotation);
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
        DestroyFireball();
    }
    public void DetachFromParent()
    {
        transform.parent = null;
        
    }
   
}
