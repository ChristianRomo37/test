using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;

    private List<PointInTime> pointsInTime;

    [SerializeField] public float timeToRewind;

    Rigidbody rb;

    bool on;

    private void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GameManager.instance.rewindManager.isActiveAndEnabled)
        {
            on = true;
        }
        else
        {
            on = false;
        }
    }

    private void FixedUpdate()
    {
        if (on)
        {
            if (isRewinding)
                Rewind(); 
            else
                Record();
        }
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0) {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            GameManager.instance.player.GetComponentInChildren<FireStapler>().currMag = pointInTime.ammo;
            GameManager.instance.player.GetComponent<PlayerHealth>().currHp = pointInTime.health;
            pointsInTime.RemoveAt(0);
        } else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(timeToRewind / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, GameManager.instance.player.GetComponentInChildren<FireStapler>().currMag, GameManager.instance.player.GetComponent<PlayerHealth>().currHp));
    }

    public void StartRewind(){
        isRewinding = true;}

    public void StopRewind(){ 
        isRewinding = false;}
}
