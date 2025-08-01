using UnityEngine;

[CreateAssetMenu(fileName = "PU_Modifer", menuName = "Scriptable Objects/PU_Modifer")]
public abstract class PU_Modifer : ScriptableObject
{
    public abstract void Activate(GameObject target);
    
}
