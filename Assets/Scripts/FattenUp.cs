using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using DG.Tweening;


public class FattenUp : MonoBehaviour
{
    public bool ShouldFollowMaster = false;
    public float FatJuice = 3, Speed = .2f;
    public int NoteTrigger = 48, Channel = 1;
    public List<GameObject> SkinnyBois;

    private float StartScale;
    private MasterValues masterValues;

    private void OnEnable()
    {
        MidiMaster.noteOnDelegate += NoteOn;
        StartScale = transform.localScale.x;
        masterValues = gameObject.GetComponent<MasterValues>();
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
        if (ShouldFollowMaster)
        {
            FatJuice = FatJuice * masterValues.MasterIntensity;
            Speed = Speed * masterValues.MasterSpeed;
        }
    }

    void NoteOn(MidiChannel channel, int note, float velocity)
    {
        Debug.Log("NoteOn: " + channel + "," + note + "," + velocity);

        if (Channel == (int)channel + 1)
        {
            if (NoteTrigger == note)
            {
                foreach (GameObject go in SkinnyBois)
                {
                    go.transform.DOScale(FatJuice * velocity, Speed).SetEase(Ease.OutFlash);
                    StartCoroutine(goBack(go));
                }
            }
        }
    }

    IEnumerator goBack(GameObject go)
    {
        yield return new WaitForSeconds(Speed / 2);
        go.transform.DOScale(StartScale, Speed).SetEase(Ease.OutFlash);
    }
}
