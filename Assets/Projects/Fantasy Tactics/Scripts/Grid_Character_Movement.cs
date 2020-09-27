using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Character_Movement
{

  /*--Description--
      (The current idea of this script is to control the movement of chacters around the grid)
      (wip)
  */

  /*--Notes--
      this script is still very much a work in progress
      it needs to be optimized and streamlined more
  */

    GameObject[] gridUnits;

    List<GameObject> path;
    GameObject[] creaturePath;
    // an array of all the grid units objects [currently empty]

    public Grid_Character_Movement ()
    {// (wip) this function will be called everytime a new class is made
    }

    // --------------high level functions---------------------------------------

    public void findAreaOfMovement (GameObject gridUnit = null, int currentX = 0, int currentY = 0, int currentZ = 0, int numOfSteps = 4)
    { //(wip) will find the the exact area a creature can move in based on its position

      GameObject g, s;

      if (gridUnit != null)
      {
        g = gridUnit;
      }
      else
      {
        g = findGridUnitByCordinate(currentX, currentY, currentZ);
      }

      updateGridUnitStepNumber(g, 0);

      for(int i = 0; i < numOfSteps; i++) // Step
      {
        do // find all spaces of step
        {
          g = findGridUnitByStepNumber(i);
          if (g != null) // Find surrounding Spaces
          {
            for(int a = 1; a < 5; a++)
            {
              s = findSurroundingSpaces(g, a);
              if (gridUnitChecker(s))
              {//
                updateGridUnitStepNumber(s, i + 1);
              }
            }
          }
          else
          {
          }
        }
        while (g != null);
      }
    }

    public GameObject[] findPathOfMovement (GameObject gridUnit = null, int startX = 0, int startY = 0, int startZ = 0, int endX = 1, int endY = 0, int endZ = 1)
    {//(wip) this function will control the main operations of drawing the path from point A to B
      GameObject c;

      GameObject e = findGridUnitByCordinate(endX,endY,endZ);
      if (e == null)
      {
        return null;
      }

      if (gridUnitChecker(e) == false)
      {
        //Debug.Log ("This end space can be moved to");

        // add e to end of list

        int numOfStepsNeeded = getGridUnitStepNum(e);

        //Debug.Log("The length of this path should be " + numOfStepsNeeded + " steps long");

        creaturePath = new GameObject[numOfStepsNeeded + 1];

        creaturePath[numOfStepsNeeded] = e;

        for (int i = numOfStepsNeeded; i >= 0; i--)
        {
          for (int s = 0; s <= 4; s++)
          {
            c = findSurroundingSpaces(creaturePath[i], s);
            if(getGridUnitStepNum(c) == getGridUnitStepNum(creaturePath[i]) - 1)
            {
              creaturePath[i - 1] = c;
              //Debug.Log ("Found a space");
            }
            //Debug.Log (" Current s step cycle is " + s);
          }
          //Debug.Log (" Current i step cycle is " + i);
        }

        //Debug.Log ("Creature Path is " + creaturePath);

        return creaturePath;
      }
      else
      {
        //Debug.Log ("This is outside the given range");
        return null;
      }
      return null;
    }

    public void resetPathOfMovement ()
    { //(wip)
      gridUnits = gridUnits = GameObject.FindGameObjectsWithTag("gridUnit");
      foreach (GameObject gridUnit in gridUnits)
      {
        gridUnit.GetComponent<Grid_Unit_Script>().isPartOfPath = false;
      }
    }

    public void resetAreaOfMovement ()
    { //(wip)

      gridUnits = gridUnits = GameObject.FindGameObjectsWithTag("gridUnit");
      foreach (GameObject gridUnit in gridUnits)
      {
        gridUnit.GetComponent<Grid_Unit_Script>().stepNum = -1;
        gridUnit.GetComponent<Grid_Unit_Script>().stepChecker = -1;
      }
    }

    // -------------low level functions-----------------------------------------

    // __find grid units___

    public GameObject findSurroundingSpaces (GameObject gridUnit, int caseSwitch)
    { //(wip) the idea of this function is to find all the spaces arround any given cordinate
      GameObject gridUnitUp, gridUnitDown, gridUnitLeft, gridUnitRight;

      int x = gridUnit.GetComponent<Grid_Unit_Script>().x, y = gridUnit.GetComponent<Grid_Unit_Script>().y, z = gridUnit.GetComponent<Grid_Unit_Script>().z;

      switch (caseSwitch)
      {
        case 1:
          gridUnitUp = findGridUnitByCordinate(x - 1, y, z);
          return gridUnitUp;
          break;
        case 2:
          gridUnitRight = findGridUnitByCordinate(x, y, z + 1);
          return gridUnitRight;
          break;
        case 3:
          gridUnitDown = findGridUnitByCordinate(x + 1, y, z);
          return gridUnitDown;
          break;
        case 4:
          gridUnitLeft = findGridUnitByCordinate(x, y, z - 1);
          return gridUnitLeft;
          break;
        default:
          return null;
          break;
      }
      return null;
    }

    public GameObject findGridUnitByCordinate (int x = 0, int y = 0, int z = 0)
    { //(wip) currently finds an exact grid cordinate matching inputs
      gridUnits = GameObject.FindGameObjectsWithTag("gridUnit");
      foreach (GameObject gridUnit in gridUnits)
      {
        int gridUnitX = gridUnit.GetComponent<Grid_Unit_Script>().x;
        int gridUnitY = gridUnit.GetComponent<Grid_Unit_Script>().y;
        int gridUnitZ = gridUnit.GetComponent<Grid_Unit_Script>().z;

        if (gridUnitX == x && gridUnitY == y && gridUnitZ == z)
        {
          return gridUnit;
        }
      }
      return null;
    }

    public GameObject findGridUnitByStepNumber (int step = 1, bool ignoreStepChecker = false)
    { //(wip) currently finds an exact grid cordinate matching inputs
      gridUnits = GameObject.FindGameObjectsWithTag("gridUnit");
      foreach (GameObject gridUnit in gridUnits)
      {
        int gridUnitStep = gridUnit.GetComponent<Grid_Unit_Script>().stepNum;
        int gridUnitStepChecker = gridUnit.GetComponent<Grid_Unit_Script>().stepChecker;

        if (ignoreStepChecker)
        {
          if (gridUnitStep == step)
          {
            return gridUnit;
          }
        }
        else
        {
          if (gridUnitStep == step && gridUnitStepChecker != step)
          {
            updateGridUnitStepChecker(gridUnit, step);
            return gridUnit;
          }
        }
      }
      return null;
    }

    // ___check grid unit____

    public bool gridUnitChecker (GameObject gridUnit)
    { //(wip)
      if (gridUnit != null)
      {
        if (gridUnit.GetComponent<Grid_Unit_Script>().stepNum < 0)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      return false;
    }

    public bool canMoveToSpace (GameObject gridUnit = null, int endX = 0, int endY = 0, int endZ = 0)
    {//(wip) checks if a crature can move to the given space
      GameObject g;

      if (gridUnit != null)
      {
        g = gridUnit;
      }
      else
      {
        g = findGridUnitByCordinate(endX, endY, endZ);
      }
      if (g != null)
      {
        if (gridUnitChecker(g)) return false;
        else return true;
      }
      else
      {
        return false;
      }
    }

    public int getGridUnitStepNum (GameObject gridUnit)
    {//(wip) will return the given gridUnits current StepNum
      if (gridUnit != null)
      {
        return gridUnit.GetComponent<Grid_Unit_Script>().stepNum;
      }
      else
      {
        return -100;
      }
    }

    // __update grid unit_____

    public void updateGridUnits (GameObject[] gridUnits, int caseSwitch = 0)
    {//(wip) will be used to update many gridUnits at once
      foreach (GameObject gridUnit in gridUnits)
      {
        // update gridUnits
        switch(caseSwitch)
        {
          case 0:
            updateGridUnitIsPartOfPath(gridUnit, true);
            break;
          default:
            break;
        }
      }
    }

    public void updateGridUnitStepNumber (GameObject gridUnit, int step)
    { //(wip) currently updates an exact grid cordinates matching variable
      if (gridUnit != null)
      {
        gridUnit.GetComponent<Grid_Unit_Script>().stepNum = step;
      }
      else
      {
      }
    }

    public void updateGridUnitStepChecker (GameObject gridUnit, int step)
    { //(wip) currently updates an exact grid cordinates matching variable
      if (gridUnit != null)
      {
        gridUnit.GetComponent<Grid_Unit_Script>().stepChecker = step;
      }
      else
      {
      }
    }

    public void updateGridUnitIsPartOfPath (GameObject gridUnit, bool selection)
    { //(wip) currently updates weather the grid unit appears to be part of a path or not
      gridUnit.GetComponent<Grid_Unit_Script>().isPartOfPath = selection;
    }

    public void updateGridUnitTerrianDifficulty (GameObject gridUnit, bool isDifficult)
    { //(wip) currently updates an exact grid cordinates matching variable
      if (gridUnit != null)
      {
        gridUnit.GetComponent<Grid_Unit_Script>().isDifficultTerrain = isDifficult;
      }
      else
      {
      }
    }

    public void updateGridSub (int[,] cArray)
    {
      for (int i = 0; i < cArray.GetLength(0); i++)
      {
        int x = cArray[i,0];
        int y = cArray[i,1];
        int z = cArray[i,2];

        GameObject g;

        g = findGridUnitByCordinate(x,y,z);

        // alter g;
      }
    }

}
