using UnityEngine;
using UnityEngine.InputSystem;

public class Kratos_movement : MonoBehaviour
{
    public static Kratos_movement i;

    [SerializeField] private Vector2 movementInput = Vector2.zero;
    private bool jumpInput;


    [SerializeField] float movementSpeed;

    [SerializeField] private float cameraSensitivity = 0.5f; //Adjustable variable based on how much camera should move based on input
    [SerializeField] private bool invertedCamera = false; //Is camera inverted or not?
    [SerializeField] private Vector2 cameraXValueClamp = new Vector2(-60f, 60f);
    //Clamp values that stop our camera from flipping fully over or under the player
    
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float groundDistanceCheck;
    [SerializeField] private float gravity;

    [SerializeField] Vector3 velocity;

    private Camera myCamera;
    private Rigidbody rb;

    private void Start()
    {
        myCamera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;

        i = this;

        if(Saving_and_Loading.IsThereDataToLoad())
        {
            transform.position = Saving_and_Loading.LoadData().playerPos;
        }
    }

    private void OnDestroy()
    {
        i = null;
    }

    private void Update()
    {
        Vector3 newMovementInput = new Vector3(movementInput.x, 0, movementInput.y);
        float forwardsMovement = newMovementInput.z;
        float sidewaysMovement = newMovementInput.x;

        Vector3 horizontalVel = (transform.forward * forwardsMovement * movementSpeed) + (transform.right * sidewaysMovement * movementSpeed);
        float verticalVel = rb.linearVelocity.y;

        if (OnGrounded())
        {
            if (verticalVel < 0f)
            {
                Debug.Log("Setting velocity to 0");
                verticalVel = 0;
                
            }

            if (jumpInput)
            {
                verticalVel = jumpSpeed;
            }
        }
        else
        {
            verticalVel -= gravity * Time.deltaTime;
        }

        rb.linearVelocity = horizontalVel + (Vector3.up * verticalVel);

        velocity = rb.linearVelocity;
        jumpInput = false;
    }

    private bool OnGrounded()
    {
        RaycastHit hitInfo;
        MeshCollider col = GetComponentInChildren<MeshCollider>();
        Vector3 rayCastOffset = -Vector3.up * col.bounds.extents.y * 0.9f;
        Physics.Raycast(transform.position + rayCastOffset, -transform.up, out hitInfo, groundDistanceCheck);
        Debug.DrawRay(transform.position + rayCastOffset, -transform.up * groundDistanceCheck, Color.red, 0.1f);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.tag == "Platforms")
            {
                return true;
            }
        }

        return false;
    }

    public void SyncMovementInput(InputAction.CallbackContext input)
    {
        movementInput = input.ReadValue<Vector2>(); //Vector2 Bounds of (1, 1) and (-1, -1), rests at (0,0);
    }

    public void SyncJumpMovement(InputAction.CallbackContext input)
    {
        if(!input.canceled)
        {
            jumpInput = true;
        }
    }

    public void MoveCamera(InputAction.CallbackContext newInput) //Assign an Input Event to this, using Mouse Delta / Joystick input
    {
        if (Time.timeScale == 0)
        {
            return;
        }

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
            vertAngle = cameraXValueClamp.y - currentRot - 0.01f; //Make sure we rotated by an inbounds angle
        }
        else if (desiredRot < cameraXValueClamp.x) //If our desired ROt is too low
        {
            vertAngle = cameraXValueClamp.y + currentRot + 0.01f; //Make sure we rotated by an inbounds angle
        }

        myCamera.transform.Rotate(vertAngle, 0f, 0f); //Rotate the camera around the player vertically

        #endregion
    }
}