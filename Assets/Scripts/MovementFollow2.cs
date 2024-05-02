using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFollow2 : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float duration = 1f;
    [SerializeField] public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Under Construction
        StartCoroutine(TowardsNearCenter(duration));
    }

    private IEnumerator TowardsNearCenter(float duration)
    {
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.LookAt(player.transform);

            //  Wait for the next frame before recalculating the position
            yield return new WaitForEndOfFrame();
        }
    }
}
