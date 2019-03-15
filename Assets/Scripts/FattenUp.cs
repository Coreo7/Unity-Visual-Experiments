using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using DG.Tweening;


public class FattenUp : MonoBehaviour
{
    public bool ShouldFollowMaster = false, ShouldFollowClock = false;
    public float FatJuice = 3, Speed = .2f;
    public int NoteTrigger = 48, ClockTrigger = -1, Channel = 1;
    public List<GameObject> SkinnyBois;

    private float StartScale, noteTimer = 0, OriginalFat, OriginalSpeed;
    private MasterValues masterValues;
    private bool shouldTime = false;

    private const float TOGGLE_TIME = 2;

    private void OnEnable()
    {
        MidiMaster.noteOffDelegate += NoteOff;
        MidiMaster.noteOnDelegate += NoteOn;
        StartScale = transform.localScale.x;
        OriginalFat = FatJuice;
        OriginalSpeed = Speed;
        masterValues = gameObject.GetComponent<MasterValues>();
    }
    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
        MidiMaster.noteOffDelegate -= NoteOff;
    }
    // Start is called before the first frame update
    void Start()
    {
        print(StartScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFollowMaster)
        {
            FatJuice = OriginalFat * masterValues.MasterIntensity;
            Speed = OriginalSpeed * masterValues.MasterSpeed;
        }

        if (shouldTime)
        {
            Debug.Log(noteTimer);
            noteTimer += Time.deltaTime;
        }

        if(noteTimer > TOGGLE_TIME)
        {
            shouldTime = false;
            ShouldFollowClock = !ShouldFollowClock;
            noteTimer = 0;
        }
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        if (Channel == (int)channel + 1)
        {
            if (NoteTrigger == note)
            {
                foreach (GameObject go in SkinnyBois)
                {
                    go.transform.DOScale(FatJuice * velocity, Speed).SetEase(Ease.OutFlash);
                    StartCoroutine(goBack(go));
                }

                //Start counting time until note off is triggered
                shouldTime = true;
            }

            if (ShouldFollowClock && ClockTrigger == note)
            {
                foreach (GameObject go in SkinnyBois)
                {
                    go.transform.DOScale(FatJuice * velocity, Speed).SetEase(Ease.OutFlash);
                    StartCoroutine(goBack(go));
                }
            }
        }
    }

    void NoteOff(MidiChannel channel, int note)
    {
        if (NoteTrigger == note)
        {
            noteTimer = 0;
            shouldTime = false;
         }
    }

    IEnumerator goBack(GameObject go)
    {
        yield return new WaitForSeconds(Speed / 2);
        go.transform.DOScale(StartScale, Speed).SetEase(Ease.OutFlash);
    }
}
