
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ThrowVR : MonoBehaviour
{
    string Spell;
//    public GameObject fireballManager;
//    public GameObject lightningManager;
    private FireballManagement fireballManagement;
    private LightningManagement lightningManagement;
    [SerializeField] private SteamVR_Action_Boolean pinchAction;
    [SerializeField] private SteamVR_Action_Boolean grabAction;
    public SteamVR_Input_Sources handType;
    private Hand hand;
    private bool fireballAttached;
    private GameObject attachedFireball;

    // Start is called before the first frame update

    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
//        fireballManagement = FireballManagement.instance;
//        fireballManagement = fireballManager.GetComponent<FireballManagement>();
//        lightningManagement = lightningManager.GetComponent<LightningManagement>();
        Spell = "Fireball";
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (hand != null)
        {
        //Debug.Log("Hand: " + hand);
        //Debug.Log("Hand.grabGripAction: " + hand.grabGripAction);
            if (hand.grabGripAction.GetStateDown(handType))
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

            if (hand.grabPinchAction.GetStateUp(handType))
            {
                if (Spell == "Lightning" && LightningManagement.instance.LightningActive())
                {
                    LightningManagement.instance.DeactivateLightning();
                }
            }

            if (hand.grabPinchAction.GetState(handType))
            {
                //Debug.Log("Fire1");
                //Debug.Log(Spell);
                if (Spell == "Fireball" && !hand.ObjectIsAttached(attachedFireball))
                {
                    Debug.Log("Spawning Fireball!");

                    attachedFireball = FireballManagement.instance.SpawnAttachableFireball();
                    if (attachedFireball != null)
                        hand.AttachObject(attachedFireball, GrabTypes.Pinch);
                }

                if (Spell == "Lightning")
                {
                    LightningManagement.instance.ActivateLightning();
                }
            }
        }
        }

}