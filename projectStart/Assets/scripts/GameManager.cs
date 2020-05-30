using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{

    //this script will have all information stored about the game and game state
    public StandingTargetSystem[] standingTargetSystems;
    public GameObject breakableObjects;
    public Transform dragonSpawnPoint;
    public Dragon boss;
    private static GameManager instance; //Singelton pattern
    public enum LEVEL
    {
        INTRO,
        BREAK_OBJECTS,
        NINJA_STARS,
        FIGHT_MONSTERS,
        DRAGON_BOSS,
        FINAL,
        END

    }
    private LEVEL _currentLevel;


    private int attackWave = 0; //which attackWave is currently active
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

    public LEVEL GetLevel()
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
        _currentLevel = LEVEL.INTRO;
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
        _currentLevel = LEVEL.NINJA_STARS;
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
        _currentLevel = LEVEL.BREAK_OBJECTS;
        Debug.Log(_currentLevel);
        LoadInstance("Vases");
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
        _currentLevel = LEVEL.FIGHT_MONSTERS;
        attackWave++;
        Debug.Log(_currentLevel);
        inProgress = false;
        //call up monsters

    }

    public void SetLevelDragonBoss()
    {
        inProgress = true;
        if (!manual)
        {
            grandpa.FinalAction();
        }
        StartCoroutine(LoadLevelDragonBoss());
    }
    IEnumerator LoadLevelDragonBoss()
    {
        yield return new WaitForSeconds(1.0f);
        _currentLevel = LEVEL.DRAGON_BOSS;
        Instantiate(boss, dragonSpawnPoint.position, dragonSpawnPoint.rotation);
        Debug.Log(_currentLevel);
        inProgress = false;
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
        yield return new WaitForSeconds(5.0f);
        _currentLevel = LEVEL.FINAL;
       // LoadInstance("RewardFinal");
        Debug.Log(_currentLevel);
        inProgress = false;
    }
    public void SetLevelEnd()
    {
        _currentLevel = LEVEL.END;
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
        //SetLevelFightMonsters();
        Debug.Log(_currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            manual = !manual;
        }
        if (_currentLevel.Equals(LEVEL.INTRO) && GameObject.FindGameObjectsWithTag("IntroObject").Length == 0 && !inProgress)
        {
            SetLevelToBreakObjects();
        } else if (_currentLevel.Equals(LEVEL.NINJA_STARS) && GameObject.FindGameObjectsWithTag("ninjaStarTarget").Length == 0 && !inProgress)
        {
            SetLevelFightMonsters();
        }
        else if (_currentLevel.Equals(LEVEL.BREAK_OBJECTS) && SwordTargetsCleared() && GameObject.FindGameObjectsWithTag("breakableItems").Length == 0 && !inProgress)
        {
            SetLevelToNinjaStars();
        }
        else if (_currentLevel.Equals(LEVEL.FIGHT_MONSTERS) && !inProgress)
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

                SetLevelDragonBoss();
            }
        }
        else if (_currentLevel.Equals(LEVEL.DRAGON_BOSS) && GameObject.FindGameObjectsWithTag("dragon").Length == 0  && !inProgress)
        {
            SetLevelFinal();
        }
    }
}

