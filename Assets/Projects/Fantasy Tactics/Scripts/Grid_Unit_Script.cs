using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Unit_Script : MonoBehaviour
{
  /*--Description--
      This script holds all the changeable variables for each grid unit.
  */

  /*--Notes--
      I may have to add more variables here as the game is futher developed.
      I also need to keep all the variables public to let them be changed.
      This script is attached to a prefab that gets Instantiated a lot
  */

    public int x = 0,y = 0,z = 0; // these are the grid units position on the grid

    public bool isAvalible = true; // this states weather the unit can be accessed
    public int stepNum = -1; // this states what number it is in the movement process (wip)
    public int stepChecker = 0;

    public bool isOcupied = false;
    public bool isDifficultTerrain = false;
    public bool isHalfWall = false;
    public bool isFullWall = false;
    public bool isBrush = false;
    public bool isShadow = false;

    public bool isPartOfPath = false;
    public MeshRenderer render;
    public GameObject selection;
    public GameObject pathing;
    public bool isHovered = false;

    void Start ()
    {//(wip)
      render.enabled = false;
      selection.SetActive(false);
      isHovered = false;
    }

    void Update ()
    {//(wip)
      if(stepNum >= 0)
      {
        render.enabled = true;
      }
      else
      {
        render.enabled = false;
      }
      if(isPartOfPath)
      {
        pathing.SetActive(true);
      }
      else
      {
        pathing.SetActive(false);
      }
    }
}
