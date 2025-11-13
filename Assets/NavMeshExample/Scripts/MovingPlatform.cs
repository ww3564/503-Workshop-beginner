using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    private float speed;
    [SerializeField] float offsetAmount;
    private Vector3 defaultPosition;
    private Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultPosition = this.transform.position;
        targetPosition = defaultPosition + (Vector3.up * offsetAmount);
        speed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDirection = (targetPosition - this.transform.position).normalized;
        transform.Translate(movementDirection * speed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, targetPosition) < 0.05f)
        {
            targetPosition = targetPosition == defaultPosition ? defaultPosition + (Vector3.up * offsetAmount) : defaultPosition;
            StartCoroutine(Wait());
        }
    }
    
    private IEnumerator Wait()
    {
        speed = 0f;
        yield return new WaitForSeconds(3f);
        speed = defaultSpeed;
    }
}
