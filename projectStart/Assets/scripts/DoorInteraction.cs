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
    public int seconds = 3;
    public bool left = true;
    bool c = true;
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
        

        if (collider.gameObject.tag == "monster1" || collider.gameObject.tag == "monster2" || collider.gameObject.tag == "monster3" || collider.gameObject.tag == "dragon")
        {
            Debug.Log("door hit");
            //collider.isTrigger = false;
            audio.PlayOneShot(Resources.Load<AudioClip>("BreakingDoorDown"));
            this.GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce((this.transform.forward * 300), ForceMode.Impulse);
            GetComponent<Rigidbody>().useGravity = true;
            otherDoor.GetComponent<Rigidbody>().isKinematic = false;
            if (left)
            {
                otherDoor.GetComponent<Rigidbody>().AddForce((otherDoor.transform.forward * 250+ otherDoor.transform.right*(-80)), ForceMode.Impulse);
            }
            else
            {
                otherDoor.GetComponent<Rigidbody>().AddForce((otherDoor.transform.forward * 250 + otherDoor.transform.right * (100)), ForceMode.Impulse);

            }
            otherDoor.GetComponent<Rigidbody>().useGravity = true;
            if (c)
            {
                c = false;
                StartCoroutine(ExampleCoroutine());
            }
        }
    }
    IEnumerator ExampleCoroutine()
    {


        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);
        Destroy(otherDoor);
        Destroy(gameObject);
    }
}
