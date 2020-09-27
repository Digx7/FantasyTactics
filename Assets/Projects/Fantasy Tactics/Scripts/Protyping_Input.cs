using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protyping_Input : MonoBehaviour
{

  /*--Description--
    The idea of this script is to hold all the prototyping/development inputs in one place.
    Rather than in the update functions of many different scripts.

  */

  /*--Notes--
    This script might need to be changed overtime
    Remember not to make this script integral down the road
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
    private bool m_isAxisInUse = false;
    [Space]
    public int endX;
    public int endZ;
    [Space]
    public GameObject[] path;

    // Update is called once per frame
    void Update()
    {
      // grid inputs
      if (Input.GetKeyDown("g") == true)
      {// generates a grid
        int rows = Grid_Generator_Script.rows;
        int columns = Grid_Generator_Script.columns;
        int layers = Grid_Generator_Script.layers;
        int unitSpacing = Grid_Generator_Script.unitSpacing;
        int startX = Grid_Generator_Script.startX;
        int startY = Grid_Generator_Script.startY;
        int startZ = Grid_Generator_Script.startZ;

        Grid_Generator_Script.GenerateGrid(rows,columns,layers,startX,startY,startZ,unitSpacing);
        if(Grid_Generator_Script.debugLogs_02) print ("A Grid has been made");
      }
      if (Input.GetKeyDown("0") == true)
      {// will reveal the entire grid
        GameObject[] gArray = GameObject.FindGameObjectsWithTag("gridUnit");

        foreach (GameObject g in gArray)
        {
          g.GetComponent<Grid_Unit_Script>().stepNum = 1;
        }
      }
      if (Input.GetKeyDown("1") == true)
      {// will make grid combination number 1
        Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();
        Grid_Character_Movement.updateGridSub(new int[,] {{4,0,0}, {4,0,1}, {4,0,2}, {4,0,3}, {4,0,4}, {5,0,0}, {5,0,1}, {5,0,2}, {5,0,3}, {5,0,4}, {0,0,4}, {1,0,4}, {7,0,5}, {7,0,6}, {8,0,5}, {8,0,6}, {9,0,5}, {9,0,6}, {4,0,8}, {4,0,9}, {5,0,8}, {5,0,9}});
      }
      if (Input.GetKeyDown("c") == true)
      {//clears any area of movement
        Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();
        Grid_Character_Movement.resetAreaOfMovement();
        print ("C was pressed");
      }
      if (Input.GetKeyDown("p") == true)
      {// will draw a creature path
        Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();
        path = Grid_Character_Movement.findPathOfMovement(endX : endX, endZ : endZ);

        int x,y,z;

        int num = 0;

        if (path != null)
        {
          foreach(GameObject unit in path)
          {
            x = unit.GetComponent<Grid_Unit_Script>().x;
            y = unit.GetComponent<Grid_Unit_Script>().y;
            z = unit.GetComponent<Grid_Unit_Script>().z;

            //print ("The path point " + num + " is at the cordinate (" + x + "," + y + "," + z + ")");

            num++;
          }

          Grid_Character_Movement.updateGridUnits(path);
        }

        print ("p was pressed");
      }
      if (Input.GetKeyDown("r") == true)
      {// will reset the drawn path
        Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

        Grid_Character_Movement.resetPathOfMovement();
      }
      if (Input.GetKeyDown("w") == true)
      {
        GameObject g;
        g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];

        g.GetComponent<Grid_Character_Model_Movement>().startModelMoveAnimation(path);

        print ("w was pressed");
      }

      /*
      if (Input.GetKeyDown("w") == true)
      {//test the findAreaOfMovement funtion
        int currentX = Grid_Character_Movement.currentX;
        int currentY = Grid_Character_Movement.currentY;
        int currentZ = Grid_Character_Movement.currentZ;
        int numOfSteps = Grid_Character_Movement.numOfSteps;

        Grid_Character_Movement.findAreaOfMovement(currentX: currentX, currentY: currentY, currentZ: currentZ, numOfSteps: numOfSteps);
        if(Grid_Character_Movement.debugLogs_Input) print ("W was pressed");
      }
      if (Input.GetKeyDown("c") == true)
      {//clears any area of movement
        Grid_Character_Movement.resetAreaOfMovement();
        if(Grid_Character_Movement.debugLogs_Input) print ("C was pressed");
      }
      */

      /*
      if (Input.GetKeyDown("s") == true)
      {// finds the area of movement based on given creature
        int currentX = Creature_Stats.currentX;
        int currentY = Creature_Stats.currentY;
        int currentZ = Creature_Stats.currentZ;
        int numOfSteps = Creature_Stats.speed;

        Grid_Character_Movement.findAreaOfMovement(currentX: currentX, currentY: currentY, currentZ: currentZ, numOfSteps: numOfSteps);
      }
      if (Input.GetKeyDown("m") == true)
      {// moves given creature to its end position, if it can
        int endX = Creature_Stats.endX;
        int endY = Creature_Stats.endY;
        int endZ = Creature_Stats.endZ;

        if (Grid_Character_Movement.canMoveToSpace(endX: endX, endY: endY, endZ: endZ))
        {
          Grid_Character_Movement.resetAreaOfMovement();

          Creature_Stats.currentX = endX;
          Creature_Stats.currentY = endY;
          Creature_Stats.currentZ = endZ;

          Grid_Character_Model_Movement.moveModel(endX: endX, endY: endY, endZ: endZ);
        }
        else
        {

        }
      }
      if (Input.GetKeyDown("p") == true)
      {// moves given creature model to its starting position
        int endX = Creature_Stats.currentX;
        int endY = Creature_Stats.currentY;
        int endZ = Creature_Stats.currentZ;

        Grid_Character_Model_Movement.moveModel(endX: endX, endY: endY, endZ: endZ);
      }
      */

      // frame work set up
      if (Input.GetKeyDown("s") == true)
      {// sets up the grid
        Grid_Turn_Manager.setUpTurnOrder();
        Grid_Turn_Manager.setUpGrid();
        Grid_Turn_Manager.nextTurn();

        print ("s was pressed");
      }

      // creature turn options

      /*
        if (Input.GetKeyDown("e") == true)
          {// will end the current creatures turn

        GameObject g;
        g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];

        g.GetComponent<Grid_Character_Turn_Manager>().remainingMovement = 0;
        g.GetComponent<Grid_Character_Turn_Manager>().numOfAvailibleActions = 0;

        print ("e was pressed");
          }
      */

      if (Input.GetKeyDown("m") == true)
      {// will move the creature whos turn it is to there given end point
        GameObject g;
        g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];

        g.GetComponent<Grid_Character_Model_Movement>().preformAnimationBool("isMoving");

        print ("m was pressed");
      }
      if (Input.GetKeyDown("d") == true)
      {// will have the creature whos turn it is do the wait action
        GameObject g;
        g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];

        g.GetComponent<Grid_Character_Model_Movement>().preformAnimationBool("isDead");

        print ("d was pressed");
      }
      if (Input.GetKeyDown("a") == true)
      {// will have the creature whos turn it is do the attack action
        GameObject g;
        g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];

        g.GetComponent<Grid_Character_Model_Movement>().preformAnimationTrigger("isAttacking");
        print ("a was pressed");
      }
      if (Input.GetKeyDown("h") == true)
      {// will have the creature whos turn it is do the attack action
        GameObject g;
        g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];

        g.GetComponent<Grid_Character_Model_Movement>().preformAnimationTrigger("isHit");
        print ("h was pressed");
      }
      if (Input.GetKeyDown("z") == true)
      {// will select a target for the creature to attack
        GameObject g;
        g = Grid_Turn_Manager.turnOrder[Grid_Turn_Manager.currentTurn];

        int[] currentPos = new int[] {g.GetComponent<Creature_Stats>().currentX, g.GetComponent<Creature_Stats>().currentY, g.GetComponent<Creature_Stats>().currentZ};
        int[,] targetPos = new int[,] {{(int)actTarget.x, (int)actTarget.y, (int)actTarget.z}};

        g.GetComponent<Grid_Character_Turn_Manager>().outgoingActionProcessing(currentPos, targetPos);

        print ("z was pressed");
      }

    /*

      if( Input.GetAxisRaw("Fire1") != 0)//(wip) this will call the onclick function
     {
         if(m_isAxisInUse == false)
         {
             // Call your event function here.
             Grid_UI_Manager.onClick();

             print ("Left mouse button was pressed");
             m_isAxisInUse = true;
         }
     }
     if( Input.GetAxisRaw("Fire1") == 0)
     {
         m_isAxisInUse = false;
     }

     if (Input.GetKeyDown("t") == true)
     {
       Grid_UI_Manager.toggleType();

       print ("t was pressed");
     }
    */
    }
}
