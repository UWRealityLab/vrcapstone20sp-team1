using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : MonoBehaviour
{
    // Start is called before the first frame update
    private static Grandpa instance; //Singelton pattern
    public AudioClip intro;
    public AudioClip introObject;
    public AudioClip breakObjects;
    public AudioClip ninjaStars;
    public AudioClip fight;
    public AudioClip final;
    public AudioClip end;
    public AudioClip[] fighting;
    private float startTime = 0;
    private float len = 0;
    private long time;
    GameManager manager;
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
        manager = GameManager.GetInstance();
        intro = Resources.Load<AudioClip>("Voice_Lines/lets_get_started");
        introObject = Resources.Load<AudioClip>("Voice_Lines/its_me_grandpa_1");
        breakObjects = Resources.Load<AudioClip>("Voice_Lines/cut_all_these_logs");
        ninjaStars = Resources.Load<AudioClip>("Voice_Lines/throw_and_hit_those_targets");
        fight = Resources.Load<AudioClip>("Voice_Lines/do_you_hear_that");
        final = Resources.Load<AudioClip>("Voice_Lines/stone_of_life_isnt_safe_here");
        end = Resources.Load<AudioClip>("Voice_Lines/remove_the_spirit_link");
        time = 1;

    }


    public void IntroAction()
    {
        Debug.Log("Intro Action called");
        if (intro == null)
        {
            intro = Resources.Load<AudioClip>("Voice_Lines/lets_get_started");
        }
        GetComponent<AudioSource>().PlayOneShot(intro);

    }
    public void DragonAction()
    {
        Debug.Log("Intro Action called");
        //GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Voice_Lines/its_a_dragon"));
        StartCoroutine(audioPlayWait(Resources.Load<AudioClip>("Voice_Lines/its_a_dragon"), 3.0f));
    }
    public void BreakObjectsAction()
    {
        Debug.Log("Intro Action called");
        if (breakObjects == null || introObject == null)
        {
            breakObjects = Resources.Load<AudioClip>("Voice_Lines/cut_all_these_logs");
            introObject = Resources.Load<AudioClip>("Voice_Lines/its_me_grandpa_1");
        }
        GetComponent<AudioSource>().PlayOneShot(introObject);
        StartCoroutine(audioPlayWait(breakObjects, introObject.length));

    }
    public void AirSlashAction()
    {
        Debug.Log("Intro Action called");
        if (breakObjects == null)
        {
            breakObjects = Resources.Load<AudioClip>("Voice_Lines/cut_all_these_logs");
        }
        GetComponent<AudioSource>().PlayOneShot(breakObjects);
    }
    public void NinjaStarsAction()
    {
        if (ninjaStars == null)
        {
            ninjaStars = Resources.Load<AudioClip>("Voice_Lines/throw_and_hit_those_targets");
        }
        GetComponent<AudioSource>().PlayOneShot(ninjaStars);
    }
    public void FightAction()
    {
        if (fight == null)
        {
            fight = Resources.Load<AudioClip>("Voice_Lines/do_you_hear_that");
        }
        AudioClip a1 = Resources.Load<AudioClip>("Voice_Lines/monsters_are_coming_1");
        AudioClip a2 = Resources.Load<AudioClip>("Voice_Lines/are_you_ready");
        GetComponent<AudioSource>().PlayOneShot(fight);
        StartCoroutine(audioPlayWait(a1, fight.length));
        StartCoroutine(audioPlayWait(a2, fight.length + a1.length));
    }
    public void FinalAction()
    {
        if (final == null)
        {
            final = Resources.Load<AudioClip>("Voice_Lines/stone_of_life_isnt_safe_here");
        }
        AudioClip a1 = Resources.Load<AudioClip>("Voice_Lines/bring_it_back_to_our_world");
        AudioClip a2 = Resources.Load<AudioClip>("Voice_Lines/its_safer_with_you");
        GetComponent<AudioSource>().PlayOneShot(final);
        StartCoroutine(audioPlayWait(a1, final.length));
        StartCoroutine(audioPlayWait(a2, final.length + a1.length));
    }
    public void EndAction()
    {
        if (end == null)
        {
            end = Resources.Load<AudioClip>("Voice_Lines/remove_the_spirit_link");
        }
        GetComponent<AudioSource>().PlayOneShot(end);
    }

    public void onMonsterDeath()
    {
        if(!isPlaying()){
            AudioClip a = fighting[Random.Range(0, fighting.Length)];
            GetComponent<AudioSource>().PlayOneShot(a);
            startTime = Time.time;
            len = a.length;
        }
    }
    IEnumerator audioPlayWait(AudioClip clip, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
    public bool isPlaying()
    {
        if ((Time.time - startTime) >= len)
            return false;
        return true;
    }

// Update is called once per frame
void Update()
    {
        if (time % 600 == 0 && manager.GetLevel() == GameManager.LEVEL.INTRO && !manager.InProgress())
        {
            IntroAction();
        }else if (manager.InProgress())
        {
            time = 0;
        }else if ((time % 700 == 0) && manager.GetLevel() == GameManager.LEVEL.END && !manager.InProgress())
        {
            EndAction();
        }
        time++;

    }
}
