using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAmeMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Vector3 moveSide = Vector3.left;
    private Vector3 movementAxisZ = Vector3.forward;
    [SerializeField] private float limitZ = 19.2f;
    private float limitX = 7f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Forward());
        StartCoroutine(SideToSide());
    }

    private IEnumerator Forward()
    {
        while (transform.position.z > limitZ)
        {
            transform.Translate(movementAxisZ * Time.deltaTime * speed);
            //  Wait for the next frame before recalculating the position
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


}
