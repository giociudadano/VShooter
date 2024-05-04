using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyParticle : MonoBehaviour
{

    
    [SerializeField] private GameObject onDestroyParticle;
    
    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        GameObject instance = Instantiate(onDestroyParticle, transform.position, transform.rotation);
        ParticleSystem particle = instance.GetComponent<ParticleSystem>();
        particle.Play();
    }
}
