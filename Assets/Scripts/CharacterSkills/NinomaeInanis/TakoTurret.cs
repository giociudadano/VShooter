using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TakoTurret : MonoBehaviour
{
    private float leftAngleLimit = 315f;
    private float rightAngleLimit = 45f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float duration = 7f;
    private float time;

    void Start()
    {
        TimeStamp();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        TimeCheck();
    }

    private void TimeStamp()
    {
        time = Time.time;
    }

    private void TimeCheck()
    {
        if (Time.time >= time + duration)
        {
            Destroy(gameObject);
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);

        // The speed shifts if it hits the 45 degrees of each side.
        if(Mathf.Abs(transform.rotation.eulerAngles.y)>= rightAngleLimit && 
            Mathf.Abs(transform.rotation.eulerAngles.y)<= leftAngleLimit) {
            speed*=-1;
        }
    }
}
