using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class Testing2 : MonoBehaviour
{
    public Hand hand;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
       
    }

    // Update is called once per frame
    void Update()
    {
        if (hand != null)
        {
            //hand.ShowController(true);
            //hand.transform.Find("ControllerButtonHints").GetComponent<ControllerButtonHints>()
            
        }
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3);
        //transform.localPosition = new Vector3(0, 0, 0);
        ControllerButtonHints.ShowButtonHint(hand, hand.uiInteractAction);
    }
}
