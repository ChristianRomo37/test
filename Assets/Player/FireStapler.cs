using System.Collections;
using UnityEngine;

public class FireStapler : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float dmg;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPos;
    public KeyCode fire = KeyCode.Mouse0;
    bool isShooting;

    Camera camera;
    GameObject staple;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(fire))
        {
            if(!isShooting) {
            StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    { 
        isShooting = true;

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        staple = Instantiate(bullet, shootPos.position, camera.transform.rotation);
        staple.GetComponent<Rigidbody>().AddForce(camera.transform.forward * bulletSpeed, ForceMode.Impulse);

        yield return new WaitForSeconds(fireRate);

        isShooting = false;
    }

}
