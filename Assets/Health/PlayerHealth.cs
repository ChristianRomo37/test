using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [Header("Stats")]
    [SerializeField] float MaxHp;
    [SerializeField] public float currHp;
    public bool dead;

    private void Start()
    {
        currHp = MaxHp;

        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(currHp, currHp / MaxHp);
    }

    public void TakeDamage(float damage)
    {
        currHp -= damage;

        if (currHp <= 0)
        {
            dead = true;
        }

        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(currHp, currHp / MaxHp);

    }
}
