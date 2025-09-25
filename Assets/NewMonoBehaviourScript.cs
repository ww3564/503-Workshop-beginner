using System.Net.Cache;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject Cube;
    [SerializeField] Vector3 initialPosition;

    [SerializeField] float lerpAmount;

    [SerializeField] float lerpTime = 1f;


    [SerializeField] Vector3 vectorPath;

    private bool recalling = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        vectorPath = Cube.transform.position - transform.position;

        if (recalling)
        {
            transform.position = Vector3.Slerp(initialPosition, Cube.transform.position, lerpAmount);
            lerpAmount += ((1f / lerpTime) * Time.deltaTime);
        }
    }

    public void Recall()
    {
        initialPosition = transform.position;
        lerpAmount = 0f;
        recalling = true;
    }
}