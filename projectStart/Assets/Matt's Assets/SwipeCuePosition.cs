using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCuePosition : MonoBehaviour
{
    float playerHeight;
    float offset;
    float forearmLength;
    float shoulderDistance;
    Transform shoulder;
    Transform elbow;
    Transform wrist;
    //public Transform head;
    // Start is called before the first frame update
    void Start()
    {
        playerHeight = transform.parent.localPosition.y;
        offset = playerHeight * .25f;
        forearmLength = playerHeight * .156f;
        shoulderDistance = playerHeight / 8;
        shoulder = transform.GetChild(0);
        elbow = shoulder.transform.GetChild(0);
        wrist = shoulder.transform.GetChild(0);
        transform.localPosition = new Vector3(-shoulderDistance, 0, 0);
        elbow.transform.localPosition = new Vector3(0, 0, forearmLength);
        wrist.transform.localPosition = new Vector3(0, 0, forearmLength);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, -offset, 0);
        transform.eulerAngles = new Vector3(0, transform.parent.eulerAngles.y, transform.parent.eulerAngles.z);
    }
}
