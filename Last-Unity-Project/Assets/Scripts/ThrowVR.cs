
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
        attachedFireball = null;


    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("Holding: " + hand.currentAttachedObject);
       // if (hand != null)
       // {
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

                if (Spell == "Fireball" && attachedFireball)
                {
                    Debug.Log("Release - Attached object: " + hand.currentAttachedObject + "    attached Fireball " +attachedFireball);
                    attachedFireball = null;
                    //hand.DetachObject(attachedFireball, true);

                }
            }

            if (hand.grabPinchAction.GetStateDown(handType))
            {
                //Debug.Log(attachedFireball);
                if (Spell == "Fireball" && !hand.ObjectIsAttached(attachedFireball) && FireballManagement.instance.getCooldown() <= 0)
                {
                    
                    
                    attachedFireball = FireballManagement.instance.SpawnAttachableFireball(hand.transform);
                    Debug.Log("Press - Attached object" + attachedFireball);
                    if (attachedFireball)
                        hand.AttachObject(attachedFireball, GrabTypes.Pinch);
                }
            }

            if (hand.grabPinchAction.GetState(handType))
            {
               
                

                if (Spell == "Lightning")
                {
                    LightningManagement.instance.ActivateLightning();
                }
            }
        }
       // }

}