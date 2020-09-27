using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
  /*--Description--
    This custom class will be a template for any and all actions that
    any creature can take
  */

  /*--Notes--
    This is highly experinmental and unknown territory for me
  */

  // stats
  public string name; // the name of the given action
  public string type; // what type of action it is
  public int damage; // how much damage that action does, negative damage will heal
  public int range; // how far away that attack must be
  public bool needSelection; // does it require player input to choose a target
  public int maxNumberOfTargets; // how many targets the player can choose

  //polish
  public string animationName; // what animation it plays
  public string animationAccessID; // the name of the parameter that will activate the animation
  public string animationAccessType; // the type of paraperter for the action


  // this funtion will be called every time that an action is Instantiated
  public Action () {} // will happen every time a new class is made
}
