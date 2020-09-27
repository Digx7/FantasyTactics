using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDatabase
{
    /*--Description--
      This script should hold all the data on any action that can be taken by any creature
      All the data should be able to be accessed at any time
    */

    /*--Notes--
      Keep this very loose as there may be a better way to do this down the road
      look into data bases for unity as there may be a better way

    */

    public ActionDatabase () {}// will happen every time a new class is made

    public Action callUpAction (string actionName)
    {// (wip) this function contains all the possible actions in the game

      Action act = new Action();

      switch (actionName)
      {
        case "Attack" : // the creature will attack another creature
          act.name = "Attack";
          act.type = "Attack";
          act.damage = 5;
          act.range = 2;
          act.needSelection = true;
          act.maxNumberOfTargets = 1;

          act.animationName = "Attack";
          act.animationAccessID = "isAttacking";
          act.animationAccessType =  "trigger";

          return act;
          break;
        case "Heal" : // the creature will heal itself
          act.name = "Heal";
          act.type = "Spell";
          act.damage = -3;
          act.range = 0;
          act.needSelection = false;
          act.maxNumberOfTargets = 1;

          act.animationName = null;
          act.animationAccessID = null;
          act.animationAccessType =  null;

          return act;
          break;
        case "Wait" : // the creature will do nothing and end its turn
          act.name = "Wait";
          act.type = "Wait";
          act.damage = 0;
          act.range = 0;
          act.needSelection = false;
          act.maxNumberOfTargets = 1;

          act.animationName = null;
          act.animationAccessID = null;
          act.animationAccessType =  null;

          return act;
          break;
        default : // nothing happens *should never be called*
          return null;
          break;
      }
    }
}
