using UnityEngine;

public class Gate_Opener : MonoBehaviour, IDamageable
{

    [SerializeField] Gate_Moving myGate;

    private float numb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Axe()
    {
        numb += 1f;

        if (numb > 1f)
        {
            return;
        }

        myGate.Up();
        
    }
}
