using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Character_Turn_Manager : MonoBehaviour
{
  /*--Description--
    This script should hold and manage everything that a creature can do on its turn
  */

  /*--Notes--
    This may need the most reworking as I balance and rework the game
    keep modular
  */

    [Header ("Meta")]
    public bool isTurn = false;
    [Space]
    [Header ("Movement")]
    public int maxMovement = 4;
    public int remainingMovement = 2;
    [Space]
    [Header ("Actions")]
    public int numOfMaxActions = 1;
    public int numOfAvailibleActions = 0;
    [Space]
    public int numOfMaxBonusActions = 1;
    public int numOfAvailibleBonusActions = 0;
    [Space]
    public bool requestSelection = false;
    public Action currentAct = null;
    public bool foundTarget = false;
    public bool isInMovementMode = false;
    [Space]
    [Header("Stats")]
    public Creature_Stats Creature_Stats;
    public Grid_Character_Model_Movement Grid_Character_Model_Movement;
    public Grid_Turn_Manager Grid_Turn_Manager;

    // ------------------- meta stuff -------------------------------

    public void syncSystems ()
    {//(wip) this funtion will set up any needed stats in this script, should only be called once

      // sets up the movement speed
      maxMovement = Creature_Stats.speed;

      GameObject[] g;
      g = GameObject.FindGameObjectsWithTag("ScriptHolder");
      Grid_Turn_Manager = g[0].GetComponent<Grid_Turn_Manager>();
    }

    // ------------------ turn management ---------------------------

    public void startTurn ()
    {//(wip) will reset all of a creatures stats at start of turn, will be called at start of turn

      remainingMovement = maxMovement;
      numOfAvailibleActions = numOfMaxActions;
      numOfAvailibleBonusActions = numOfMaxBonusActions;
      isInMovementMode = true;

      Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

      // will pull up area of movement for that creature
      Grid_Character_Movement.findAreaOfMovement(currentX: Creature_Stats.currentX, currentY: Creature_Stats.currentY, currentZ: Creature_Stats.currentZ, numOfSteps: maxMovement);

      isTurn = true;
    }

    private bool endOfTurn ()
    {//(wip) this script will check all the end of turn conditions
      if (remainingMovement == 0 && numOfAvailibleActions == 0)
      {

        Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

        Grid_Character_Movement.resetAreaOfMovement();

        return true;
      }
      else
      {
        return false;
      }
    }

    void Update ()
    {//(wip) this script will end the creaturs turn if the conditions are met
      if (isTurn)
      {
        if (endOfTurn())
        {

          print ("This is the end of this creatures turn");

          isTurn = false;
          Grid_Turn_Manager.currentTurn++;
          Grid_Turn_Manager.nextTurn();

        }
      }
    }

    // ------------------ ui/options per turn -------------------------


    public void moveCreature (int x, int y, int z)
    {//(wip) this script will be called when a creature inputs a move command

      Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

      if (isTurn)
      {
        if (remainingMovement > 0)
        {
          if (Grid_Character_Movement.canMoveToSpace(endX: x, endY: y, endZ: z))
          {

            //animate character moving

            int startX = Creature_Stats.currentX;
            int startY = Creature_Stats.currentY;
            int startZ = Creature_Stats.currentZ;

            GameObject[] path = Grid_Character_Movement.findPathOfMovement (startX: startX, startY: startY, startZ: startZ, endX: x, endY: y, endZ: z);

            Grid_Character_Model_Movement.startModelMoveAnimation(path);
            Grid_Character_Movement.resetAreaOfMovement();
            Creature_Stats.currentX = x;
            Creature_Stats.currentY = y;
            Creature_Stats.currentZ = z;

            //-------------------------

            // update amount moved here
            // -------------------------
            remainingMovement = 0; // needs to be updated when pathing system is in place
          }
          else
          {
            print ("This creature can't move there");
          }
        }
        else
        {
          print ("This creature has no movement left");
        }
      }
      else
      {
        print ("It is not this creatures turn");
      }
    }

    public void doAction (string actionType = "Wait")
    {//(wip) this script will be called when a creature inputs an action command

      if (isTurn)
      {
        if (numOfAvailibleActions > 0)
        {
          ActionDatabase actData = new ActionDatabase();
          Action act = actData.callUpAction(actionType);

          if (selectionChecker(act))
          {// wait for selection
            currentAct = act;
            requestSelection = true;

            Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

            Grid_Character_Movement.resetAreaOfMovement();

            Grid_Character_Movement.findAreaOfMovement(currentX: Creature_Stats.currentX, currentY: Creature_Stats.currentY, currentZ: Creature_Stats.currentZ, numOfSteps: act.range);

            print ("target selection is needed");
          }
          else
          {// run without selection
            Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

            Grid_Character_Movement.resetAreaOfMovement();

            Grid_Character_Movement.findAreaOfMovement(currentX: Creature_Stats.currentX, currentY: Creature_Stats.currentY, currentZ: Creature_Stats.currentZ, numOfSteps: act.range);

            outgoingActionProcessing(act : act, creaturePos : new int[] {Creature_Stats.currentX, Creature_Stats.currentY, Creature_Stats.currentZ}, targetPos : new int[,] {{Creature_Stats.currentX, Creature_Stats.currentY, Creature_Stats.currentZ}});
          }
          print ("this creature did something");
        }
        else
        {
          print ("this creature has no more actions left");
        }
      }
      else
      {
        print ("It is not this creatures turn");
      }
    }

    public bool selectionChecker (Action act)
    {//(wip) this script will check to see if a target selection is needed or not

      if (act.needSelection)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public void toggleIsInMovementMode ()
    {// (wip) will toggle weather this character is in movement mode or in action mode
      Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

      if (isInMovementMode)
      {
        if (numOfAvailibleActions > 0)
        {
          isInMovementMode = false;
          Grid_Character_Movement.resetAreaOfMovement();

          print ("Is in action mode");
        }
      }
      else
      {
        if (remainingMovement > 0)
        {
          isInMovementMode = true;
          Grid_Character_Movement.findAreaOfMovement(currentX: Creature_Stats.currentX, currentY: Creature_Stats.currentY, currentZ: Creature_Stats.currentZ, numOfSteps: remainingMovement);

          print ("Is in movement mode");
        }
      }
    }

    // ------------------- action processing -----------------------------

    public void outgoingActionProcessing (int[] creaturePos, int[,] targetPos, Action act = null)
    {//(wip) this script will find the creature to do the action to and will send the needed data to that given creature
      if (isTurn)
      {
        if (numOfAvailibleActions > 0)
        {
          Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

          if(currentAct != null)
          {
            act = currentAct;
          }
          if(act == null)
          {
            print ("this creature needs an action selected first");
          }
          else
          {
            print ("processing the action " + act.name);

            Grid_Character_Movement.resetAreaOfMovement();

            Grid_Character_Movement.findAreaOfMovement(currentX: creaturePos[0], currentY: creaturePos[1], currentZ: creaturePos[2], numOfSteps: act.range);

            GameObject[] creatures = GameObject.FindGameObjectsWithTag("creature");

            for(int i = 0; i < targetPos.GetLength(0); i++)
            {
              int x = targetPos[i,0];
              int y = targetPos[i,1];
              int z = targetPos[i,2];

              foreach(GameObject creature in creatures)
              {
                int creatureX = creature.GetComponent<Creature_Stats>().currentX;
                int creatureY = creature.GetComponent<Creature_Stats>().currentY;
                int creatureZ = creature.GetComponent<Creature_Stats>().currentZ;

                if (creatureX == x && creatureY == y && creatureZ == z && !Grid_Character_Movement.gridUnitChecker(Grid_Character_Movement.findGridUnitByCordinate(x,y,z)))
                {
                  print ("a target has been found");
                  foundTarget = true;

                  creature.GetComponent<Grid_Character_Turn_Manager>().incomingActionProcessing(act);
                }
              }
            }

            //run polish here--------------

            if(act.animationName != null)
            {
              if(act.animationAccessType == "trigger") Grid_Character_Model_Movement.preformAnimationTrigger(act.animationAccessID);
              if(act.animationAccessType == "bool") Grid_Character_Model_Movement.preformAnimationBool(act.animationAccessID);
            }

            //----------------------------

            numOfAvailibleActions--;
            Grid_Character_Movement.resetAreaOfMovement();
          }
        }
        else
        {
          print ("this creature has no more actions avalible");
        }
      }
      else
      {
        print ("it is not this creatures turn");
      }
    }

    public void incomingActionProcessing (Action act = null)
    {//(wip) this script will process any incoming action data
      if (act != null) print (Creature_Stats.Name + " has been hit ");

      if (act.damage != 0)
      {//(wip) will process the damage data
        Creature_Stats.currentHealth -= act.damage;
        print (Creature_Stats.Name + " has taken " + act.damage + " damage");
        Grid_Character_Model_Movement.preformAnimationTrigger("isHit");
      }
    }


}
