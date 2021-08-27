using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    public FireBreath fireBreath;

    // Methods are called from animation events attached to the start and end of attack3 and the start of flinch
    void StartBreath()
    {
        fireBreath.FireDelay();
    }
    void StopBreath()
    {
        fireBreath.FireStop();
    }
    void SuddenStop()
    {
        fireBreath.AbruptStop();
    }
}
