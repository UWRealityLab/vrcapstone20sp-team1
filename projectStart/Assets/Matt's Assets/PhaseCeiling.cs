using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseCeiling : MonoBehaviour
{
    public MeshCollider ceiling;
    //public MeshRenderer ceiling;
    float offTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        ceiling.enabled = false;
        //other.GetComponent<Collider>().enabled = false;
        //StartCoroutine(DisableCeiling(offTime));
    }
    private void OnTriggerExit(Collider other)
    {
        ceiling.enabled = true;
        //other.GetComponent<Collider>().enabled = true;
    }
    IEnumerator DisableCeiling(float time)
    {
        
        yield return new WaitForSeconds(time);
        ceiling.enabled = true;
        
    }
}
