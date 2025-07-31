using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector3 movementInputVector {  get; private set; }
    public Axis xAxis {  get; private set; }
    public Axis yAxis { get; private set; }

    private void OnMove(InputValue inputValue)
    {
        movementInputVector = inputValue.Get<Vector3>();
    }

    private void OnMouseX(InputValue inputValue)
    {
        xAxis = inputValue.Get<Axis>();
    }

    private void OnMouseY(InputValue inputValue) 
    { 
        yAxis = inputValue.Get<Axis>();
    }
}
