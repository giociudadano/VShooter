using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class UpgradeItemTooltipManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] private UpgradeManager upgradeManager;

  [SerializeField] private GameObject tooltipUI;

  public void OnPointerEnter(PointerEventData eventData) {
		tooltipUI.SetActive(true);
  }

  public void OnPointerExit(PointerEventData eventData) {
    tooltipUI.SetActive(false);
  }

  void Start() {
    tooltipUI.SetActive(false);

		upgradeManager = GameObject.Find("GameManager").GetComponent<UpgradeManager>();
		Dictionary<string, dynamic> upgrade = upgradeManager.upgrades[this.name];
		GameObject image = transform.Find("UpgradeItemTooltip/Image").gameObject;
		image.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>($"Abilities/{upgrade["icon"]}");
		GameObject title = transform.Find("UpgradeItemTooltip/Title").gameObject;
		title.GetComponent<TMP_Text>().text = $"{upgrade["title"]}\n<color=#FFA>LEVEL {upgradeManager.GetCurrentUpgradeLevel(name)}</color>";
    GameObject type = transform.Find("UpgradeItemTooltip/Type").gameObject;
		type.GetComponent<TMP_Text>().text = upgradeManager.ParseAbilityType(upgrade["type"]);
    GameObject description = transform.Find("UpgradeItemTooltip/Description").gameObject;
    description.GetComponent<TMP_Text>().text = upgradeManager.ParseAbilityDescription(name, $"{upgrade["description"]}", upgrade["parameters"]);
  }

  void Update() {
        
  }
}
