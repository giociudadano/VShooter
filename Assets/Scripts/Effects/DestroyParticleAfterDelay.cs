using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticleAfterDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
