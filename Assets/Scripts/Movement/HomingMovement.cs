using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMovement : MonoBehaviour
{
    // Manages objects moving up and down the screen.

    [SerializeField] private float speed = 8f;
    [SerializeField] private float viewLimitDown = -5f;

    [SerializeField] private float viewLimitUp = 50f;
    [SerializeField] public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(MoveObject());
    }

    void Update()
    {

        //DeleteObject();
    }

    //  We don't use Vecto3.Lerp() here since we don't need fancy swerving/acceration/retargetting
    private IEnumerator MoveObject()
    {

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.up = player.transform.position - transform.position;

            //  Wait for the next frame before recalculating the position
            yield return new WaitForEndOfFrame();
        }
    }
}
