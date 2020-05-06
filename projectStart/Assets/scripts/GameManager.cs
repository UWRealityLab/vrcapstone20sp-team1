using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{

    //this script will have all information stored about the game and game state
    public StandingTargetSystem[] standingTargetSystems;
    public GameObject breakableObjects;
    private static GameManager instance; //Singelton pattern
    private string _currentLevel = string.Empty;
    private readonly string[] LEVELS = { "Intro", "ninjaStars", "breakObjects", "fightMonsters", "final", "end" };
    private int attackWave = 0; //which attackWave is currently active
    private int deadMonsters = 0;
    private bool inProgress = false;
    private bool manual = false;
    private AudioManager audioManager;
    private Grandpa grandpa;

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

    public bool InProgress()
    {
        return inProgress;
    }
    public bool IsManual()
    {
        return manual;
    }
    public void SetLevelIntro()
    {
        _currentLevel = LEVELS[0];
        Debug.Log(_currentLevel);
    }
    public void SetLevelToNinjaStars()
    {
        inProgress = true;
        if (!manual)
        {
            grandpa.NinjaStarsAction();
        }
        StartCoroutine(LoadLevelNinjaStars());
    }
    IEnumerator LoadLevelNinjaStars()
    {
        yield return new WaitForSeconds(3.0f);
        _currentLevel = LEVELS[1];
        LoadInstance("Targets");
        Debug.Log(_currentLevel);
        inProgress = false;
    }
    public void SetLevelToBreakObjects()
    {
        inProgress = true;
        if (!manual)
        {
            grandpa.BreakObjectsAction();
        }
        StartCoroutine(LoadBreakObjectsScene());
        //make boxes appear
    }
    IEnumerator LoadBreakObjectsScene()
    {
        Debug.Log("break was called");
        yield return new WaitForSeconds(3.0f);
        _currentLevel = LEVELS[2];
        Debug.Log(_currentLevel);
        //LoadInstance("BreakableObjects");
        SpawnSwordTargets();
        inProgress = false;
    }
    public void SetLevelFightMonsters()
    {
        inProgress = true;
        if (!manual)
        {
            grandpa.FightAction();
        }
        StartCoroutine(LoadLevelFightMonsters());
        //call up monsters

    }
    IEnumerator LoadLevelFightMonsters()
    {
        yield return new WaitForSeconds(3.0f);
        _currentLevel = LEVELS[3];
        attackWave++;
        Debug.Log(_currentLevel);
        inProgress = false;
        //call up monsters

    }
    public void SetLevelFinal()
    {
        inProgress = true;
        audioManager.PlayFinal();
        if (!manual)
        {
            grandpa.FinalAction();
        }
        StartCoroutine(LoadLevelFinal());
    }
    IEnumerator LoadLevelFinal()
    {
        yield return new WaitForSeconds(3.0f);
        _currentLevel = LEVELS[4];
        LoadInstance("RewardFinal");
        LoadInstance("Effects");
        Debug.Log(_currentLevel);
        inProgress = false;
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

    private void SpawnSwordTargets()
    {
        foreach(StandingTargetSystem system in standingTargetSystems)
        {
            system.SpawnTarget();
        }
    }
    private bool SwordTargetsCleared()
    {
        foreach (StandingTargetSystem system in standingTargetSystems)
        {
            if(system.HasTarget())
            {
                return false;
            }
        }

        return true;
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
        grandpa = Grandpa.GetInstance();
        audioManager = AudioManager.GetInstance();
       
        SetLevelIntro();
        Debug.Log(_currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            manual = !manual;
        }
        if (_currentLevel.Equals(LEVELS[0]) && GameObject.FindGameObjectsWithTag("IntroObject").Length == 0 && !inProgress)
        {
            SetLevelToBreakObjects();
        } else if (_currentLevel.Equals(LEVELS[1]) && GameObject.FindGameObjectsWithTag("ninjaStarTarget").Length == 0 && !inProgress)
        {
            SetLevelFightMonsters();
        }
        else if (_currentLevel.Equals(LEVELS[2]) && SwordTargetsCleared() && !inProgress)
        {
            SetLevelToNinjaStars();
        }
        else if (_currentLevel.Equals(LEVELS[3]) && !inProgress)
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

