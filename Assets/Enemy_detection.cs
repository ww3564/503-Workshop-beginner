using UnityEngine;

public class Enemy_detection : MonoBehaviour
{

    [SerializeField] GameObject Kratos;

    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 360 * Time.deltaTime, 0);

        if (Kratos != null)
        {
            transform.LookAt(target);
        }

        //float playerProximity = Quaternion.Dot(transform.rotation,); (If the player is in the perifiral vision of the enemy)
        //Get a Quaternion that represents the direction of the enemy looking at the player (Not where the enemy is looking right now) and normalize it
        //Check the dot product of the enemies direction (where the enemy is looking at currently) against the direction of the enemy looking at the player (Where I want the enemy to look at)
        //If the dot product is close enough to 1 (based on a float value)
        //  Stop rotating the camera
        //  Make the camera look at the player

    }
}

