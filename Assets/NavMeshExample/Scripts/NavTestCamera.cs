using UnityEngine;
using UnityEngine.InputSystem;

public class NavTestCamera : MonoBehaviour
{

    [SerializeField] Transform worldCenter;
    [SerializeField] NavPoint navPoint;
    float movementFloat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.RotateAround(worldCenter.position, Vector3.up, movementFloat * Time.deltaTime * 50f);
    }

    public void SetMovementFloat(InputAction.CallbackContext inputData)
    {
        movementFloat = inputData.ReadValue<float>();
    }

    public void SetNavigationPoint(InputAction.CallbackContext inputData)
    {
        if (!inputData.started)
        {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Vector3 rayDirection = (mousePos - transform.position).normalized;

        RaycastHit hitInfo;
        Physics.Raycast(transform.position, rayDirection, out hitInfo, 100f);
        
        if(hitInfo.collider != null)
        {
            navPoint.transform.position = hitInfo.point;
        }
    }

}
