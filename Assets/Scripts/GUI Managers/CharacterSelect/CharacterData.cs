using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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
						{"title", "Dark Aura"},
						{"icon", "Ina_DarkAura"},
						{"description", "If an enemy is within <color=#AFA>150/200/250</color> units of you, deal <color=#AFA>6/9/12</color> damage per second and slow the target by <color=#AFA>20/30/40%</color>."}
          }},
          {"passive_2", new Dictionary<string, dynamic> () {
						{"title", "Violet Bloom"},
						{"icon", "Ina_VioletBloom"},
						{"description", "Attacks apply a stack of <color=#DAF>Hex</color> for 1.5 seconds, stacking up to 6 times. At max stacks, consume all <color=#DAF>Hex</color> stacks to deal <color=#AFA>6/8/10%</color> of the target's maximum health and <color=#FDA>stun</color> the target for 1.5 seconds (6s cooldown)."}
          }},
          {"passive_3", new Dictionary<string, dynamic> () {
						{"title", "The Ancient One"},
						{"icon", "Ina_TheAncientOne"},
						{"description", "Every <color=#AFA>10/9/8</color> seconds, create a zone that has a <color=#AFA>40/60/80%</color> chance to convert non-boss enemies to <color=#DAF>Takodachis</color>. <color=#DAF>Takodachis</color> have a set amount of health and will seek out and fight random enemies."}
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
						{"title", "Dark Aura"},
						{"icon", "Ina_DarkAura"},
            {"cooldown", 7},
						{"description", "If an enemy is within <color=#AFA>150/200/250</color> units of you, deal <color=#AFA>6/9/12</color> damage per second and slow the target by <color=#AFA>20/30/40%</color>."}
          }},
          {"active_2", new Dictionary<string, dynamic> () {
						{"title", "Violet Bloom"},
						{"icon", "Ina_VioletBloom"},
            {"cooldown", 15},
						{"description", "Attacks apply a stack of <color=#DAF>Hex</color> for 1.5 seconds, stacking up to 6 times. At max stacks, consume all <color=#DAF>Hex</color> stacks to deal <color=#AFA>6/8/10%</color> of the target's maximum health and <color=#FDA>stun</color> the target for 1.5 seconds (6s cooldown)."}
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

