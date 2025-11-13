using System.Collections;
using UnityEngine;

public class Enemy_detection : MonoBehaviour, IDamageable
{

    [SerializeField] protected GameObject kratos;

    [SerializeField] protected GameObject detectionObject;

    protected bool detected = false;

    protected bool dead = false;

    [SerializeField] float deathTimer = 3f;

    // Update is called once per frame
    protected virtual void Update()
    {
        if(dead)
        {
            return;
        }

        if (!detected)
        {
            transform.Rotate(0, 90 * Time.deltaTime, 0);
        }
        else
        {
            transform.LookAt(kratos.transform);
            detected = CanISeePlayer();
            Damage();
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

    protected bool CanISeePlayer()
    {
        RaycastHit hit;
        Vector3 kratosVector =  (kratos.transform.position - detectionObject.transform.position).normalized;

        Physics.Raycast(detectionObject.transform.position, kratosVector, out hit);
        Debug.DrawRay(detectionObject.transform.position, kratosVector, Color.red);

        return hit.collider.gameObject.layer == 3;
    }

    public void Axe()
    {
        dead = true;
        StartCoroutine(DeathTimer());
    }

    protected IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTimer); //Wait for an amount of seconds = to death timer
        Destroy(this.gameObject);
    }

    protected void Damage()
    {
        kratos.GetComponent<Health>().health -= 0.01f;
    }
}

