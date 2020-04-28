using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : MonoBehaviour
{
    // Start is called before the first frame update
    private static Grandpa instance; //Singelton pattern
    public AudioClip intro;
    public AudioClip breakObjects;
    public AudioClip ninjaStars;
    public AudioClip fight;
    public AudioClip final;
    public static Grandpa GetInstance()
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
        intro = Resources.Load<AudioClip>("success_02");
        breakObjects = Resources.Load<AudioClip>("goodJob_06");
        ninjaStars = Resources.Load<AudioClip>("exellent_01");
        fight = Resources.Load<AudioClip>("LevelUp_01");
        final = Resources.Load<AudioClip>("gameover_03");

    }

    public void IntroAction()
    {
        Debug.Log("Intro Action called");
        if(intro == null)
        {
            intro = Resources.Load<AudioClip>("success_02");
        }
        GetComponent<AudioSource>().PlayOneShot(intro);

    }
    public void BreakObjectsAction()
    {
        Debug.Log("Intro Action called");
        if (breakObjects == null)
        {
            intro = Resources.Load<AudioClip>("goodJob_06");
        }
        GetComponent<AudioSource>().PlayOneShot(breakObjects);
    }
    public void NinjaStarsAction()
    {
        if (ninjaStars == null)
        {
            intro = Resources.Load<AudioClip>("exellent_01");
        }
        GetComponent<AudioSource>().PlayOneShot(ninjaStars);
    }
    public void FightAction()
    {
        if (fight == null)
        {
            intro = Resources.Load<AudioClip>("success_02");
        }
        GetComponent<AudioSource>().PlayOneShot(fight);
    }
    public void FinalAction()
    {
        if (final == null)
        {
            intro = Resources.Load<AudioClip>("gameover_03");
        }
        GetComponent<AudioSource>().PlayOneShot(final);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
