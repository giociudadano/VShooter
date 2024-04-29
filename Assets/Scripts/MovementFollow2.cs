using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFollow2 : MonoBehaviour
{
    [SerializeField] private float speed = -2f;
    [SerializeField] private float duration = 0.1f;
    [SerializeField] public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Under Construction
        //StartCoroutine(TowardsNearCenter(duration));
    }

    private IEnumerator TowardsNearCenter(float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            Vector3 startPosition = transform.position;

            while (Time.time - startTime < duration)
            {
                //  Calculate the current position based on time and speed
                float journeyLength = Vector3.Distance(startPosition, targetPosition);
                float coveredDistance = (Time.time - startTime) * speed;
                float fractionOfJourney = coveredDistance / journeyLength;
                transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

                yield return null;
            }

            transform.position = targetPosition;

            //  Wait for the next frame before recalculating the position
            yield return new WaitForEndOfFrame();
        }
    }
}
