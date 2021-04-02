using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InitiateSword : MonoBehaviour
{
    public Hand hand;
    public GameObject sword;
    int childCount;

    // Start is called before the first frame update
    void Start()
    {
        childCount = transform.childCount;
        StartCoroutine(WaitForEnable());
        
    }
    IEnumerator WaitForEnable()
    {
        while(transform.childCount == childCount)
        {
            yield return new WaitForEndOfFrame();
        }
        hand.AttachObject(sword, GrabTypes.None);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        hand.AttachObject(sword, GrabTypes.None);
    }

}
