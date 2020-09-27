using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Grid_UI_Manager : MonoBehaviour
{

  /*--Description--
    This script should handle all the player inputs and ui systems
  */

  /*--Notes--
    Do not make this script neccisary in running the main systems as you will need to build another version for the ai
  */

  [Header("UI Objects")]
  public GameObject actionButtonHolder;
  public GameObject endTurnButton;
  public GameObject exitGameButton;
  public GameObject[] actionButtons;
  public GameObject EnemyTurn;
  public GameObject initiativeUi;
  [Space]

  public Vector3 selectedGridUnit = new Vector3();
  public Grid_Turn_Manager Grid_Turn_Manager;
  [Space]

  private Transform lastHitObject;
  [SerializeField]
  private Grid_Unit_Script script;

  [SerializeField]
  private string[] creatureActions;
  [SerializeField]
  private GameObject actionButtonsHolder;

  public Transform initiativeUiPrefab, initiativeUiHolder;
  [SerializeField]
  private int initiativeUiOffset = 1;

  private bool s_isAxisInUse = false;
  private bool x_isAxisInUse = false;
  private bool e_isAxisInUse = false;

  // --------------------set up------------------------------

  public void setUpInitiativeUi ()
  {//(wip) this function will set up the initiative ui elements
    GameObject[] g;
    g = Grid_Turn_Manager.turnOrder;

    for(int i = 0; i < g.Length; i++)
    {
      // create initiative ui elements
      Transform initiativeUiPrefabClone = Instantiate(initiativeUiPrefab, initiativeUiHolder, false);
      initiativeUiPrefabClone.localPosition = new Vector3(0, i * initiativeUiOffset, 0);

      Transform initiativeUiPrefabCloneName = initiativeUiPrefabClone.transform.GetChild(2);
      Transform initiativeUiPrefabCloneIsTurn = initiativeUiPrefabClone.transform.GetChild(1);

      initiativeUiPrefabCloneName.GetComponent<Text>().text = g[i].GetComponent<Creature_Stats>().Name;
      // set up ui elements in the correct screen position for each one
      // set up correct name for each ui element
    }
  }

  // -------------------mouse input-------------------------

  public void onClick ()
  {//(wip) this function should be called any time that the mouse button is clicked
    GameObject g;
    g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
    var c = g.GetComponent<Grid_Character_Turn_Manager>();
    var s = g.GetComponent<Creature_Stats>();

    print ("onClick was called");

    if (script!= null)
    {

      print ("script was not null");

      if (script.selection.activeSelf)
      {
        //--------------------------------
        if(c.isInMovementMode)
        {

          print ("the creature was in movement mode");

          // will run if the creature is in movement mode
          c.moveCreature((int)selectedGridUnit.x, (int)selectedGridUnit.y, (int)selectedGridUnit.z);

          print ("The creature is trying to move");
        }
        else
        {

          print ("the creature was in action mode");

          // will run if the creature is in action mode
          if (c.currentAct != null && c.currentAct.maxNumberOfTargets > 1)
          {
            print ("multisection is needed"); // NOTE this feature may be cut
          }
          else
          {
            c.outgoingActionProcessing(new int[]{s.currentX, s.currentY, s.currentZ},new int[,] {{(int)selectedGridUnit.x, (int)selectedGridUnit.y, (int)selectedGridUnit.z}});

            print ("The creatue is trying to select a target");
          }
        }
        //--------------------------------
      }
      else if (!script.selection.activeSelf)
      {
        print ("script.selection.activeSelf is not active");
      }
    }
    else if (script == null)
    {
      print ("script was null");
    }
  }

  public void mouseHover ()
  {//(wip) this function will update the mouse selection

    //--------Player Code----------------------------
    if (Grid_Turn_Manager.isEnemyTurn == false)// will keep the player from selecting anything during the enemies turn
    {

      e_isAxisInUse = false;

      var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit))
      {
        if (lastHitObject != null && lastHitObject != hit.collider.transform)
        {

          //if the lastHitObject is not null and different from the current hit object then
          //reset the material of the lastHitObject
          script = lastHitObject.GetComponent<Grid_Unit_Script>();
          if (script != null)
          {
            script.selection.SetActive(false);
          }

        }

        lastHitObject = hit.collider.transform;
        //change the hit objects material;
        script = lastHitObject.GetComponent<Grid_Unit_Script>();
        if (script != null)
        {

          selectedGridUnit.x = script.x;
          selectedGridUnit.y = script.y;
          selectedGridUnit.z = script.z;


          script.selection.SetActive(true);
        }
      }
      else if(lastHitObject)
      {
        //reset the material of the lastHitObject if the raycast fail;
        script = lastHitObject.GetComponent<Grid_Unit_Script>();
        if (script != null)
        {
          script.selection.SetActive(false);
        }
      }
    }
    else if (Grid_Turn_Manager.isEnemyTurn == true)
    {

      //------------Enemy Code--------------------------------

      if(e_isAxisInUse == false)
      {
          // Call your event function here.
          if (script != null) script.selection.SetActive(false);
          e_isAxisInUse = true;
      }
    }
  }

  public void ai_mouseHover (int x, int y, int z, bool unselect = false)
  {//(wip) this function will update the mouse selection

    if(unselect == false)
    {

      Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

      GameObject g = Grid_Character_Movement.findGridUnitByCordinate(x,y,z);
      script = g.GetComponent<Grid_Unit_Script>();

      selectedGridUnit.x = script.x;
      selectedGridUnit.y = script.y;
      selectedGridUnit.z = script.z;

      script.selection.SetActive(true);
    }

    if(unselect == true)
    {
      script.selection.SetActive(false);
    }

    print ("The enemy has selected a space");
  }

  // ----------------on screen buttons-----------------------

  public void onEndTurnClick ()
  {//(wip) this script will be called when the end turn button is called
    GameObject g;
    g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
    var c = g.GetComponent<Grid_Character_Turn_Manager>();

    c.remainingMovement = 0;
    c.numOfAvailibleActions = 0;
  }

  public void selectAction (int choice)
  {//(wip) this funcion will be called when a player selects an action
    GameObject g;
    g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
    var c = g.GetComponent<Grid_Character_Turn_Manager>();

    if(checkIfCreatureActionExists(choice))
    {
      c.doAction(creatureActions[choice]);
    }
  }

  public void exitGame (int selection)
  {//(wip) this funcion will be called when the exit game button is pressed and load in the selected scene;
    SceneManager.LoadScene(selection);
  }

  // --------------hidden/hotkey buttons------------------------

  public void toggleType ()
  {//(wip) this script should toggle the current creature between action and movement mode
    GameObject g;
    g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
    var c = g.GetComponent<Grid_Character_Turn_Manager>();

    c.toggleIsInMovementMode();
    modeChecker();
  }

  // -------------------toggle ui elements----------------------

  public void toggleEndTurnButton (bool state)
  {//(wip) this function will toggle weather or not the end turn button is active or not
    endTurnButton.SetActive(state);
  }

  public void toggleExitGameButton (bool state)
  {//(wip) this function will toggle weather or not the exit game button is active or not
    exitGameButton.SetActive(state);
  }

  public void toggleActionButtonHolder (bool state)
  {//(wip) this function will toggle weather or not the action button holder is active or not
    actionButtonHolder.SetActive(state);
  }

  public void toggleEnemyTurn (bool state)
  {//(wip) this function will toggle weather or not the enemy turn object is active or not
    EnemyTurn.SetActive(state);
  }

  public void toggleInitiativeUi (bool state)
  {//(wip) this function will toggle weather or not the initiative ui holder is active or not
    initiativeUi.SetActive(state);
  }

  // ----------------------ui states--------------------------

  public void enemyTurn ()
  {//(wip) will activate when it is an enemy turn
    if (Grid_Turn_Manager.isEnemyTurn == true)
    {
      if(s_isAxisInUse == false)
      {
          // Call your event function here.

          toggleEnemyTurn(true);
          toggleActionButtonHolder(false);
          toggleEndTurnButton(false);

          s_isAxisInUse = true;
      }
    }
    else if (Grid_Turn_Manager.isEnemyTurn == false)
    {
      s_isAxisInUse = false;
    }
  }

  public void playerTurn ()
  {//(wip) will activate when it is a player turn
    if (Grid_Turn_Manager.isEnemyTurn == false)
    {
      if(x_isAxisInUse == false)
      {
          // Call your event function here.

          toggleEnemyTurn(false);
          toggleActionButtonHolder(true);
          toggleEndTurnButton(true);

          x_isAxisInUse = true;
      }
    }
    else if (Grid_Turn_Manager.isEnemyTurn == true)
    {
      x_isAxisInUse = false;
    }
  }

  // ------------------------meta----------------------------

  public void updateInitiativeUi ()
  {//(wip) this function will update the initiative ui elements
    GameObject g;
    g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
    var s = g.GetComponent<Creature_Stats>();

    GameObject[] uiElements;
    uiElements = GameObject.FindGameObjectsWithTag("initiativeUI");

    foreach (GameObject uiElement in uiElements)
    {
      Transform initiativeUiPrefabCloneIsTurn = uiElement.transform.GetChild(1);
      Transform initiativeUiPrefabCloneName = uiElement.transform.GetChild(2);
      if (initiativeUiPrefabCloneName.GetComponent<Text>().text == s.Name)
      {
        initiativeUiPrefabCloneIsTurn.gameObject.SetActive(true);
      }
      else
      {
        initiativeUiPrefabCloneIsTurn.gameObject.SetActive(false);
      }
    }
  }

  public void updateActionUIButtonText ()
  {//(wip) this function will update the action buttons text
    for(int i = 0; i < 3; i++)
    {
      if(checkIfCreatureActionExists(i))
      actionButtons[i].GetComponentInChildren<Text>().text = creatureActions[i];
      else actionButtons[i].GetComponentInChildren<Text>().text = "none";
    }
  }

  private bool checkIfCreatureActionExists (int num)
  {//(wip) this function will check if the given int is in the array;
    if(num < creatureActions.Length)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  public void modeChecker ()
  {//(wip) this funcion will check to see weather the creature is in movement mode or not
    GameObject g;
    g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
    var c = g.GetComponent<Grid_Character_Turn_Manager>();

    if (c.isInMovementMode)
    {
      // turn off action buttons
      actionButtonsHolder.SetActive(false);
    }
    else
    {
      // turn on action buttons
      actionButtonsHolder.SetActive(true);
    }
  }

  public void nextTurn ()
  {//(wip) this funcion will be called every turn to update the ui as needed
    GameObject g;
    g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];
    var s = g.GetComponent<Creature_Stats>();

    creatureActions = s.actions;
    updateInitiativeUi();
    updateActionUIButtonText();
    modeChecker();
    // update current turn ui
  }

  // Update is called once per frame
  void Update()
  {//(wip)
        mouseHover();
        enemyTurn();
        playerTurn();
  }
}
