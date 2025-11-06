using System.Collections;
using UnityEngine;

public class Enemy_detection : MonoBehaviour, IDamageable
{

    [SerializeField] GameObject kratos;

    [SerializeField] GameObject detectionObject;

    private bool detected = false;

    private bool dead = false;

    [SerializeField] float deathTimer = 3f;

    // Update is called once per frame
    void Update()
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



        //float playerProximity = Quaternion.Dot(transform.rotation,); (If the player is in the perifiral vision of the enemy)
        //Get a Quaternion that represents the direction of the enemy looking at the player (Not where the enemy is looking right now) and normalize it
        //Check the dot product of the enemies direction (where the enemy is looking at currently) against the direction of the enemy looking at the player (Where I want the enemy to look at)
        //If the dot product is close enough to 1 (based on a float value)
        //  Stop rotating the camera
        //  Make the camera look at the player

    }

    private bool CanISeePlayer()
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

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTimer); //Wait for an amount of seconds = to death timer
        Destroy(this.gameObject);
    }

    private void Damage()
    {
        kratos.GetComponent<Health>().health -= 0.01f;
    }
}

