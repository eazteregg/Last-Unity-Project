using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FireballManagement : MonoBehaviour
{
    
#region Singleton

public static FireballManagement instance;

private void Awake()
{
    if (instance == null)
        instance = this;
}

#endregion
    
    
    
    Queue<GameObject> fireballs;
    public float fireBallLifetime;
    public float fireBallsPerSecond;
    private float coolDown;
    public GameObject origFireball;
    public GameObject hand;
    



    // Start is called before the first frame update
    
    
    
    void Start()
    {
        
        fireballs = new Queue<GameObject>();
        coolDown = 0.0f;
        int maxFireballs = Mathf.CeilToInt(fireBallLifetime * fireBallsPerSecond);
        Debug.Log(maxFireballs.ToString());
        
        
        
        for (int i=1; i < maxFireballs; i++)
        {
            GameObject newFireball = Instantiate(origFireball, parent: transform);
            fireballs.Enqueue(newFireball);
        }
        
    }

    private void Update()
    {
        coolDown -= 1 * Time.deltaTime;
        if (fireballs.Count == 0)
        {
            Debug.LogWarning("StackEmpty");
        }
    }

    public void Enqueue(GameObject fireball)
    {
        fireballs.Enqueue(fireball);
    }

    public void SpawnFireball()
    {
        
        
        Debug.Log(coolDown.ToString());
        
        if (coolDown <= 0)
        {

            GameObject fireball = fireballs.Dequeue();
            Spawn spawn = fireball.GetComponent<Spawn>();
            spawn.SpawnFireball(hand.transform);
            coolDown = fireBallsPerSecond;
        }
   
        
    }

    public GameObject SpawnAttachableFireball(Transform tf)
    {
        if (coolDown <= 0)
        {
           
            GameObject fireball = fireballs.Dequeue();
            fireball.GetComponent<Interactable>().enabled = true;
            Spawn spawn = fireball.GetComponent<Spawn>();
            spawn.SpawnAttachableFireball(tf);
            coolDown = 1 / fireBallsPerSecond;

            return fireball;

        }

        return null;


    
    }

    public float getCooldown()
    {
        return coolDown;
    }
    private void OnApplicationQuit()
    {
        foreach(GameObject fireball in fireballs)
        {
            if (fireball != origFireball)
            {
                Destroy(fireball);
            }
        }
    }
}
