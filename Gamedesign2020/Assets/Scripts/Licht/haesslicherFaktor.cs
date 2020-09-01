﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haesslicherFaktor : MonoBehaviour
{
    public float eigentlicheIntensity;
    public float cryFactor = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        eigentlicheIntensity=this.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = eigentlicheIntensity * cryFactor;
    }
}