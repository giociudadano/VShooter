using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAmeAttack : MonoBehaviour
{
    [SerializeField] private float missileInterval;
    [SerializeField] private float missileDelay;
    [SerializeField] private GameObject missile;
    [SerializeField] private int missileCount;
    [SerializeField] private float missileLaunchDelay = 0.1f;
    [SerializeField] private Vector3 missileSpawnOffset;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Missiles", missileDelay, missileInterval);
    }

    private void Missiles()
    {
        StartCoroutine(LaunchMissiles());
    }

    private IEnumerator LaunchMissiles()
    {
        for (int i = 0; i < missileCount; i++)
        {
            Vector3 spawnPosition = transform.position + (missileSpawnOffset * i);
            Instantiate(missile, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(missileLaunchDelay);
        }
    }


}
