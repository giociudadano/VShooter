using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalloweenSanaMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Vector3 movementAxisZ = Vector3.forward;
    [SerializeField] private float farZ = 20.0f;
    [SerializeField] private float closeZ = 3.0f;

    [SerializeField] private float movementInterval = 5.0f;

    [SerializeField] private float movementSpeed = 8f;

    private float limitX = 7f;

    private Vector3 moveSide = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(Position());
    }

    private IEnumerator Position()
    {
        while (transform.position.z > farZ)
        {
            transform.Translate(movementAxisZ * Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(BossMovement());
        StartCoroutine(SideToSide());
    }

    private IEnumerator Backward()
    {
        while (transform.position.z < farZ)
        {
            transform.Translate(-movementAxisZ * Time.deltaTime * speed);
            //  Wait for the next frame before recalculating the position
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Forward()
    {
        while (transform.position.z > closeZ) {
            transform.Translate(movementSpeed * Time.deltaTime * movementAxisZ);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator SideToSide()
    {
        while (true)
        {
            transform.Translate(moveSide * Time.deltaTime * speed);

            if (transform.position.x < -limitX)
            {
                moveSide = Vector3.left;
            }
            if (transform.position.x > limitX)
            {
                moveSide = Vector3.right;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator BossMovement()
    {   
        while (true) {
            StartCoroutine(Forward());
            yield return new WaitForSeconds(movementInterval);
            StartCoroutine(Backward());
        }
    }


}
