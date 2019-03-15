using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using DG.Tweening;

public class ClockedRotate : MonoBehaviour
{
    public float Speed;
    public int NoteTrigger = 50, Channel = 1, CurrrentAngle = 0;

    public List<GameObject> ObjectsToEffect;

    public List<Vector3> SavedAngles;

    public AnimationCurve EaseCurve;

    private bool isCoolingDown = false;

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
        //Debug.Log("RotateOn: " + channel + "," + note + "," + velocity);


        if (Channel == (int)channel + 1 && !isCoolingDown)
        {
            isCoolingDown = true;
            StartCoroutine(Cooldown());


            if (NoteTrigger == note)
            {
                foreach (GameObject go in ObjectsToEffect)
                {
                    if (CurrrentAngle < SavedAngles.Count)
                    {
                        go.transform.DORotate(SavedAngles[CurrrentAngle], Speed).SetEase(EaseCurve);
                    }
                    else
                    {
                        CurrrentAngle = 0;
                        go.transform.DORotate(SavedAngles[CurrrentAngle], Speed).SetEase(EaseCurve);
                    }
                }
                CurrrentAngle++;
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return null;
        isCoolingDown = false;
    }
}
