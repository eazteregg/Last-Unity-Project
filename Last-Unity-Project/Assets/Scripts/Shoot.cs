using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    string Spell;
    public Camera camera;
    public GameObject fireballManager;
    private FireballManagement fireballManagement;

    // Start is called before the first frame update

    void Start()
    {
        fireballManagement = fireballManager.GetComponent<FireballManagement>();
        
        Spell = "Fireball";
        Transform[] spells = GetComponentsInChildren<Transform>();
        foreach (Transform t in spells)
        {
            if (t != transform)
            {
                t.gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        bool hitSomething = Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit);
        if (hitSomething)
        {
            transform.parent.transform.LookAt(hit.point);
        }

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Fire1");

            if (Spell == "Fireball")
            {
                
                fireballManagement.SpawnFireball();
            }
        }
        
    }
}
