using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoTurret : MonoBehaviour
{   
    [Header("Rotation")]
    [SerializeField] private float leftAngleLimit = 315f;
    [SerializeField] private float rightAngleLimit = 45f;
    [SerializeField] private float speed = 10f;
    private bool isRotating = false; // Initialize to true to start rotating

    [Header("Firing")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private Vector3 shootOffset = new Vector3(0f, 0f, 1f);
    [SerializeField] private float duration = 12f;
    [SerializeField] public float projectileDamage = 20f;
    [SerializeField] private float attackSpeed = 1f;

    [Header("Turret Heal")]
    private float healAmount = 50f;

    private SfxManager sfx;
    private PlayerHealthManager playerHealthManager;
    private Vector3 _eulerAngle;
    private float _yDegrees = 1f;

    void Start()
    {
        _eulerAngle = new Vector3(0, _yDegrees, 0);
        playerHealthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
        StartCoroutine(DestroyTurret());
        StartCoroutine(FireProjectile());
    }

    void Update()
    {
        if (isRotating) {
            Rotate();
            CheckRotation();
        }
    }

    private IEnumerator DestroyTurret()
    {   
        float uptime = 0f;
        while (uptime <= duration) {
            uptime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        };
        playerHealthManager.Heal(healAmount);
        Destroy(gameObject);
    }

    private void Rotate()
    {
        transform.Rotate(_eulerAngle * Time.deltaTime * speed);
    }

    private void CheckRotation() {
        float yRotation = transform.rotation.eulerAngles.y;

        if ((yRotation >= rightAngleLimit) &&
            (yRotation <= leftAngleLimit)) {
            _eulerAngle = -_eulerAngle;
        }
    }

    private IEnumerator FireProjectile()
    {
        yield return new WaitForSeconds(1f);
        while (true) {
            Instantiate(projectile, transform.position + shootOffset, transform.rotation);
            sfx.PlayShootingSfx();
            yield return new WaitForSeconds(1f / attackSpeed);
        }
    }
    public void Frenzy(float bonusAttackSpeedMult = 5.0f)
    {
        attackSpeed *= bonusAttackSpeedMult;
        isRotating = true;
    }
}
