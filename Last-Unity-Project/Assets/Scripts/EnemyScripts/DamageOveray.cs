﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOveray : MonoBehaviour
{
    public Image overlayImage;

    private float r;
    private float g;
    private float b;
    private float a;

    // Start is called before the first frame update
    void Start()
    {
        r = overlayImage.color.r;
        g = overlayImage.color.g;
        b = overlayImage.color.b;
        a = overlayImage.color.a;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            a -= 0.01f;
        }

        if (Input.GetKey(KeyCode.T))
        {
            a += 0.01f;

        }

        a = Mathf.Clamp(a,0,1f);
        AdjustColor();

    }

    private void AdjustColor()
    {
        Color c = new Color(r,g,b,a);
        overlayImage.color = c;
    }
}
