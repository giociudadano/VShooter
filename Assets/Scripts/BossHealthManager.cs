using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class BossHealthManager : MonoBehaviour
{
    [Header("Managers")]
    private UpgradeManager upgradeManager;
    private PlayerXPManager playerXPManager;
    private SfxManager sfx;

    [Header("GUI")]
    public GameObject gameManager;
    public TMP_Text healthText;
    public GameObject healText;
    public GameObject healthBar;

    [Header("Damage Text")]
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject damageTextBurn;
    [SerializeField] private GameObject damageTextCritical;

    [Header("Boss Stats")]
    [SerializeField] public float currentHealth;
    [SerializeField] private float baseHealth = 300;
    [SerializeField] public float maximumHealth;
    [SerializeField] private float XPReward = 15f;

    [Header("Defense")]
    [SerializeField] public float defense;
    [SerializeField] private float baseDefense = 0;

    [Header("Health Regen")]
    [SerializeField] private float healthRegen = 0f;

    private GameObject player;

    [Header("Effects")]
    private bool isBurning = false;


    // Start is called before the first frame update
    void Start()
    {
        maximumHealth = baseHealth;
        currentHealth = maximumHealth;
        defense = baseDefense;
        UpdateHealthbar();

        player = GameObject.FindGameObjectWithTag("Player");
        upgradeManager = gameManager.GetComponent<UpgradeManager>();
        playerXPManager = player.GetComponent<PlayerXPManager>();
        sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
    }

    private void UpdateHealthbar() {
      healthText.text = Mathf.CeilToInt(currentHealth).ToString() + " / " + maximumHealth.ToString();
      healthBar.transform.localScale = new Vector3(currentHealth/maximumHealth, 1f, 1f);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("AllyProjectile")){
            Destroy(collision.gameObject);
            float projectileDamage = GetProjectileDamage(collision.gameObject.GetComponent<BasicProjectile>().projectileDamage, upgradeManager.GetComponent<UpgradeManager>().bonusAttackPercent);
            Hurt(collision.gameObject, projectileDamage, 0.1f);
        }
    }

    private float GetProjectileDamage(float basicDamage, float bonusAttackPercent = 0){
        float totalDamage = Random.Range(basicDamage * 0.9f, basicDamage * 1.1f);
        totalDamage *= 1 + bonusAttackPercent;
        return totalDamage;
    }

    public void Hurt(float rawDamage) {
        float netDamage = rawDamage * (100 / (100 + defense));
        currentHealth -= netDamage;
        if (currentHealth <= 0) {
            Kill();
        }
        GameObject damagePopup = Instantiate(damageTextBurn, new Vector3(gameObject.transform.position.x + 0.2f, 2f, gameObject.transform.position.z), Quaternion.identity);
        damagePopup.transform.Find("DamageText").GetComponent<TMP_Text>().text = rawDamage.ToString("0");
        UpdateHealthbar();
    }

    public void Hurt(GameObject source, float rawDamage, float critChance) {
        //sfx.PlayImpactSfx();
        // Net Damage Calculation
        float critRoll = Random.Range(0f, 1f);
        if (critRoll < critChance){
        rawDamage *= 1.5f;
        GameObject damagePopup = Instantiate(damageTextCritical, new Vector3(gameObject.transform.position.x + 0.2f, 2f, gameObject.transform.position.z), Quaternion.identity);
        damagePopup.transform.Find("DamageText").GetComponent<TMP_Text>().text = rawDamage.ToString("0") + "!";
        } else {
        GameObject damagePopup = Instantiate(damageText, new Vector3(gameObject.transform.position.x + 0.2f, 2f, gameObject.transform.position.z), Quaternion.identity);
        damagePopup.transform.Find("DamageText").GetComponent<TMP_Text>().text = rawDamage.ToString("0");
        }
        currentHealth -= rawDamage;
        if (currentHealth <= 0f){
            Kill(source);
        }

        // Upgrade effects
        Dictionary<string, dynamic> onHitPassives = new Dictionary<string, dynamic>() {
            {"MORICALLIOPE_ENDOFALIFE", new Dictionary<string, dynamic>() {
            {"source", gameObject}
            }}
        };
        upgradeManager.ApplyPassive(onHitPassives);
        UpdateHealthbar();
    }
    public void Kill() {
        playerXPManager.GainXP(XPReward);
        Destroy(gameObject);
    }

    public void Kill(GameObject source) {
        // Apply only these passives to projectile kills.
        if (source.CompareTag("AllyProjectile")){
        Dictionary<string, dynamic> onKillPassives = new Dictionary<string, dynamic>() {
            {"MORICALLIOPE_SOULHARVESTER", null},
            {"MORICALLIOPE_TASTEOFDEATH", new Dictionary<string, dynamic>() {
            {"source", gameObject}
            }}
        };
        upgradeManager.ApplyPassive(onKillPassives);
        }
        playerXPManager.GainXP(XPReward);
        gameObject.GetComponent<BossUIManager>().BossUI.SetActive(false);
        //sfx.PlayKillSfx();
        Destroy(gameObject);
    }
    
    public void ApplyBurn(float burnDamage, float effectDuration) {
        if (isBurning) {
        StopCoroutine("BurnTick");
        }
        StartCoroutine(BurnTick(burnDamage, effectDuration));
    }

    public IEnumerator BurnTick(float burnDamage, float effectDuration) {
        isBurning = true;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++){
        Hurt(burnDamage / 3);
        yield return new WaitForSeconds(effectDuration / 3);
        }
        isBurning = false;
    }

    public void ApplyExecuteThreshold(float executionThreshold) {
        StartCoroutine(ExecuteThreshold(executionThreshold));
    }

    private IEnumerator ExecuteThreshold(float executionThreshold) {
    while (isBurning) {
      if ((currentHealth / maximumHealth) < executionThreshold) {
        Kill();
      }
      yield return new WaitForSeconds(0.5f);
    };
  }

}
