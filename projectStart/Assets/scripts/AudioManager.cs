using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip final;
    private static AudioManager instance; //Singelton pattern
    public static AudioManager GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            Debug.Log("Instance is created");
        }
        else
        {
            Debug.LogError("there are multiple game managers");
        }
    }
    void Start()
    {
        final = Resources.Load<AudioClip>("Nobility");
    }

    public void PlayFinal()
    {
        GetComponent<AudioSource>().PlayOneShot(final);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
