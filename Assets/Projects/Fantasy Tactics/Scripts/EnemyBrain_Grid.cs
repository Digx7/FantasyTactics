using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain_Grid : MonoBehaviour
{
    /*--Description--
      This script should manage the enemy ai
    */

    /*--Note--
      this script may need heavy modification as the battle system changes
    */
    [Header("Data")]
    public string Name;
    public int currentX, currentY, currentZ;
    public int numOfMaxActions, numOfAvailibleActions;
    public int speed;
    public string[] actions;
    public bool isInMovementMode = false;
    [Space]
    [Header("Access")]
    public Grid_Turn_Manager Grid_Turn_Manager;
    public Grid_UI_Manager Grid_UI_Manager;
    [Space]
    [SerializeField]
    private float t = 0;
    private bool codeButtonA = false;
    [SerializeField]
    private int lastTurn = -1;
    [Space]
    [SerializeField]
    private string[] inputs;
    [SerializeField]
    private int numOfCommands;
    [SerializeField]
    private float delayTime;

    // --------------------things it can do------------------------------
    public void SelectSpace (int x, int y, int z)
    {//(wip) this function should select a space for the enemy
      Grid_UI_Manager.ai_mouseHover(x,y,z);
      Grid_UI_Manager.onClick();
      Grid_UI_Manager.ai_mouseHover(x,y,z,true);

      print("the creature should have moved");
    }

    public void PreformAction (int choice)
    {//(wip) this function should preform an action for the enemy
      Grid_UI_Manager.selectAction(choice);
      print ("the creature has selected an action");
    }

    public void ToggleMode()
    {//(wip) this function should toggle the movement mode for the enemy
      Grid_UI_Manager.toggleType();
      print ("toggling mode");
    }

    public void endTurn()
    {//(wip) this funtion will end the enemies turn
      Grid_UI_Manager.onEndTurnClick();
    }

    // --------------------processing------------------------------

    public void planActions ()
    {//(wip) this function will plan the needed action for the creature
      print ("The enemy is think up a clever strategy");

      //inputs = new string[1 + numOfCommands];
      inputs[inputs.Length - 1] = "end";

      //executeActions();
    }

    /*public void executeActions ()
    {//(wip) this function will execute the planed actions
      print ("The enemy decided to spare you .... for now");

      for(int i = 0; i < inputs.Length; i++)
      {
        if(inputs[i] == "select") selectProcessing(inputs[i + 1], inputs[i + 2], inputs[i + 3]);
        if(inputs[i] == "end") endTurn();
        if(inputs[i] == "toggle") ToggleMode();
        if(inputs[i] == "action") preformActionProcessing(inputs[i + 1]);

        do {
          t = t + Time.deltaTime;
        } while (t < delayTime);
        Debug.Log("enemy is moving to its next step");
      }
    }*/

    /*IEnumerator executeAction(string[] inputs)
    {

    }*/

    public void selectProcessing (string x, string y, string z)
    {
      print ("creature should attempt to move toward : " + x + "," + y + "," + z);

      int posX = int.Parse(x);
      int posY = int.Parse(y);
      int posZ = int.Parse(z);

      SelectSpace(posX,posY,posZ);
    }

    public void preformActionProcessing (string action)
    {
      int choice = int.Parse(action);

      PreformAction(choice);
    }

    public void delayTimer ()
    {//(wip) this function will put delays in the ai's actions;

    }

    // --------------------meta------------------------------

    public void startTurn ()
    {//(wip) this function will be run at the start of the enemy turn
      print("Starting an enemy turn");

      extractData();
    }

    public void extractData ()
    {//(wip) this function will gather up all the needed data
      GameObject g;
      g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
      Creature_Stats Creature_Stats = g.GetComponent<Creature_Stats>();

      Name = Creature_Stats.Name;

      currentX = Creature_Stats.currentX;
      currentY = Creature_Stats.currentY;
      currentZ = Creature_Stats.currentZ;

      numOfMaxActions = Creature_Stats.numOfMaxActions;
      numOfAvailibleActions = numOfMaxActions;

      speed = Creature_Stats.speed;

      actions = Creature_Stats.actions;

      planActions();
    }

    public void Update ()
    {//(wip) this function will be called once per frame

      if (Grid_Turn_Manager.currentTurn != lastTurn)
      {
        // run code

        codeButtonA = false;

        print ("it should be a new turn");

        lastTurn = Grid_Turn_Manager.currentTurn;
      }

      if (Grid_Turn_Manager.isEnemyTurn == true)
      {
        if(codeButtonA == false)
        {
            // Call your event function here.
            //--------------
            startTurn();
            //--------------
            codeButtonA = true;
          }
      }
      else if (Grid_Turn_Manager.isEnemyTurn == false)
      {
        codeButtonA = false;
      }
    }
}
