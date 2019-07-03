using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    private GameObject origVersion;
    private Transform pivot;
    
    public void Start()
    {
        pivot = transform.parent;
        destroyedVersion.SetActive(false);
        destroyedVersion = Instantiate(destroyedVersion, pivot.position, Quaternion.identity);
        
    }
    private void OnCollisionEnter(Collision other)
   
    
    {
        
        if (other.gameObject.CompareTag("Spell"))
        {
            Debug.Log("Deactivating!!");
            gameObject.SetActive(false);
            destroyedVersion.SetActive(true);
            
        }
    }
}
