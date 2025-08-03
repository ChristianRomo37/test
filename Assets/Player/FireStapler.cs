using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class FireStapler : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float dmg;
    [SerializeField] public float fireRate;
    [SerializeField] public float bulletSpeed;
    [Space]
    [SerializeField] public float maxMagazine;
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
    ReticleSpread reticleSpread;

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
        reticleSpread = PlayerUIManager.instance.playerUIHudManager.reticleSpread;
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
        PlayerUIManager.instance.playerUIHudManager.SpreadReticleIsShooting(isShooting);
    }

    private IEnumerator Shoot()
    { 
        isShooting = true;

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);
        Ray screenRay = Camera.main.ViewportPointToRay(screenCenter);

        float y = UnityEngine.Random.Range((-reticleSpread.currentSize / reticleSpread.maxSize) / 15, (reticleSpread.currentSize / reticleSpread.maxSize) / 15);
        float x = UnityEngine.Random.Range((-reticleSpread.currentSize / reticleSpread.maxSize) / 15, (reticleSpread.currentSize / reticleSpread.maxSize) / 15);

        if(audioSource != null)
        {
            audioSource.PlayOneShot(shoot, shootVol);
        }
        
        staple = Instantiate(bullet, shootPos.position, camera.transform.rotation);
        currMag--;
        Debug.Log("BulletSpeed: " + bulletSpeed);

        Vector3 spreadDirection = screenRay.direction + new Vector3(x, y, 0f);

        staple.GetComponent<Rigidbody>().AddForce(spreadDirection * bulletSpeed, ForceMode.Impulse);

        yield return new WaitForSeconds(fireRate);

        isShooting = false;
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        if (GameManager.instance.akimboArm.activeSelf)
            anim.SetBool("Reload2", true);
        else
            anim.SetBool("Reload", true);

            yield return new WaitForSeconds(reloadWait);
        currMag = maxMagazine;
        isReloading = false;

        if (GameManager.instance.akimboArm.activeSelf)
            anim.SetBool("Reload2", false);
        else
            anim.SetBool("Reload", false);

        if (!isReloading) {audioSource.PlayOneShot(_reload, reloadVol);}
    }

}
