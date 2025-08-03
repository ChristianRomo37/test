using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 3f;

    [Header("Damage")]
    public int damage = 1;

    [Header("Rotation")]
    public Vector3 spinSpeed = new Vector3(360f, 360f, 360f);

    [Header("Target")]
    public string targetTag;

    //[Header("Pierce Bullets Script")]
    //[SerializeField] PierceBulletsMod pierceBulletsScript;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);

        //if (this.gameObject.CompareTag("Bullet"))
        //{
        //    pierceBulletsScript.pierceBullets = false;

        //    if (!pierceBulletsScript.pierceBullets)
        //        Destroy(gameObject, lifeTime);
        //    else
        //        Destroy(gameObject, 6f);
        //}
    }

    void Update()
    {
        //transform.Rotate(spinSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        transform.Rotate(spinSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }

        //if (this.gameObject.CompareTag("Bullet"))
        //{
        //    if (pierceBulletsScript.pierceBullets)
        //    {

        //    }
        //}
        //else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            IDamage damageable = other.GetComponentInParent<IDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            //if (this.gameObject.CompareTag("Bullet"))
            //{
            //    if (pierceBulletsScript)
            //    {
                   
            //    }
            //}
            //else
            //{
               CleanUp();
            //}
        }
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(5f);
    }

}

