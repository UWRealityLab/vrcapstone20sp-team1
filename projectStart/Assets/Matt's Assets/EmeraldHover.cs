using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmeraldHover : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 position;
    public float amplitude = .01f;
    public float frequency = .2f;
    float time;
    // Start is called before the first frame update
    void Awake()
    {
        startPosition = transform.position;
    }

    // Update is called once per framess
    void Update()
    {
        time += Time.deltaTime;
        transform.position = startPosition + new Vector3(0, 1, 0) * amplitude * Mathf.Sin(360 * frequency * time * Mathf.PI / 180);
    }
}
