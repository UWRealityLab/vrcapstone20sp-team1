using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public HashSet<string> destroyers;

    public GameObject otherDoor;
    public AudioSource audio;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        destroyers = new HashSet<string>();
        destroyers.Add("monsknlnter1");
        destroyers.Add("monsknlter2");
        destroyers.Add("monstlkler3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        
        Debug.Log("collision!!!!");
        if (collider.gameObject.tag == "monster1" || collider.gameObject.tag == "monster2" || collider.gameObject.tag == "monster3")
        {
            Debug.Log("door hit");
            //collider.isTrigger = false;
            audio.PlayOneShot(Resources.Load<AudioClip>("BreakingDoorDown"));
            this.GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce((this.transform.forward * 450), ForceMode.Impulse);
            GetComponent<Rigidbody>().useGravity = true;

            otherDoor.GetComponent<Rigidbody>().isKinematic = false;
            otherDoor.GetComponent<Rigidbody>().AddForce((otherDoor.transform.forward * 450), ForceMode.Impulse);
            otherDoor.GetComponent<Rigidbody>().useGravity = true;
        }
    }

}
