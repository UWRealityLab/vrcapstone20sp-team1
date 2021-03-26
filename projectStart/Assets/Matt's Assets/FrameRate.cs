using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetFrameRate());
    }
    IEnumerator GetFrameRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            Debug.Log(1 / Time.deltaTime);
        }
    }

  
}
