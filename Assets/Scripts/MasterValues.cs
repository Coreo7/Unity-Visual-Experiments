using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;


public class MasterValues : MonoBehaviour
{
    public float MasterIntensity = 1, IntensityMultiplier = 1;
    public int MasterIntensityKnob;

    public float MasterSpeed = 1, SpeedMultiplier = 1;
    public int MasterSpeedKnob;

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
      //  Debug.Log("NoteOn: " + channel + "," + note + "," + velocity);
    }

    void NoteOff(MidiChannel channel, int note)
    {
       // Debug.Log("NoteOff: " + channel + "," + note);
    }

    void Knob(MidiChannel channel, int knobNumber, float knobValue)
    {
        if(MasterSpeedKnob == knobNumber)
        {
            MasterSpeed = knobValue * SpeedMultiplier +.01f;
        }
        
        if(MasterIntensityKnob == knobNumber)
        {
            MasterIntensity = knobValue * IntensityMultiplier + .01f;
        }
    }

    void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.knobDelegate += Knob;
    }

    void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        MidiMaster.noteOffDelegate -= NoteOff;
        MidiMaster.knobDelegate -= Knob;
    }
}
