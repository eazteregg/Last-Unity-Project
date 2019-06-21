using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ShootVR : MonoBehaviour
{
    string Spell;
    public GameObject fireballManager;
    public GameObject lightningManager;
    private FireballManagement fireballManagement;
    public SteamVR_Action_Boolean pinchAction;
    public SteamVR_Input_Sources handType;

    // Start is called before the first frame update

    void Start()
    {
        fireballManagement = fireballManager.GetComponent<FireballManagement>();
        //lightningManagement = lightningManagement.GetComponent<LightningManagement>();
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
        if (false)
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
        if (pinchAction.GetStateDown(handType))
        {
            Debug.Log("Fire1");
            //Debug.Log(Spell);
            if (Spell == "Fireball")
            {
                Debug.Log("Spawning Fireball!");
                fireballManagement.SpawnFireball();
            }

            if (Spell == "Lightning")
            {
                //lightningManagement.SpawnLightning();
            }
        }

    }
}

