using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRTK.SecondaryControllerGrabActions;

public class ShootVR : MonoBehaviour
{
    string Spell;
    public GameObject fireballManager;
    public GameObject lightningManager;
    private FireballManagement fireballManagement;
    private LightningManagement lightningManagement;
    [SerializeField] private SteamVR_Action_Boolean pinchAction;
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    public SteamVR_Input_Sources handType;

    // Start is called before the first frame update

    void Start()
    {
        fireballManagement = fireballManager.GetComponent<FireballManagement>();
        lightningManagement = lightningManager.GetComponent<LightningManagement>();
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
        if (grabAction.GetStateDown(handType))
        {
            Debug.Log("Switching spell...");
            if (Spell == "Fireball")
            {
                Spell = "Lightning";
            }
            else if (Spell == "Lightning")
            {
                Spell = "Fireball";
            }
        }

        if (pinchAction.GetStateUp(handType) && lightningManager.activeSelf)
        {
            lightningManagement.DeactivateLightning();
        }

    if (pinchAction.GetState(handType)) 
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
                lightningManagement.ActivateLightning();
            }
        }

    }
}

