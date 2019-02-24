using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OffsetY : MonoBehaviour
{

    public Transform TargetTransform;
    public float Delay = .5f;

    private Transform myTransform;
    private float offset;
    // Start is called before the first frame update

    void Start()
    {
        myTransform = GetComponent<Transform>();

        offset = TargetTransform.position.z - myTransform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
       // if (TargetTransform.position.z - myTransform.position.z < 2 || TargetTransform.position.z - myTransform.position.z > 8)
      //  {
            //print("moving");
            myTransform.DOMoveZ(TargetTransform.position.z - offset, Delay).SetEase(Ease.InOutSine); //= new Vector3(myTransform.position.x, myTransform.position.y, TargetTransform.position.z - offset);
       // }
    }
}
