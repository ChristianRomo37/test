using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [Header("Stats")]
    [SerializeField] public float MaxHp;
    [SerializeField] public float currHp;
    public bool dead;

    AudioSource audioSource;
    [Header("Audio")]    
    [SerializeField] AudioClip damage;
    [SerializeField] AudioClip _dead;
    [SerializeField][Range(0,1)] float damageVol;
    [SerializeField][Range (0,1)] float deadVol;

    private void Start()
    {
        currHp = MaxHp;

        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(currHp, currHp / MaxHp);

        audioSource = GameManager.instance.playerAudioSource;
    }

    public void TakeDamage(float _damage)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(damage, damageVol);
        }
        

        currHp -= _damage;

        if (currHp <= 0)
        {
            if(audioSource != null)
            {
               audioSource.PlayOneShot(_dead, deadVol);
            }
            
            dead = true;
        }

        PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(currHp, currHp / MaxHp);

    }
}
