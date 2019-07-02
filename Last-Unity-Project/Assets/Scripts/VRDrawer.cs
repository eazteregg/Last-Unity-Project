using UnityEngine;
using System.Collections;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRDrawer : MonoBehaviour
{
    public Camera cam;
    public int SIZE;
    private Hand hand;
    
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        Debug.Log(rend.sharedMaterial);
        Debug.Log(rend.sharedMaterial.mainTexture);
        Debug.Log(meshCollider);



        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Debug.Log("passed text");

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;

        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        Debug.Log("passed returns");

        //Expand where to draw on both direction
        for (int i = 0; i < SIZE; i++)
        {
            int x = (int)pixelUV.x;
            int y = (int)pixelUV.y;

            //Increment the X and Y
            x += i;
            y += i;

            //Apply
            tex.SetPixel(x, y, Color.red);
            Debug.Log("in for");
            //De-increment the X and Y
            x = (int)pixelUV.x;
            y = (int)pixelUV.y;

            x -= i;
            y -= i;

            //Apply
            tex.SetPixel(x, y, Color.red);
        }
        tex.Apply();
    }
}