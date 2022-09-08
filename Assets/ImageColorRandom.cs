using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorRandom : MonoBehaviour
{
    public Gradient gradient;
    public float colorVelocity = 0.1f;
    private Image image;
    private float gradientTime = 0f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void FixedUpdate()
    {
        gradientTime += colorVelocity * Time.fixedDeltaTime;
        if(gradientTime > 1f) gradientTime = 0f;
        
        image.color = gradient.Evaluate(gradientTime);
    }
}
