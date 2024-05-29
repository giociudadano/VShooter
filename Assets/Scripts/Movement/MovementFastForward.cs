using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFastForward : MonoBehaviour
{
    [SerializeField] private float viewLimitDown = -20f;
    [SerializeField] private float viewLimitUp = 70f;
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float launchSpeed = 55f;
    [SerializeField] private float launchDelay = 1f;
    [SerializeField] private float duration = 2f;
    [SerializeField] public GameObject player;
    private bool readyToLaunch = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Under Construction
        StartCoroutine(MoveForwardAndStop(duration));
    }

    void Update()
    {
        LookAtPlayer();
        Launch();
    }

    private void LookAtPlayer()
    {
        if (readyToLaunch != true)
        {
            transform.LookAt(player.transform);
        }
    }

    private IEnumerator MoveForwardAndStop(float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

            //  Wait for the next frame before recalculating the position
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(launchDelay);
        readyToLaunch = true;
    }

    private void Launch()
    {
        if (readyToLaunch == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * launchSpeed);
            if (transform.position.z < viewLimitDown || transform.position.z > viewLimitUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
