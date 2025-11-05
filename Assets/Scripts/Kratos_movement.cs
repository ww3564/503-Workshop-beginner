using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Kratos_movement : MonoBehaviour
{
    [SerializeField] private Vector2 movementInput = Vector2.zero;
    [SerializeField] float movementSpeed;

    [SerializeField] private float cameraSensitivity = 0.5f; //Adjustable variable based on how much camera should move based on input
    [SerializeField] private bool invertedCamera = false; //Is camera inverted or not?
    [SerializeField] private Vector2 cameraXValueClamp = new Vector2(-60f, 60f);
    //Clamp values that stop our camera from flipping fully over or under the player
    [SerializeField] private float jumpSpeed;

    private KeyCode JumpKey;

    private bool jumpTriggered;
    private ThirdPersonCamera myCamera;
    private Rigidbody rb;

    private bool moving = false;

    private void Start()
    {
        myCamera = GetComponent<ThirdPersonCamera>();
        //rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 newMovementInput = new Vector3(movementInput.x, 0, movementInput.y);
        float forwardsMovement = newMovementInput.z;
        float sidewaysMovement = newMovementInput.x;
        transform.position += transform.forward * forwardsMovement * Time.deltaTime * movementSpeed;
        transform.position += transform.right * sidewaysMovement * Time.deltaTime * movementSpeed;
    }

    public void SyncMovementInput(InputAction.CallbackContext input)
    {
        movementInput = input.ReadValue<Vector2>(); //Vector2 Bounds of (1, 1) and (-1, -1), rests at (0,0);
    }

    public void MoveCamera(InputAction.CallbackContext newInput) //Assign an Input Event to this, using Mouse Delta / Joystick input
    {
        Vector2 inputAxes = newInput.ReadValue<Vector2>() * cameraSensitivity; // Get Mouse Input * mouseSensitivity

        #region HorizontalMovement

        transform.Rotate(0f, inputAxes.x, 0f); //Spin our whole character left / right based 
        #endregion

        #region VerticalMoveement

        float currentRot = myCamera.transform.localEulerAngles.x; //Get our current rotation on the X axes (Up / Down)
        float vertAngle = invertedCamera ? inputAxes.y : -inputAxes.y; //If our camera is inverted, factor that into our calculations

        if (currentRot > 180f) //Remember, a euler angle under 0f doesn't go negative, but wraps around to 360f
        {
            currentRot -= 360f; //So we do this map to remap it to our understanding of angles
        }

        float desiredRot = currentRot + vertAngle; //We use this angle to check if we've rotated "too far" and flipped the camera

        //We ultimately need to rotate the camera by a float value, "the angular amount we want to turn", here called vertAngle
        //To make sure we don't over turn, we first check if our "desired Value" is in bounds
        //If not, we can then adjust vertAngle to an amount within bounds

        if (desiredRot > cameraXValueClamp.y) //If our desired Rot is too high
        {
            Debug.Log(desiredRot + " is too high");
            vertAngle = cameraXValueClamp.y - currentRot - 0.01f; //Make sure we rotated by an inbounds angle
        }
        else if (desiredRot < cameraXValueClamp.x) //If our desired ROt is too low
        {
            Debug.Log(desiredRot + " is too low");
            vertAngle = cameraXValueClamp.y + currentRot + 0.01f; //Make sure we rotated by an inbounds angle
        }

        myCamera.transform.Rotate(vertAngle, 0f, 0f); //Rotate the camera around the player vertically

        #endregion
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector2 Jumping {  get; private set; }

    public event Action OnJumpButton;

    /*private void Awake()
    {
        .OnJumpButton += JumpButtonPressed;
    }

    private void OnMove(InputValue inputValue)
    {
        Jumping = inputValue.Get<Vector2>();
    }

    private void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            OnJumpButton?.Invoke();
        }
    }

    private void JumpButtonPressed()
    {
        jumpTriggered = true;
    } */
}