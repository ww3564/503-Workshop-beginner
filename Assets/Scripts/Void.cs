using UnityEngine;
using UnityEngine.SceneManagement;

public class Void : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
