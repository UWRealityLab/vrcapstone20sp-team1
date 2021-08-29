
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{

    //this script will have all information stored about the game and game state
    public StandingTargetSystem[] standingTargetSystems;
    public SpawnRailingTarget railingTargetSystem;
    public WispMovement wisp;
    public GameObject introObject;
    public GameObject wispStartPoint;
    public Transform dragonSpawnPoint;
    public Dragon boss;
    public GameObject breakableCeiling;
    public GameObject regularCeiling;
    public Wave[] waves;
    public GameObject reward;
    public AudioClip[] noise;
    public GameObject[] playPoints;
    public HandSwapperV2 handswapper;
    public GameObject clue;
    public GameObject monsterRabble;
    public ResonanceAudioSource athrielSpatialAudio;

    private static GameManager instance; //Singelton pattern
    public enum LEVEL
    {
        SETUP,
        INTRO,
        BREAK_VASES,
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
        introObject = LoadInstance("IntroObject");
        _currentLevel = LEVEL.INTRO;
        wisp.ClearTargets();
        wisp.SetMovementType(WispMovement.MovementType.STILL);
        Debug.Log("setting target to intro object");
        wisp.AddTarget(
            introObject,
            new Vector3(0, introObject.GetComponentInChildren<Renderer>().bounds.size.y + 0.5f, 0),
            introObject.GetComponentInChildren<Renderer>().bounds.size.x / 2 + 0.25f);
        Debug.Log(_currentLevel);
    }
    public void SetLevelToNinjaStars()
    {
        clue.SetActive(false);
        inProgress = true;
        handswapper.StartStarTutorial();
        grandpa.NinjaStarsAction();
        StartCoroutine(LoadLevelNinjaStars());
    }
    IEnumerator LoadLevelNinjaStars()
    {
        yield return new WaitForSeconds(10.0f);
        _currentLevel = LEVEL.NINJA_STARS;
        StartCoroutine(railingTargetSystem.Spawn3Targets());
        wisp.SetMovementType(WispMovement.MovementType.ROTATE_AROUND);
        wisp.AddTarget(
            railingTargetSystem.gameObject,
            new Vector3(0, railingTargetSystem.gameObject.GetComponentInChildren<Renderer>().bounds.size.y / 2 + 0.75f, 0),
            railingTargetSystem.gameObject.GetComponentInChildren<Renderer>().bounds.size.x / 2 + 0.25f);
        Debug.Log(_currentLevel);
        inProgress = false;
    }

    public void SetLevelToBreakVases()
    {
        inProgress = true;
        grandpa.BreakObjectsAction();
        StartCoroutine(LoadBreakVasesScene());
    }
    IEnumerator LoadBreakVasesScene()
    {
        Debug.Log("break was called");
        yield return new WaitForSeconds(9.0f);
        _currentLevel = LEVEL.BREAK_VASES;
        Debug.Log(_currentLevel);
        LoadInstance("Vases");
        GameObject[] vases = GameObject.FindGameObjectsWithTag("breakableItems");
        wisp.AddTargets(
            vases,
            Vector3.up,
            0.5f);
        wisp.SetMovementType(WispMovement.MovementType.ROTATE_AROUND);
        inProgress = false;

    }
    public void SetLevelToBreakObjects()
    {
        clue.SetActive(true);
        inProgress = true;
        grandpa.AirSlashAction();
        StartCoroutine(LoadBreakObjectsScene());
        //make boxes appear
    }
    IEnumerator LoadBreakObjectsScene()
    {
        yield return new WaitForSeconds(7.0f);
        _currentLevel = LEVEL.BREAK_OBJECTS;
        Debug.Log(_currentLevel);
        SpawnSwordTargets();
        // LoadInstance("Vases");
        wisp.ClearTargets();
        GameObject[] vases = GameObject.FindGameObjectsWithTag("breakableItems");
        wisp.AddTargets(
            vases,
            Vector3.up * 1.75f,
            0.5f);
        wisp.SetMovementType(WispMovement.MovementType.ROTATE_AROUND);
        inProgress = false;

    }
    public void SetLevelFightMonsters()
    {
        handswapper.EndStarTutorial();
        inProgress = true;
        grandpa.FightAction();
        //wisp.UnsetTarget();
        //playRandomMonsterSound();
        monsterRabble.SetActive(true);
        
        StartCoroutine(LoadLevelFightMonsters());
    }
    IEnumerator LoadLevelFightMonsters()
    {
        wisp.ClearTargets();
        audioManager.PlayFight();
        yield return new WaitForSeconds(10f);
        _currentLevel = LEVEL.FIGHT_MONSTERS;  
        wisp.AddTarget(
            reward,
            Vector3.zero,
            0.5f);
        wisp.SetMovementType(WispMovement.MovementType.STILL);
        LoadWave(attackWave);
        Debug.Log(_currentLevel);
        inProgress = false;
    }

    public void LoadWave(int waveNum)
    {
        waves[waveNum].SpawnEnemies();
    }

    public void SetLevelDragonBoss()
    {
        inProgress = true;
        grandpa.DragonAction();
        StartCoroutine(LoadLevelDragonBoss());
    }
    IEnumerator LoadLevelDragonBoss()
    {
        athrielSpatialAudio.gainDb = 13.0f;
        yield return new WaitForSeconds(5.0f);
        _currentLevel = LEVEL.DRAGON_BOSS;
        //regularCeiling.SetActive(false);
        Instantiate(boss, dragonSpawnPoint.position, dragonSpawnPoint.rotation);
        breakableCeiling.SetActive(true);
        Debug.Log(_currentLevel);
        inProgress = false;
    }

    public void SetLevelFinal()
    {
        inProgress = true;
        audioManager.PlayFinal();
        wisp.AddTarget(
            reward,
            Vector3.zero,
            0.5f);
        wisp.SetMovementType(WispMovement.MovementType.STILL);
        monsterRabble.SetActive(false);
        grandpa.FinalAction();
        StartCoroutine(LoadLevelFinal());
    }
    IEnumerator LoadLevelFinal()
    {
        yield return new WaitForSeconds(10.0f);
        _currentLevel = LEVEL.FINAL;
        //LoadInstance("RewardFinal");
        wisp.SetMovementType(WispMovement.MovementType.STILL);
        wisp.AddTarget(reward, new Vector3(0.5f, 0, -0.25f), 0.5f);
        Debug.Log(_currentLevel);
        inProgress = false;
    }
    public void SetLevelEnd()
    {
        _currentLevel = LEVEL.END;
        Debug.Log(_currentLevel);
        //grandpa.EndAction();
        //the end
    }
    public GameObject LoadInstance(string prefabN)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>(prefabN));
        return instance;
    }
    IEnumerator audioPlayWait(AudioClip clip, float waitTime, GameObject p)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("clip played");
        p.GetComponent<AudioSource>().PlayOneShot(clip);
    }
    public void playRandomMonsterSound()
    {
        foreach (GameObject p in playPoints){
            StartCoroutine(audioPlayWait(noise[Random.Range(0, noise.Length)], Random.Range(13, 25), p));
            StartCoroutine(audioPlayWait(noise[Random.Range(0, noise.Length)], Random.Range(5,12), p));
            StartCoroutine(audioPlayWait(noise[Random.Range(0, noise.Length)], Random.Range(0, 4), p));
            
        }
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
        audioManager.PlayIntro();
        wisp.SetMovementType(WispMovement.MovementType.STILL);
        wisp.AddTarget(wispStartPoint, Vector3.zero, 0f);
        manual = true;
        _currentLevel = LEVEL.NINJA_STARS;
        Debug.Log(_currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            manual = !manual;
            if (_currentLevel.Equals(LEVEL.SETUP))
            {
                SetLevelIntro();
            }
           
        }
        
        if (_currentLevel.Equals(LEVEL.INTRO) && GameObject.FindGameObjectsWithTag("IntroObject").Length == 0 && !inProgress)
        {
            SetLevelToBreakVases();
        } else if (_currentLevel.Equals(LEVEL.BREAK_VASES) && GameObject.FindGameObjectsWithTag("breakableItems").Length == 0 && !inProgress)
        {
            SetLevelToBreakObjects();
        } else if (_currentLevel.Equals(LEVEL.NINJA_STARS) && railingTargetSystem.ActiveTargets() == 0 && !inProgress)
        {
            SetLevelFightMonsters();
        }
        else if (_currentLevel.Equals(LEVEL.BREAK_OBJECTS) && SwordTargetsCleared() && SwordTargetsCleared() && !inProgress)
        {
            SetLevelToNinjaStars();
        }
        else if (_currentLevel.Equals(LEVEL.FIGHT_MONSTERS) && !inProgress)
        {
            if (waves[attackWave].EnemiesLeft() == 0)
            {
                attackWave++;
                if(attackWave < waves.Length)
                {
                    LoadWave(attackWave);
                }
                else
                {
                    SetLevelDragonBoss();
                }
            }
        }
        else if (_currentLevel.Equals(LEVEL.DRAGON_BOSS) && GameObject.FindGameObjectsWithTag("dragon").Length == 0  && !inProgress)
        {
            SetLevelFinal();
        }
    }
}

