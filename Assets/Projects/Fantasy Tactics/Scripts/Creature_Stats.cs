using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_Stats : MonoBehaviour
{
  /*--Description--
    This script will hold all the stats of any creature (enemeys or players) on the map.
  */

  /*--Notes--
    This should be nothing more than a complex spread sheet
  */

  //meta Info
  [Header ("Meta Info")]
  public int currentX;
  public int currentY;
  public int currentZ;
  [Space]
  public int endX;
  public int endY;
  public int endZ;
  [Space]
  public bool isEnemy = false;

  //info
  [Header ("Info")]
  public string Name;
  public int level;
  public string race;
  public string clas;
  [Space]

  //battle stat
  [Header ("Battle Stats")]
  public int armorClass;
  public int initiative;
  public int speed;
  public int maxHealth;
  public int currentHealth;
  public int deathSaveSuccesses;
  public int deathSaveFailures;
  [Space]

  //actions
  [Header ("Actions")]
  public int numOfMaxActions;
  public string[] actions;
  [Space]

  //main stats
  [Header ("Main Stats")]
  public int strength;
  public int dexterity;
  public int constitution;
  public int intelligence;
  public int wisdom;
  public int charisma;
  public int proficiencyBonus;
  public int passivePerception;
  public string[] savingThrows;
  public string[] skills;
  [Space]

  //other proficencies
  [Header ("Proficencies")]
  public string[] proficiencies;
  [Space]

  //features & traits
  [Header ("Traits")]
  public string[] traits;
}
