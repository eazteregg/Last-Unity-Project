using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Spawn : MonoBehaviour
{
    FireballManagement fireballManagement;
    Vector3 offsetPosition;
    Quaternion offsetRotation;
    Rigidbody rb;
    public Transform wandPivot;
    public float THRUST;
    public float OFFSET;
    Vector3 scale;
    float lifetime;
    bool beingShot;
    private Transform fireballManager;


    // Start is called before the first frame update

    void Start()
    {
        fireballManager = transform.parent;
        fireballManagement = transform.parent.gameObject.GetComponent<FireballManagement>();
        scale = new Vector3(2, 2, 2);
       PositionInFrontOfWand();
       rb = GetComponent<Rigidbody>();
        
        Debug.Log("Calling Start");
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
                DestroyFireball();
            }
        }

       

    }

    public void SpawnAttachableFireball()
    {
        Debug.Log("Calling SpawnFireball");
        if (fireballManagement == null)
        {
            fireballManagement = transform.parent.gameObject.GetComponent<FireballManagement>();
        }
        lifetime = fireballManagement.fireBallLifetime;
        gameObject.SetActive(true);
 
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        PositionInFrontOfWand();
        
        transform.localScale = scale;
        
    }

    public void SpawnFireball()
    {
        Debug.Log("Calling SpawnFireball");
        if (fireballManagement == null)
        {
            fireballManagement = transform.parent.gameObject.GetComponent<FireballManagement>();
        }
        lifetime = fireballManagement.fireBallLifetime;
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
        fireballManagement.Push(gameObject);
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
        Debug.Log("Collision!");
        DestroyFireball();
    }
    
    public void DetachFromParent()
    {
        transform.parent = null;
        
    }

    public void SetFireballManager(Transform go)
    {
        fireballManager = go;
    }
    public void SetBeingShot()
    {
        transform.parent = fireballManager;
        Debug.Log("Being Shot");
        beingShot = true;
    }
   
}
