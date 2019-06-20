using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootVR : MonoBehaviour
{
    string Spell;
    public GameObject fireballManager;
    public GameObject lightningManager;
    private FireballManagement fireballManagement;
    

    // Start is called before the first frame update

    void Start()
    {
        fireballManagement = fireballManager.GetComponent<FireballManagement>();
        lightningManagement = lightningManagement.GetComponent<LightningManagement>();
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
        if (Input.GetButton("SwitchSpell"))
        {
            if (Spell == "Fireball")
            {
                Spell = "Lightning";
            }
            else if (Spell == "Lightning")
            {
                Spell = "Fireball";
            }
        }
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Fire1");

            if (Spell == "Fireball")
            {

                fireballManagement.SpawnFireball();
            }

            if (Spell == "Lightning")
            {
                lightningManagement.SpawnLightning();
            }
        }

    }
}

