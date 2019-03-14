using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class Pixelation : MonoBehaviour
{

    public float PixelationVal = 2048;
    public int KnobNumber = 3;
    public Material PixelMat;

    private float pixelationOrigin;
    // Start is called before the first frame update
    void Start()
    {
        pixelationOrigin = PixelationVal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void Knob(MidiChannel channel, int knobNumber, float knobValue)
    {
        if (KnobNumber == knobNumber)
        {
            PixelationVal = knobValue * pixelationOrigin;

            if (knobValue < .9f)
                PixelationVal = PixelationVal / 6;

            if(knobValue == 0)
            {
                PixelationVal = 2;
            }

            PixelMat.SetFloat("_x", PixelationVal);
            PixelMat.SetFloat("_y", PixelationVal);
        }
    }

    void OnEnable()
    {
        MidiMaster.knobDelegate += Knob;
    }

    void OnDisable()
    {
        MidiMaster.knobDelegate -= Knob;
    }
}
