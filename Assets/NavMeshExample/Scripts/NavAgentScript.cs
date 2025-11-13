using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NavAgentScript : MonoBehaviour
{
    [SerializeField] NavPoint myNavPoint;
    NavMeshAgent nMA;

    void Start()
    {
        nMA = GetComponent<NavMeshAgent>();
    }

    public void UpdateNavPoint(InputAction.CallbackContext inputData)
    {
        if (!inputData.started)
        {
            return;
        }
        
        nMA.SetDestination(myNavPoint.transform.position);
    }
}
