using UnityEngine;

public class Gate_Moving : MonoBehaviour
{

    [SerializeField] Vector3 gateOffset = new Vector3(0f, 5f, 0f);

    public void Up()
    {
        transform.position += gateOffset;
    }
}
