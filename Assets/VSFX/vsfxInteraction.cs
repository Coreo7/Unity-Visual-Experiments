using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class vsfxInteraction : MonoBehaviour
{
    public float SpawnAmount = 9999;

    public VisualEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            effect.SetFloat("_myFloat", SpawnAmount);
        }
    }
}
