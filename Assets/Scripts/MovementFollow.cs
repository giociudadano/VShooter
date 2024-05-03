using System.Collections;
using UnityEngine;

public class MovementFollow : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FollowPlayer());
    }

    //  Ren's notes: Refactored into coroutine to get more performance, since updating dozens of objects per frame with Update() doesn't seem good
    //  This also has the side-effect of emulating how the zombies move in the ads --- gradually in a straight line then swerving towards the player near the end
    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            float distance = Vector3.Distance(transform.position, targetPosition);
            float duration = distance / speed;
            transform.LookAt(player.transform);

            float startTime = Time.time;
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
