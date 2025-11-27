using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    [SerializeField] Slider healthSlider;

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp01(health);
        healthSlider.value = health;

        if (Mathf.Clamp01(health) == 0f)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
