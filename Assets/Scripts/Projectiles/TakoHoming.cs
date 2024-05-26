using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TakoHoming : MonoBehaviour
{
    private GameObject player;
    private List<Collider> collidersInRange = new List<Collider>();

    // Property to get the list of colliders
    public List<Collider> CollidersInRange => collidersInRange;

    private Collider nearestEnemyCollider = null;

    private float nearestDistance = Mathf.Infinity;

    [SerializeField] private float areaSize = 200f;

    [SerializeField] private float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetEnemies();
        FindNearEnemy();
        FollowEnemy();
    }

    private void FindNearEnemy()
    {               
        foreach (Collider collider in collidersInRange)
        {
            // Assuming enemies have a specific tag, you can customize this condition based on your setup
            if (collider.CompareTag("Enemy") || collider.CompareTag("Boss"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < Mathf.Infinity)
                {
                    nearestDistance = distance;
                    nearestEnemyCollider = collider;
                }
            }
        }
    }

    private void GetEnemies()
    {
        if (collidersInRange.Count == 0)
        {
            // Use Physics.OverlapSphere to find all colliders within the radius
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, areaSize);

            // Add them to the list
            collidersInRange.AddRange(hitColliders);
        }
    }

    private void FollowEnemy()
    {
        while (true)
        {
            Vector3 targetPosition = new Vector3(nearestEnemyCollider.transform.position.x, transform.position.y, nearestEnemyCollider.transform.position.z);
            float distance = Vector3.Distance(transform.position, targetPosition);
            float duration = distance / speed;
            transform.LookAt(nearestEnemyCollider.transform);

            float startTime = Time.time;
            Vector3 startPosition = transform.position;

            while (Time.time - startTime < duration)
            {
                //  Calculate the current position based on time and speed
                float journeyLength = Vector3.Distance(startPosition, targetPosition);
                float coveredDistance = (Time.time - startTime) * speed;
                float fractionOfJourney = coveredDistance / journeyLength;
                transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            }
            transform.position = targetPosition;
        }
    }
}
