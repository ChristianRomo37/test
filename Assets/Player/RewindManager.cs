using System.Collections;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    TimeBody timeBody;

    private void Start()
    {
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
