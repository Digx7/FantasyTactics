using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Input_Script : MonoBehaviour
{

  /*--Description--
    This script should hold the main input systems for the game

  */

  /*--Notes--
    This will have to be kept flexible for both keyboards and contorlers

  */

  public Grid_Unit_Script Grid_Unit_Script;
  [Space]
  public Grid_Generator_Script Grid_Generator_Script;
  public Grid_Turn_Manager Grid_Turn_Manager;
  [Space]
  public Creature_Stats Creature_Stats;
  public Grid_Character_Model_Movement Grid_Character_Model_Movement;
  public Grid_UI_Manager Grid_UI_Manager;
  [Space]
  public Vector3 actTarget;
  [Space]
  private bool s_isAxisInUse = false, e_isAxisInUse = false, t_isAxisInUse = false;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {//(wip) this currently calls all the other funcions to keep an eye on the input
    if (Grid_Turn_Manager.isEnemyTurn == false)
    {
      selectSpace();
      endTurn();
      toggleInputMode();
    }
  }

  // ----------------mouse buttons----------------------

  private void selectSpace()//(wip) this will call the onclick function
    {
      if( Input.GetAxisRaw("Select") != 0)
       {
           if(s_isAxisInUse == false)
           {
               // Call your event function here.
               Grid_UI_Manager.onClick();

               print ("Left mouse button was pressed");
               s_isAxisInUse = true;
           }
       }
       if( Input.GetAxisRaw("Select") == 0)
       {
           s_isAxisInUse = false;
       }
    }

  // -----------------keyboard buttons------------------

  private void endTurn()//(wip) this will end the current creatures turn
    {
          if ( Input.GetAxisRaw("EndTurn") != 0)
          {
            if(e_isAxisInUse == false)
            {
                // Call your event function here.
                Grid_UI_Manager.onEndTurnClick();

                print ("e was pressed");
                e_isAxisInUse = true;
            }
          }
        if( Input.GetAxisRaw("EndTurn") == 0)
        {
            e_isAxisInUse = false;
        }
    }

  private void toggleInputMode()//(wip) this will toggle the selected creatures input type
    {
          if( Input.GetAxisRaw("ToggleType") != 0)
           {
               if(t_isAxisInUse == false)
               {
                 Grid_UI_Manager.toggleType();

                 print ("t was pressed");

                 t_isAxisInUse = true;
               }
           }
           if( Input.GetAxisRaw("ToggleType") == 0)
           {
               t_isAxisInUse = false;
           }
    }

  // -----------------onscreen buttons------------------

  public void onEndTurnClick ()//(wip) this will end the current creatures turn
    {
      if(Grid_Turn_Manager.isEnemyTurn == false) Grid_UI_Manager.onEndTurnClick();
    }

  public void selectAction (int choice)//(wip) this function will be called when a player selects an action
    {
      if(Grid_Turn_Manager.isEnemyTurn == false) Grid_UI_Manager.selectAction(choice);
    }

  public void exitGame (int selection)//(wip) this function will be called when the exit game button is pressed and load in the selected scene;
    {
      Grid_UI_Manager.exitGame(selection);
    }


}
