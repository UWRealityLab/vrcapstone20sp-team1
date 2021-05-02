using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip final;
    public AudioClip fight;
    public AudioClip begin;
    private static AudioManager instance; //Singelton pattern
    AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        final = Resources.Load<AudioClip>("Music/InstrumentalJapaneseMusic");
        fight = Resources.Load<AudioClip>("Music/NinjaWishBattleMusic");
        begin = Resources.Load<AudioClip>("Music/DistantDreams");
    }
    public void PlayIntro()
    {
        if(audioSource == null)
        {
            Debug.Log("audioSource is null");
            audioSource = GetComponent<AudioSource>();
            final = Resources.Load<AudioClip>("Music/InstrumentalJapaneseMusic");
            fight = Resources.Load<AudioClip>("Music/NinjaWishBattleMusic");
            begin = Resources.Load<AudioClip>("Music/DistantDreams");
        }
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.clip = begin;
        audioSource.volume = 0.15f;
        audioSource.Play();
    }
    public void PlayFight()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.volume = 0.1f;
        audioSource.clip = fight;
        audioSource.Play();
    }
    public void PlayFinal()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.volume = 0.14f;
        audioSource.clip = final;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
