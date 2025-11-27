using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("SavingData");
            Saving_and_Loading.SaveCurrentData();
        }
    }
}
