using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using DG.Tweening;

public class ClockedRotate : MonoBehaviour
{
    public float Speed;
    public int NoteTrigger = 50, Channel = 1, CurrrentAngle = 0;

    public List<Vector3> SavedAngles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
    }
    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        Debug.Log("RotateOn: " + channel + "," + note + "," + velocity);

        if (Channel == (int)channel + 1)
        {
            if (NoteTrigger == note)
            {
                if (CurrrentAngle < SavedAngles.Count)
                {
                    transform.DORotate(SavedAngles[CurrrentAngle], Speed).SetEase(Ease.OutFlash);
                    CurrrentAngle++;
                }
                else
                {
                    CurrrentAngle = 0;
                }
            }
        }
    }
}
