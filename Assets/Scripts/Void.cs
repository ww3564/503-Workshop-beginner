using UnityEngine;
using UnityEngine.SceneManagement;

public class Void : MonoBehaviour
{
    [SerializeField] bool is_ending;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (!is_ending)
            {
                col.gameObject.GetComponent<Health>().health = 0;
            }
            else
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
