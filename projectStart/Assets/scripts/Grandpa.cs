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
    public AudioClip end;
    public AudioClip[] fighting; 
    
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
        intro = Resources.Load<AudioClip>("success_02");
        breakObjects = Resources.Load<AudioClip>("goodJob_06");
        ninjaStars = Resources.Load<AudioClip>("exellent_01");
        fight = Resources.Load<AudioClip>("LevelUp_01");
        final = Resources.Load<AudioClip>("gameover_03");
        end = Resources.Load<AudioClip>("gameover_01");
        time = 1;

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
            breakObjects = Resources.Load<AudioClip>("goodJob_06");
        }
        GetComponent<AudioSource>().PlayOneShot(breakObjects);
    }
    public void NinjaStarsAction()
    {
        if (ninjaStars == null)
        {
            ninjaStars = Resources.Load<AudioClip>("exellent_01");
        }
        GetComponent<AudioSource>().PlayOneShot(ninjaStars);
    }
    public void FightAction()
    {
        if (fight == null)
        {
            fight = Resources.Load<AudioClip>("success_02");
        }
        GetComponent<AudioSource>().PlayOneShot(fight);
    }
    public void FinalAction()
    {
        if (final == null)
        {
             final = Resources.Load<AudioClip>("gameover_03");
        }
        GetComponent<AudioSource>().PlayOneShot(final);
    }
    public void EndAction()
    {
        if (end == null)
        {
            end = Resources.Load<AudioClip>("gameover_01");
        }
        GetComponent<AudioSource>().PlayOneShot(end);
    }
    
    public void onMonsterDeath()
    {
        GetComponent<AudioSource>().PlayOneShot(fighting[Random.Range(0, fighting.Length)]);
    }


    // Update is called once per frame
    void Update()
    {
        if (time % 1500 == 0 && manager.GetLevel() == GameManager.LEVEL.INTRO && !manager.InProgress())
        {
            IntroAction();
        }else if (manager.InProgress())
        {
            time = 0;
        }
        if (time % 1000 == 0 && manager.GetLevel() == GameManager.LEVEL.END && !manager.InProgress())
        {
            EndAction();
        }
        if (time == 1000 && manager.GetLevel() == GameManager.LEVEL.BREAK_OBJECTS && !manager.InProgress())
        {
           AudioClip n = end = Resources.Load<AudioClip>("n");
           GetComponent<AudioSource>().PlayOneShot(n);
        }
        time++;

    }
}
