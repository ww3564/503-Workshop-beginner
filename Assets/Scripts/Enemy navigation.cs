using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyNavigation : Enemy_detection
{
    [SerializeField] Transform[] waypoints;
    int targetIndex;

    [SerializeField] float timed;

    NavMeshAgent nMA;
    bool turning = false;

    private void Start()
    {
        nMA = GetComponent<NavMeshAgent>();
    }



    protected override void Update()
    {
        if (dead)
        {
            return;
        }

        if (!detected)
        {
            //Move from waypoint to waypoint
            nMA.SetDestination(waypoints[targetIndex].position);
            
            if(Vector3.Distance(transform.position, waypoints[targetIndex].position) < 3f && turning == false)
            {
                StartCoroutine(WaitTimer());
            }
            //If close enough to waypont
            //Active Coroutine
            //transform.Rotate(0, 90 * Time.deltaTime, 0);
        }
        else
        {
            //Chase after Kratos and try to deal damage
            transform.LookAt(kratos.transform);
            detected = CanISeePlayer();
            Damage();
            nMA.SetDestination(kratos.transform.position);
        }

        if (kratos != null)
        {
            detectionObject.transform.LookAt(kratos.transform);
            float playerProximity = Mathf.Abs(Quaternion.Dot(transform.rotation.normalized, detectionObject.transform.rotation.normalized));

            if (playerProximity >= 0.9f)
            {
                detected = CanISeePlayer();
            }
        }
    }

    protected IEnumerator WaitTimer()
    {
        turning = true;
        float timer = timed;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            transform.Rotate(0f, (180f * Time.deltaTime) / timed, 0f);
            yield return null;
        }

        targetIndex += 1;

        if(targetIndex > waypoints.Length - 1)
        {
            targetIndex = 0;
        }
        turning = false;

    }
}
