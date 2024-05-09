using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CharacterData : MonoBehaviour {
  [SerializeField] public string selectedCharacter;

  void Start() {
    DontDestroyOnLoad(this);
  }

  public Dictionary<string, dynamic> GetPassiveInfo(string character){
		print(character);
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
						{"description", "Defeating an enemy has a <color=#AFA>15/20/25%</color> chance to create an explosion, dealing <color=#AFA>60/80/100</color> damage. Non-boss enemies caught in the explosion have a <color=#AFA>8/10/12%</color> chance of being immediately executed."}
          }},
          {"passive_3", new Dictionary<string, dynamic> () {
						{"title", "End of a Life"},
						{"icon", "Calliope_EndOfALife"},
						{"description", "Attacks apply a <color=#FDA>Burn</color> that deals <color=#AFA>15/25/35</color> damage over 3 seconds. While under the effects of <color=#FDA>Burn</color>, enemies that fall below <color=#AFA>8/12/15%</color> health are immediately executed."}
          }},
        };
      case "NinomaeInanis":
        return new Dictionary<string, dynamic>() {   
          {"passive_1", new Dictionary<string, dynamic> () {
						{"title", "Artist's Dillema"},
						{"icon", "Ina_ArtistsDillema"},
						{"description", "Deal <color=#AFA>6/9/12</color> damage per second and slow by <color=#AFA>20/30/40%</color> to all enemies within <color=#AFA>150/200/250</color> units of you."}
          }},
          {"passive_2", new Dictionary<string, dynamic> () {
						{"title", "Violet"},
						{"icon", "Ina_Violet"},
						{"description", "Attacks apply a stack of <color=#DAF>Hex</color> for 1.5 seconds, stacking up to 6 times. At max stacks, consume all <color=#DAF>Hex</color> stacks to deal <color=#AFA>6/8/10%</color> of the target's maximum health and <color=#FFA>stun</color> the target for 1.5 seconds (6s cooldown)."}
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
}
