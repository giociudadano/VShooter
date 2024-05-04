using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{   
    [SerializeField] private GameObject explosionGrey;
    [SerializeField] private GameObject explosionGreen;
    [SerializeField] private GameObject explosionRed;
    [SerializeField] private GameObject explosionBlack;
    [SerializeField] private GameObject explosionOrange;
    [SerializeField] private GameObject explosionYellow;
    [SerializeField] private GameObject explosionBlue;
    [SerializeField] private GameObject explosionBrown;
    [SerializeField] private GameObject explosionPink;
    [SerializeField] private GameObject explosionPurple;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGreyExplosion(Vector3 position, Quaternion rotation)
    {
        PlayGenericExplosion(explosionGrey, position, rotation);
    }

    public void PlayGreenExplosion(Vector3 position, Quaternion rotation)
    {
        PlayGenericExplosion(explosionGreen, position, rotation);
    }

    public void PlayRedExplosion(Vector3 position, Quaternion rotation)
    {
        PlayGenericExplosion(explosionRed, position, rotation);
    }

    public void PlayBlackExplosion(Vector3 position, Quaternion rotation)
    {
        PlayGenericExplosion(explosionBlack, position, rotation);
    }

    public void PlayOrangeExplosion(Vector3 position, Quaternion rotation)
    {
        PlayGenericExplosion(explosionOrange, position, rotation);
    }

    public void PlayYellowExplosion(Vector3 position, Quaternion rotation)
    {
        PlayGenericExplosion(explosionYellow, position, rotation);
    }


    //  Generic function for instantiating and playing particle effects (particles are assumed to be destroyed in their settings)
    private void PlayGenericExplosion(GameObject particle, Vector3 position, Quaternion rotation)
    {
        GameObject instance = Instantiate(particle, position, rotation);
        ParticleSystem explosion = instance.GetComponent<ParticleSystem>();
        explosion.Play();
    }

}
