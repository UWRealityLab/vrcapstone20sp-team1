using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //this script will have all information stored about the game and game state
    public GameObject breakableObjects;
    public AudioClip breakableInst;
    public AudioClip final;
    private static GameManager instance; //Singelton pattern
    private string _currentLevel = string.Empty;
    private readonly string[] LEVELS = { "Intro", "ninjaStars", "breakObjects", "fightMonsters", "final", "end" };
    private int attackWave = 0; //which attackWave is currently active
    private int deadMonsters = 0;
    private bool InProgress = false;
    //AudioManager audioManager;

    public static GameManager GetInstance()
    {
        return instance;
    }

    public int GetAttackWave()
    {
        return attackWave;
    }

    public string GetLevel()
    {
        return _currentLevel;
    }

    public void SetLevelIntro()
    {    
        _currentLevel = LEVELS[0];
        Debug.Log(_currentLevel);
    }
    public void SetLevelToNinjaStars()
    {
        _currentLevel = LEVELS[1];
        LoadInstance("Targets");
        Debug.Log(_currentLevel);
    }
    public void SetLevelToBreakObjects()
    {
        InProgress = true;
        StartCoroutine(LoadBreakObjectsScene());
        //make boxes appear
    }
    IEnumerator LoadBreakObjectsScene()
    {
        Debug.Log("break was called");
        GetComponent<AudioSource>().PlayOneShot(breakableInst);
        yield return new WaitForSeconds(1.0f);
        _currentLevel = LEVELS[2];
        Debug.Log(_currentLevel);
        LoadInstance("BreakableObjects");
        InProgress = false;
    }
    public void SetLevelFightMonsters()
    {
        _currentLevel = LEVELS[3];
        attackWave++;
        Debug.Log(_currentLevel);
        //call up monsters

    }
    public void SetLevelFinal()
    {
        GetComponent<AudioSource>().PlayOneShot(final);
        _currentLevel = LEVELS[4];
        LoadInstance("RewardFinal");
        Debug.Log(_currentLevel);
    }
    public void SetLevelEnd()
    {
        _currentLevel = LEVELS[5];
        Debug.Log(_currentLevel);
        //the end
    }
    public GameObject LoadInstance(string prefabN)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>(prefabN));
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
    // Start is called before the first frame update
    void Start()
    {
        //audioManager = AudioManager.GetInstance();
        breakableInst = Resources.Load<AudioClip>("exellent_01");
        final = Resources.Load<AudioClip>("Nobility");
        SetLevelIntro();
        Debug.Log(_currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentLevel.Equals(LEVELS[1]) && GameObject.FindGameObjectsWithTag("ninjaStarTarget").Length == 0)
        {
            SetLevelFightMonsters();
        }
        else if (_currentLevel.Equals(LEVELS[2]) && GameObject.FindGameObjectsWithTag("breakableItems").Length == 0)
        {
            SetLevelToNinjaStars();
        }
        else if (_currentLevel.Equals(LEVELS[3]))
        {
            if (attackWave == 1 && GameObject.FindGameObjectsWithTag("monster1").Length == 0)
            {
                attackWave++;
                Debug.Log(attackWave);
            }
            else if (attackWave == 2 && GameObject.FindGameObjectsWithTag("monster2").Length == 0)
            {
                attackWave++;
                Debug.Log(attackWave);
            }
            else if (attackWave == 3 && GameObject.FindGameObjectsWithTag("monster3").Length == 0)
            {

                SetLevelFinal();
            }
        }
    }
}

