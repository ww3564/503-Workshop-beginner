using UnityEngine;

public class Orbiting : MonoBehaviour
{

    [SerializeField] float gravitationalConstant;
    [SerializeField] Vector3 startingVelocity;
    [SerializeField] Rigidbody rb;
    [SerializeField] Rigidbody[] otherRBs;
    
    void OnEnable()
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

        //for (int i = 0; i < otherRBs.Length; i++)
        //{
        //    newVelocity += CalculateVelocityImpact(otherRBs[i]);
        //}
        
        //for (int i = otherRBs.Length - 1; i >= 0; i--)
        //{
        //    
        //}

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
