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
    
    
    
    Stack<GameObject> fireballs;
    public float fireBallLifetime;
    public float fireBallsPerSecond;
    private float coolDown;
    GameObject origFireball;
    



    // Start is called before the first frame update
    
    
    
    void Start()
    {
        fireballs = new Stack<GameObject>();
        coolDown = 0.0f;
        GameObject origFireball = GetComponentInChildren<Spawn>(includeInactive:true).gameObject;
        int maxFireballs = Mathf.CeilToInt(fireBallLifetime * fireBallsPerSecond);
        Debug.Log(maxFireballs.ToString());
        fireballs.Push(origFireball);
        origFireball.SetActive(false);
        
        for (int i=1; i < maxFireballs; i++)
        {
            GameObject newFireball = Instantiate(origFireball, parent: transform);
            Spawn spawn = newFireball.GetComponent<Spawn>();
            spawn.SetFireballManager(transform);
            fireballs.Push(newFireball);
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

    public void Push(GameObject fireball)
    {
        fireballs.Push(fireball);
    }

    public void SpawnFireball()
    {
        
        
        Debug.Log(coolDown.ToString());
        
        if (coolDown <= 0)
        {
           
            GameObject fireball = fireballs.Pop();
            Spawn spawn = fireball.GetComponent<Spawn>();
            
            
            spawn.SpawnFireball();
            coolDown = 1 / fireBallsPerSecond;
        }
   
        
    }

    public GameObject SpawnAttachableFireball()
    {
        if (coolDown <= 0)
        {
           
            GameObject fireball = fireballs.Pop();
            fireball.GetComponent<Interactable>().enabled = true;
            Spawn spawn = fireball.GetComponent<Spawn>();
            spawn.SpawnAttachableFireball();
            coolDown = 1 / fireBallsPerSecond;

            return fireball;

        }

        return null;



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
