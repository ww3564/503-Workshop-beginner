using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Axe : MonoBehaviour
{
    [SerializeField] GameObject hand;
    [SerializeField] Vector3 initialPosition;
    [SerializeField] Camera cam;

    [SerializeField] float lerpAmount;

    [SerializeField] float lerpTime = 1f;


    [SerializeField] Vector3 vectorPath;

    Rigidbody rb;
    [SerializeField] float forceValue;

    public enum axeState { throwing, returning, held, hitSomething };
    public axeState state;


    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == axeState.held)
        {
            transform.position = hand.transform.position;
            transform.rotation = hand.transform.rotation;
        }
    }

    public void RecallInput(InputAction.CallbackContext inputData)
    {
        if (inputData.started && (state != axeState.returning && state != axeState.held))
        {
            StopAllCoroutines();
            StartCoroutine(Recall());
        }
    }

    public void ThrowInput(InputAction.CallbackContext inputData)
    {
        if (inputData.started && (state == axeState.held))
        {
            state = axeState.throwing;
            rb.isKinematic = false;
            rb.AddForce(cam.transform.forward * forceValue, ForceMode.Impulse);
        }
    }

    private IEnumerator Recall()
    {
        state = axeState.returning;

        initialPosition = transform.position;

        for (float i = 0; i < lerpTime; i += Time.deltaTime)
        {
            Vector3 targetPosition = hand.transform.position;
            transform.position = Vector3.Slerp(initialPosition, targetPosition, i / lerpTime);
            yield return null;
        }

        state = axeState.held;
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision col)
    {
        rb.isKinematic = true;
        state = axeState.hitSomething;

        Debug.Log(col.gameObject);

        if (col.gameObject.GetComponent<IDamageable>() != null)
        {
            col.gameObject.GetComponent<IDamageable>().Axe();
        }

        if (col.gameObject.transform.parent == null)
        {
            return;
        }

        if (col.gameObject.transform.parent.GetComponent<IDamageable>() != null)
        {
            col.gameObject.transform.parent.GetComponent<IDamageable>().Axe();
        }
    }
}