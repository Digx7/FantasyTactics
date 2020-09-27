using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Turn_Manager : MonoBehaviour
{
  /*--Description--
    This script will set up and manage the turn order of all the creatures on the grid
  */

  /*--Notes--
    This script will need to be modual
    This script should not need any hard ties to any creatures
    This script should work with any given combination and number of creatures

    Need to have some kind of initiative order
  */

  public GameObject[] turnOrder;
  public Grid_UI_Manager Grid_UI_Manager;

  public int currentTurn = 0;

  public bool isEnemyTurn = false;

  public void setUpTurnOrder()
  {//(wip) this script should set up the turn order
    turnOrder = GameObject.FindGameObjectsWithTag("creature");

    // need some code to find initiative and sort this array based on initiative value

    // sets up needed ui
    Grid_UI_Manager.setUpInitiativeUi();
  }

  public void setUpGrid ()
  {//(wip) this script should set up the grid

    foreach(GameObject turn in turnOrder)
    {
      GameObject g = turn;

      g.GetComponent<Grid_Character_Turn_Manager>().syncSystems();

      int x = g.GetComponent<Creature_Stats>().currentX;
      int y = g.GetComponent<Creature_Stats>().currentY;
      int z = g.GetComponent<Creature_Stats>().currentZ;

      g.GetComponent<Grid_Character_Model_Movement>().moveModel(endX: x, endY: y, endZ: z);
    }
  }

  public void nextTurn ()
  {//(wip) this script should manage which turn the game is on and go to the next one.
    if (currentTurn >= turnOrder.Length)
    {
      currentTurn = 0;
    }

    GameObject g = turnOrder[currentTurn];

    if(g.GetComponent<Creature_Stats>().isEnemy == true) isEnemyTurn = true;
    else isEnemyTurn = false;

    g.GetComponent<Grid_Character_Turn_Manager>().startTurn();

    Grid_UI_Manager.nextTurn();
  }
}
