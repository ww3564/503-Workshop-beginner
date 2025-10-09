using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Kratos_movement : MonoBehaviour
{
    [SerializeField] private Vector2 movementInput = Vector2.zero;
    [SerializeField] float movement;

    private bool moving = false;

    private void Update()
    {
        Vector3 newMovementInput = new Vector3(movementInput.x, 0, movementInput.y);
        transform.position += newMovementInput * Time.deltaTime * movement;
    }

    public void SyncMovementInput(InputAction.CallbackContext input)
    {
        movementInput = input.ReadValue<Vector2>(); //Vector2 Bounds of (1, 1) and (-1, -1), rests at (0,0);
    }
}
