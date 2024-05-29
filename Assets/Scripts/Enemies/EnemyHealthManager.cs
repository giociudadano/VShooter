using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthManager : MonoBehaviour
{
  // Manages health of the object.

  [Header("Managers")]
  private UpgradeManager upgradeManager;
  private PlayerXPManager playerXPManager;
  private GameObject gameManager;
  private SfxManager sfx;

  [Header("GUI")]
  [SerializeField] private GameObject canvas;
  [SerializeField] private GameObject healthbar;

  [Header("Damage Text")]
  [SerializeField] private GameObject damageText;
  [SerializeField] private GameObject damageTextBurn;
  [SerializeField] private GameObject damageTextCritical;

  [Header("Enemy Stats")]
  [SerializeField] private float maxHealth = 3f;
  [SerializeField] private float currentHealth;
  [SerializeField] private float XPReward = 5f;
  private GameObject player;


  [Header("Effects")]
  private bool isBurning = false;


  void Start()
  {
    currentHealth = maxHealth;
    canvas.SetActive(false);
    player = GameObject.FindGameObjectWithTag("Player");
    gameManager = GameObject.FindGameObjectWithTag("GameManager");
    upgradeManager = gameManager.GetComponent<UpgradeManager>();
    playerXPManager = player.GetComponent<PlayerXPManager>();
    sfx = GameObject.FindGameObjectWithTag("SfxPlayer").GetComponent<SfxManager>();
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("AllyProjectile"))
    {
      Destroy(collision.gameObject);
      float projectileDamage = GetProjectileDamage(collision.gameObject.GetComponent<BasicProjectile>().projectileDamage, upgradeManager.GetComponent<UpgradeManager>().bonusAttackPercent);
      Hurt(collision.gameObject, projectileDamage, 0.1f);
    }
  }

  private float GetProjectileDamage(float basicDamage, float bonusAttackPercent = 0)
  {
    float totalDamage = Random.Range(basicDamage * 0.9f, basicDamage * 1.1f);
    totalDamage *= 1 + bonusAttackPercent;
    return totalDamage;
  }

  public void Hurt(float rawDamage)
  {
    currentHealth -= rawDamage;
    if (currentHealth <= 0f)
    {
      Kill();
    }

    // Health bar and damage text display
    canvas.SetActive(true);
    healthbar.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    GameObject damagePopup = Instantiate(damageTextBurn, new Vector3(gameObject.transform.position.x + 0.2f, 2f, gameObject.transform.position.z), Quaternion.identity);
    damagePopup.transform.Find("DamageText").GetComponent<TMP_Text>().text = rawDamage.ToString("0");
  }

  public void Hurt(GameObject source, float rawDamage, float critChance)
  {
    sfx.PlayImpactSfx();
    // Net Damage Calculation
    float critRoll = Random.Range(0f, 1f);
    float bonusCritRate = upgradeManager.GetComponent<UpgradeManager>().bonusCritRate;
    if (critRoll < (critChance + bonusCritRate))
    {
      float bonusCritDamage = upgradeManager.GetComponent<UpgradeManager>().bonusCritDamage;
      rawDamage *= (1.5f + bonusCritDamage);
      GameObject damagePopup = Instantiate(damageTextCritical, new Vector3(gameObject.transform.position.x + 0.2f, 2f, gameObject.transform.position.z), Quaternion.identity);
      damagePopup.transform.Find("DamageText").GetComponent<TMP_Text>().text = rawDamage.ToString("0") + "!";
    }
    else
    {
      GameObject damagePopup = Instantiate(damageText, new Vector3(gameObject.transform.position.x + 0.2f, 2f, gameObject.transform.position.z), Quaternion.identity);
      damagePopup.transform.Find("DamageText").GetComponent<TMP_Text>().text = rawDamage.ToString("0");
    }
    currentHealth -= rawDamage;
    if (currentHealth <= 0f)
    {
      Kill(source);
    }

    // Upgrade effects
    Dictionary<string, dynamic> onHitPassives = new Dictionary<string, dynamic>() {
        {"MORICALLIOPE_ENDOFALIFE", new Dictionary<string, dynamic>() {
          {"source", gameObject}
        }}
    };
    upgradeManager.ApplyPassive(onHitPassives);

    // Health bar and damage text display
    canvas.SetActive(true);
    healthbar.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);

  }

  public void Kill()
  {
    playerXPManager.GainXP(XPReward);
    Destroy(gameObject);
  }

  public void Kill(GameObject source)
  {
    // Apply only these passives to projectile kills.
    if (source.CompareTag("AllyProjectile"))
    {
      Dictionary<string, dynamic> onKillPassives = new Dictionary<string, dynamic>() {
        {"MORICALLIOPE_SOULHARVESTER", null},
        {"MORICALLIOPE_TASTEOFDEATH", new Dictionary<string, dynamic>() {
          {"source", gameObject}
        }}
      };
      upgradeManager.ApplyPassive(onKillPassives);
    }
    playerXPManager.GainXP(XPReward);
    sfx.PlayKillSfx();
    Destroy(gameObject);
  }

  public void ApplyBurn(float burnDamage, float effectDuration)
  {
    if (isBurning)
    {
      StopCoroutine("BurnTick");
    }
    StartCoroutine(BurnTick(burnDamage, effectDuration));
  }

  public IEnumerator BurnTick(float burnDamage, float effectDuration)
  {
    isBurning = true;
    yield return new WaitForSeconds(0.5f);
    for (int i = 0; i < 3; i++)
    {
      Hurt(burnDamage / 3);
      yield return new WaitForSeconds(effectDuration / 3);
    }
    isBurning = false;
  }


  public void ApplyExecuteThreshold(float executionThreshold)
  {
    StartCoroutine(ExecuteThreshold(executionThreshold));
  }

  private IEnumerator ExecuteThreshold(float executionThreshold)
  {
    while (isBurning)
    {
      if ((currentHealth / maxHealth) < executionThreshold)
      {
        Kill();
      }
      yield return new WaitForSeconds(0.5f);
    };
  }

  public void Heal(float healAmount)
  {
    currentHealth += healAmount;
    if (currentHealth > maxHealth)
    {
      currentHealth = maxHealth;
      canvas.SetActive(false);
    }
    healthbar.transform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
  }
}


