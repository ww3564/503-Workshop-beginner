using UnityEngine;

public class Unlocking_mouse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }
}
