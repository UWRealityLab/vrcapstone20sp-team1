using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpaController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip intro;
    public AudioClip breakObjects;
    public AudioClip ninjaStars;
    public AudioClip fight;
    public AudioClip final;
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
        if(intro == null)
        {
            intro = Resources.Load<AudioClip>("success_02");
        }
        GetComponent<AudioSource>().PlayOneShot(intro);

    }
    public void BreakObjectsAction()
    {
        GetComponent<AudioSource>().PlayOneShot(breakObjects);
    }
    public void NinjaStarsAction()
    {
        GetComponent<AudioSource>().PlayOneShot(ninjaStars);
    }
    public void FightAction()
    {
        GetComponent<AudioSource>().PlayOneShot(fight);
    }
    public void FinalAction()
    {
        GetComponent<AudioSource>().PlayOneShot(final);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
