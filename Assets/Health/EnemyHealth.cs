using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamage
{
    [Header("Stats")]
    [SerializeField] float MaxHp;
    [SerializeField] float currHp;
    public bool dead;

    private void Start()
    {
        currHp = MaxHp;
    }

    public void TakeDamage(float damage)
    {
        currHp -= damage;

        if (currHp <= 0)
        {
            dead = true;
        }
    }
}
