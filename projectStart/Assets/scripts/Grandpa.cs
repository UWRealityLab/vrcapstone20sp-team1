using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandpa : MonoBehaviour
{
    // Start is called before the first frame update
    private static Grandpa instance; //Singelton pattern
    public AudioClip[] audioClips;
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
    private int random = -1;
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
        time = 1;

    }


    public void IntroAction()
    {
        Debug.Log("Intro Action called");
/*        if (intro == null)
        {
            intro = Resources.Load<AudioClip>("Voice_Lines/lets_get_started");
        }*/
        GetComponent<AudioSource>().PlayOneShot(audioClips[0]);

    }
    public void DragonAction()
    {
        StartCoroutine(audioPlayWait(audioClips[12], 7.0f));
    }
    public void BreakObjectsAction()
    {
        Debug.Log("BreakVases " + audioClips[1].length);
        GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
        StartCoroutine(audioPlayWait(audioClips[2], audioClips[1].length));

    }

    public void AirSlashAction()
    {
        Debug.Log("Ait Slash - logs  " + audioClips[3].length);
        GetComponent<AudioSource>().PlayOneShot(audioClips[3]);
        StartCoroutine(audioPlayWait(audioClips[4], audioClips[3].length));
        StartCoroutine(audioPlayWait(audioClips[5], audioClips[3].length + audioClips[4].length));
    }

    public void NinjaStarsAction()
    {
        Debug.Log("Ninja Start - targets  " + audioClips[6].length);
        GetComponent<AudioSource>().PlayOneShot(audioClips[6]);
        StartCoroutine(audioPlayWait(audioClips[7], audioClips[6].length));
        StartCoroutine(audioPlayWait(audioClips[8], audioClips[6].length + audioClips[7].length));
    }
    public void FightAction()
    {
        Debug.Log("final Action " + audioClips[9].length);
        GetComponent<AudioSource>().PlayOneShot(audioClips[9]);
        StartCoroutine(audioPlayWait(audioClips[10], audioClips[9].length));
        StartCoroutine(audioPlayWait(audioClips[11], audioClips[9].length + audioClips[10].length + 2.0f));
    }
    public void FinalAction()
    {
        Debug.Log("Final Action " + audioClips[14].length);
        GetComponent<AudioSource>().PlayOneShot(audioClips[14]);
        StartCoroutine(audioPlayWait(audioClips[15], audioClips[14].length));
    }
    public void EndAction()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClips[16]);
    }

    public void onMonsterDeath()
    {
        if(!isPlaying()){
            int random2 = Random.Range(0, fighting.Length);
            if(random != random2)
            {
                AudioClip a = fighting[random2];
                GetComponent<AudioSource>().PlayOneShot(a);
                startTime = Time.time;
                len = a.length;
                random = random2;
            }           
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
void FixedUpdate()
    {
        if ((time % 2000 == 0 || time == 10) && manager.GetLevel() == GameManager.LEVEL.INTRO && !manager.InProgress())
        {
            IntroAction();
        }else if (manager.InProgress() || manager.IsManual())
        {
            time = 0;
        }else if ((time % 700 == 0) && manager.GetLevel() == GameManager.LEVEL.END && !manager.InProgress())
        {
            EndAction();
        }
        time++;

    }
}
