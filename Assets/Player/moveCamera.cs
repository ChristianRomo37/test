using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public Transform cameraPos;

    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
