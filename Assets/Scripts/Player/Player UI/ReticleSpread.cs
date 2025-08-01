using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReticleSpread : MonoBehaviour
{
    private RectTransform reticle;
    public PlayerMovement player;
    [SerializeField] bool boolCheck;



    public float restingSize;
    public float maxSize;
    public float speed;
    public float currentSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
        player = FindAnyObjectByType<PlayerMovement>();
    }

    private void Update()
    {
        if (isMoving)
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        else
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);



        reticle.sizeDelta = new Vector2(currentSize, currentSize);

    }

    bool isMoving
    {
        get
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
