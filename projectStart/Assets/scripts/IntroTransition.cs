using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTransition : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
    }

    // Update is called once per frame
    void OnDestroy()
    {
        manager.SetLevelToBreakObjects();
    }
}
