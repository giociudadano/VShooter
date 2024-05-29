using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {
  [SerializeField] public string selectedCharacter;

  // NOTE: INDEXING IS HARD CODED
  public GameObject[] skillPrefabs;

  void Start() {
    DontDestroyOnLoad(this);
  }

  public Dictionary<string, dynamic> GetPassiveInfo(string character){
    switch (character) {
      case "MoriCalliope":
        return new Dictionary<string, dynamic>() {
          {"passive_1", new Dictionary<string, dynamic> () {
						{"title", "Soul Harvester"},
						{"icon", "Calliope_SoulHarvester"},
						{"description", "Defeating an enemy has a <color=#AFA>30/40/50%</color> chance to restore <color=#AFA>20/30/40HP</color>."}
          }},
          {"passive_2", new Dictionary<string, dynamic> () {
						{"title", "Taste of Death"},
						{"icon", "Calliope_TasteOfDeath"},
						{"description", "Defeating an enemy has a <color=#AFA>15/20/25%</color> chance to create an explosion, dealing <color=#AFA>60/80/100</color> damage. Non-boss enemies caught in the explosion have a <color=#AFA>8/10/12%</color> chance of being immediately <color=#FDA>executed</color>."}
          }},
          {"passive_3", new Dictionary<string, dynamic> () {
						{"title", "End of a Life"},
						{"icon", "Calliope_EndOfALife"},
						{"description", "Attacks apply a <color=#FDA>Burn</color> that deals <color=#AFA>15/25/35</color> damage over 3 seconds. While under the effects of <color=#FDA>Burn</color>, enemies that fall below <color=#AFA>8/12/15%</color> health are immediately <color=#FDA>executed</color>."}
          }},
        };
      case "NinomaeInanis":
        return new Dictionary<string, dynamic>() {   
          {"passive_1", new Dictionary<string, dynamic> () {
						{"title", "Blessings of the Gods"},
						{"icon", "Ina_BlessingsOfTheGods"},
						{"description", "Every <color=#AFA>14/12/10</color> seconds, summon a <color=#DAF>Takodachi</color> at the player's location for 12 seconds."}
          }},
          {"passive_2", new Dictionary<string, dynamic> () {
						{"title", "Placeholder 2"},
						{"icon", "Ina_DarkAura"},
						{"description", "Placeholder 2 Description"}
          }},
          {"passive_3", new Dictionary<string, dynamic> () {
						{"title", "Placeholder 3"},
						{"icon", "Ina_DarkAura"},
						{"description", "Placeholder 3 Description"}
          }},
        };  
      default:
        throw new ArgumentException("Character could not be found", character);
  	}
  }

  public Dictionary<string, dynamic> GetActiveInfo(string character){
    switch (character) {
      case "MoriCalliope":
        return new Dictionary<string, dynamic>() {
          {"active_1", new Dictionary<string, dynamic> () {
						{"title", "Excuse My Rudeness"},
						{"icon", "Calliope_ExcuseMyRudeness"},
            {"cooldown", 7},
						{"description", "Calliope spins her scythe in a circle dealing <color=#FFA>60</color> damage to all surrounding enemies. For each enemy hit, heal for <color=#AFA>10HP</color> (up to <color=#AFA>60HP</color> total)."}
          }},
          {"active_2", new Dictionary<string, dynamic> () {
						{"title", "Off With Their Heads"},
						{"icon", "Calliope_OffWithTheirHeads"},
            {"cooldown", 15},
						{"description", "Calliope consumes <color=#AFA>20%</color> of her own Maximum HP and throws her scythe forward, dealing <color=#FFA>100</color> damage to all enemies hit."}
          }},
        };
      case "NinomaeInanis":
        return new Dictionary<string, dynamic>() {   
          {"active_1", new Dictionary<string, dynamic> () {
						{"title", "Summon: Takodachi"},
						{"icon", "Ina_SummonTakodachi"},
            {"cooldown", 10},
						{"description", "Summons a <color=#DAF>Takodachi</color> at the target location for 12 seconds. <color=#DAF>Takodachis</color> deal <color=#FFA>30</color> damage to all enemies hit and restores <color=#AFA>20HP</color> to the player on expiry."}
          }},
          {"active_2", new Dictionary<string, dynamic> () {
						{"title", "The Ancient One"},
						{"icon", "Ina_TheAncientOne"},
            {"cooldown", 20},
						{"description", "All summoned <color=#DAF>Takodachis</color> gain <color=#FCA>Frenzy</color> for 5 seconds. <color=#FCA>Frenzied</color> summons gain <color=#FFA>500% Attack Speed</color> but shoot in a spray direction."}
          }},
        };  
      default:
        throw new ArgumentException("Character could not be found", character);
  	}
  }

  public Dictionary<string, GameObject> GetSkillPrefab(string character){
    switch (character){
      case "MoriCalliope":
        return new Dictionary<string, GameObject> {
          {"skill1", skillPrefabs[1]},
          {"skill2", skillPrefabs[0]},
        };
      case "NinomaeInanis":
        return new Dictionary<string, GameObject> {
          {"skill1", skillPrefabs[2]},
          {"skill2", skillPrefabs[3]},
        };
      default:
        throw new ArgumentException("Character could not be found", character);
    }
  } 
}

