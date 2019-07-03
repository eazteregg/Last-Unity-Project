using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PaintingReset : MonoBehaviour
{

    private Texture2D texture;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        this.texture = Instantiate(renderer.material.mainTexture) as Texture2D;
        renderer.material.mainTexture = this.texture;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
