using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class FireStapler : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float dmg;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;
    [Space]
    [SerializeField] private float maxMagazine;
    public float currMag;
    [SerializeField] private float reloadWait;
    public KeyCode reload = KeyCode.R;
    bool isReloading;
    [Space]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;
    public KeyCode fire = KeyCode.Mouse0;
    bool isShooting;
    private Animator anim;

    [Header("Audio")]
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip _reload;
    [SerializeField][Range(0,1)] float shootVol;
    [SerializeField][Range(0,1)] float reloadVol;
    AudioSource audioSource;

    Camera camera;
    GameObject staple;
    PlayerHealth playerHealth;

    private void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        currMag = maxMagazine;
        playerHealth = GetComponentInParent<PlayerHealth>();
        PlayerUIManager.instance.playerUIHudManager.SetAmmoText(currMag, maxMagazine);
        audioSource = GameManager.instance.playerAudioSource;
    }

    private void Update()
    {
        if (!playerHealth.dead)
        {
            if (Input.GetKey(fire) && currMag > 0 && !isReloading)
            {
                if(!isShooting) {
                StartCoroutine(Shoot());
                }
            }

            if (Input.GetKey(reload))
            {
                StartCoroutine(Reload());
            }
        }
        PlayerUIManager.instance.playerUIHudManager.SetAmmoText(currMag, maxMagazine);
    }

    private IEnumerator Shoot()
    { 
        isShooting = true;

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        audioSource.PlayOneShot(shoot, shootVol);
        staple = Instantiate(bullet, shootPos.position, camera.transform.rotation);
        currMag--;
        staple.GetComponent<Rigidbody>().AddForce(camera.transform.forward * bulletSpeed, ForceMode.Impulse);

        yield return new WaitForSeconds(fireRate);

        isShooting = false;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("Reload", true);

        yield return new WaitForSeconds(reloadWait);
        currMag = maxMagazine;
        isReloading = false;
        anim.SetBool("Reload", false);
        if (!isReloading) {audioSource.PlayOneShot(_reload, reloadVol);}
    }

}
