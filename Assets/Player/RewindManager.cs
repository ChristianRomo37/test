using System.Collections;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    public static RewindManager instance;
    public TimeBody timeBody;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        timeBody = GetComponentInParent<TimeBody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            timeBody.StartRewind();
        }
        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    timeBody.StopRewind();
        //}
    }

    private IEnumerator Rewind()
    {
        timeBody.StartRewind();

        yield return new WaitForSeconds(timeBody.timeToRewind);

        timeBody.StopRewind();
    }
}
