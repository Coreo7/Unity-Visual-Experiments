using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using DG.Tweening;


public class FattenUp : MonoBehaviour
{
    public float FatJuice = 3, speed = .2f;
    [SerializeField]
    public int NoteTrigger = 48, Channel = 1;

    private float StartScale;


    private void OnEnable()
    {
    //    MidiMaster.noteOnDelegate += NoteOn;
        StartScale = transform.localScale.x;
    }
    private void OnDisable()
    {
        MidiMaster.noteOnDelegate -= NoteOn;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        Debug.Log("NoteOn: " + channel + "," + note + "," + velocity);

        if (Channel == (int)channel + 1)
        {
            if (NoteTrigger == note)
            {
                print("FOURTY EIGHT");
                transform.DOScale(FatJuice * velocity, speed).SetEase(Ease.OutFlash);
                StartCoroutine(goBack());
            }
        }
    }

    IEnumerator goBack()
    {
        yield return new WaitForSeconds(speed / 2);
        transform.DOScale(StartScale, speed).SetEase(Ease.OutFlash);
    }
}
