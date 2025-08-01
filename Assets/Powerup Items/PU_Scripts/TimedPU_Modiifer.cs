using System.Collections;
using UnityEngine;

public abstract class TimedPU_Modiifer : PU_Modifer
{
    public float powerupTimeInSecs;

    public abstract void Deactivate(GameObject target);

    public IEnumerator StartPowerUpCountdown(GameObject target)
    {
        yield return new WaitForSeconds(powerupTimeInSecs);  
        
        Deactivate(target); 
    }
}
