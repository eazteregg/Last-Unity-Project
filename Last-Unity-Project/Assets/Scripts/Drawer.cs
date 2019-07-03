using UnityEngine;
using System.Collections;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Drawer : MonoBehaviour
{
    public Camera cam;
    public int SIZE;
    private Hand hand;
    public int radius;
    public Color color;
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