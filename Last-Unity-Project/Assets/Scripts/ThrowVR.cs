
using System;
using System.Collections;
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
    private string[] spells;
    private int currentSpell;
    public int size;
    public int radius;
    public Color color;
    // Start is called before the first frame update

    void Start()
    {
        spells = new string[3];
        spells[0] = "Fireball";
        spells[1] = "Lightning";
        spells[2] = "Drawing";
        
        currentSpell = 0;
        
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
                currentSpell += 1;
                Spell = spells[currentSpell % 3];
                
            }

            if (hand.grabPinchAction.GetStateUp(handType))
            {
                if (Spell == "Lightning" && LightningManagement.instance.LightningActive())
                {
                    LightningManagement.instance.DeactivateLightning();
                    
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
                else if (Spell == "Drawing")
                {    
                    
                    RaycastHit hit;
                    Debug.DrawRay(transform.position, transform.forward, Color.red);
                    if (!Physics.Raycast(transform.position, transform.forward, out hit, 50))
                        return;

                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    MeshCollider meshCollider = hit.collider as MeshCollider;
                    
                    if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
                        return;

                    Debug.Log("passed text");

                    Texture2D tex = rend.material.mainTexture as Texture2D;
                    Vector2 pixelUV = hit.textureCoord;

                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    Debug.Log("passed returns");

                    float rSquared = radius * radius;
                    int x = (int)pixelUV.x;
                    int y = (int)pixelUV.y;

                    for (int u = x - radius; u < x + radius + 1; u++)
                    {
                        for (int v = y - radius; v < y + radius + 1; v++)
                        {
                            if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                                tex.SetPixel(u, v, color);
                        }
                    }
                    tex.Apply();
                }
            }
        }
       // }

}