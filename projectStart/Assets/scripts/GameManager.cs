using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //this script will have all information stored about the game and game state
    public GameObject breakableObjects;
    private static GameManager instance; //Singelton pattern
    private string _currentLevel = string.Empty;
    private readonly string[] LEVELS = { "Intro", "ninjaStars", "breakObjects", "fightMonsters", "final", "end" };
    private int attackWave = 0; //which attackWave is currently active
    private int deadMonsters = 0;


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
        //load targets
        Debug.Log(_currentLevel);
    }
    public void SetLevelToBreakObjects()
    {
        _currentLevel = LEVELS[1];
        Debug.Log(_currentLevel);
        //GameObject instance = Instantiate(breakableObjects);
        //make boxes appear
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
        _currentLevel = LEVELS[4];
        Debug.Log(_currentLevel);
    }
    public void SetLevelEnd()
    {
        _currentLevel = LEVELS[5];
        Debug.Log(_currentLevel);
        //the end
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

