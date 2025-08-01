using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float lifeTime = 3f;
    int damage = 10;

    [Header("Rotation")]
    public Vector3 spinSpeed = new Vector3(360f, 360f, 360f); // degrees per second

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
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

        Destroy(gameObject);
    }
}

