using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardAndDodgeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] public GameObject player;
    private Vector3 moveSide = Vector3.left;
    private Vector3 movementAxisZ = Vector3.forward;
    private float viewLimitXAxis = 7f;
    private float viewLimitDown = 5f;
    private float viewLimitUp = 40f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ForwardAndBack());
        StartCoroutine(Dodge());
    }

    // Update is called once per frame
    private IEnumerator ForwardAndBack()
    {
        while (true)
        {
            transform.Translate(movementAxisZ * Time.deltaTime * speed);
            
            if (transform.position.z < viewLimitDown)
            {
                movementAxisZ = Vector3.back;
            }
            if (transform.position.z > viewLimitUp)
            {
                movementAxisZ = Vector3.forward;
            }

            //  Wait for the next frame before recalculating the position
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Dodge()
    {
        while (true)
        {
            transform.Translate(moveSide * Time.deltaTime * speed);

            if (transform.position.x < -viewLimitXAxis)
            {
                moveSide = Vector3.left;
            }
            if (transform.position.x > viewLimitXAxis)
            {
                moveSide = Vector3.right;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
