using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerDrift : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 startRotation;
    float randomOffset;
    float maxHorizontalDrift;
    float maxVerticalDrift = .01f;
    float x;
    float y;
    float z;
    float rX;
    float rY;
    float rZ;
    float maxRollAngle;
    float driftPeriod;
    float rollPeriod;
    float risePeriod = 3;
    float spinSpeed; // deg/s
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        randomOffset = Random.Range(0, 180);
        spinSpeed = Random.Range(3, 7);
    }

    // Update is called once per frame
    void Update()
    {
        y = maxVerticalDrift * Mathf.Sin((360 * Time.time/risePeriod + randomOffset) * Mathf.PI / 180);
        rY += spinSpeed * Time.deltaTime;
        transform.position = startPosition + new Vector3(x, y, z);
        transform.eulerAngles = startRotation + new Vector3(rX, rY, rZ);
    }
}
