using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Pen : VRTK_InteractableObject
{
    public Painting painting;
    private RaycastHit touch;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        this.painting = GameObject.Find("Painting").GetComponent<Painting>();

    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 tip = transform.Find("Tip").transform.position;
        
        if (Physics.Raycast(tip, transform.up, out touch, distance))
        {
            if (!(touch.collider.tag == "Painting"))
                return;

            this.painting = touch.collider.GetComponent < Painting > ();
            Debug.Log("touching!");

            this.painting.SetColor(Color.black);
            this.painting.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
            this.painting.ToggleTouch(true);

        } else
        {
            this.painting.ToggleTouch(false);
        }
    }
}
