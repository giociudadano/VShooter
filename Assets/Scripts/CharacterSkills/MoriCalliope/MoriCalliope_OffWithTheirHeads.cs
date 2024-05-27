using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoriCalliope_OffWithTheirHeads : MonoBehaviour
{   
    //  Maintain a set of colliders that should be debounced
    private HashSet<Collider> debouncedColliders;

    [SerializeField] private float damage = 40f;
    [SerializeField] private float rotateSpeed = 1000f;
    [SerializeField] private float forwardSpeed = 50f;
    private GameObject player;

    private SfxManager sfx;
    [SerializeField] AudioClip launchSfx;

    // Start is called before the first frame update

    void Start()
    {
        debouncedColliders = new HashSet<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        sfx.PlayOneShot(launchSfx);
    }

    void Update()
    {
        SkillTwo();
        Forward();
    }

    private void SkillTwo()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void Forward()
    {
        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;

        if (transform.position.z >= 70)
        {
            forwardSpeed *= -1;
        }
        if (transform.position.z <= player.transform.position.z)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Enemy") && !debouncedColliders.Contains(collision.gameObject.GetComponent<Collider>()))
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().Hurt(damage);
            if (collision.gameObject is not null) {
                Debounce(collision.gameObject.GetComponent<Collider>());
            }
            
        }
        if(collision.gameObject.CompareTag("Boss")  && !debouncedColliders.Contains(collision.gameObject.GetComponent<Collider>()))
        {
            collision.gameObject.GetComponent<BossHealthManager>().Hurt(damage);
            if (collision.gameObject is not null) {
                Debounce(collision.gameObject.GetComponent<Collider>());
            }
        }
        if(collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void Debounce(Collider collider)
    {
        Debug.Log("Debounced collider!");
        debouncedColliders.Add(collider);
    }
}
