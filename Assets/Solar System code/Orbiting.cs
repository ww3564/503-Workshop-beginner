using UnityEngine;

public class Orbiting : MonoBehaviour
{

    [SerializeField] float gravitationalConstant;
    [SerializeField] Vector3 startingVelocity;
    Rigidbody rb;
    [SerializeField] Rigidbody[] otherRBs;
    

    void OneEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = startingVelocity;
    }

    void FixedUpdate()
    {
        Vector3 newVelocity = rb.linearVelocity;

        foreach (Rigidbody otherRB in otherRBs)
        {
            newVelocity += CalculateVelocityImpact(otherRB);
        }

        rb.linearVelocity = newVelocity;
    }

    Vector3 CalculateVelocityImpact(Rigidbody otherMass)
    {
        Vector3 pathToOtherMass = otherMass.position - rb.position;
        Vector3 direction = pathToOtherMass.normalized;
        float magnitude = otherMass.mass * gravitationalConstant / Mathf.Pow(pathToOtherMass.magnitude, 2f);
        return direction * magnitude;
    }
}
