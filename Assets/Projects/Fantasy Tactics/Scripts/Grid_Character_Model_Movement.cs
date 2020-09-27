using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Character_Model_Movement : MonoBehaviour
{
  /*--Description--
    This script will handle the player models movement animations around the grid
  */

  /*--Notes--
    Much of this script will be polish that can be worked on later
  */

  public GameObject creatureModel;
  public GameObject creature;
  public Animator creatureAnimator;

  public float movementSpeed;
  public float speed;

  private GameObject g;

  private bool isMoving = false;
  private Vector3 movingTo;
  private int arrayPosition = 0;
  public GameObject[] path;

  //---- used to 'teleport' creatures across the map--------------

  public void moveModel (GameObject[] path = null, int currentX = 0, int currentY = 0, int currentZ = 0, int endX = 1, int endY = 1, int endZ = 1)
  {//(wip) this function will move the model across the grid

    Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();

    g = Grid_Character_Movement.findGridUnitByCordinate(endX, endY, endZ);

    if (g == null)
    {
      print ("There seems to be a problem");
    }
    else
    {
      creature.transform.position = g.transform.position; // is just a place holder for now

      // animate character moving
      // must read the array and animate the model accordingling
    }
  }

  // ------used to animate creatures moving across the map--------

  public void startModelMoveAnimation (GameObject[] _path)
  {// (wip) this function will start the process of moving the model and animating it
    isMoving = true;
    path = _path;

    print ("model move should be starting");

    preformAnimationBool("isMoving");
  }

  public void moveAnimationProcessing (GameObject gridUnit = null)
  {// (wip) this function will control the entire process of moving and animating any creature as it moves on the grid
    Vector3 gridUnitPosition = gridUnit.transform.position;

    print (gridUnit.transform.position);

    creature.transform.position = Vector3.MoveTowards(creature.transform.position, gridUnit.transform.position, movementSpeed * Time.deltaTime);

    if (creature.transform.position == gridUnit.transform.position)
    {
      arrayPosition++;
      print ("next array position");

      //preformAnimationBool ("isMoving");
    }
    else
    {
      //preformAnimationBool ("isMoving");
      // rotate model towards direction of movement
      Vector3 relativePos = gridUnit.transform.position - creature.transform.position;

      creatureModel.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
      // ------------------------------------------
    }
  }

  // -------------------------------------------------------------

  public void preformAnimationTrigger (string triggerName)
  {//(wip) this should preform the given action animation
    creatureAnimator.SetTrigger(triggerName);
    //creatureAnimator.SetBool("isAttacking", false);

    print ("an animation should be playing");
  }

  public void preformAnimationBool (string boolName)
  {//(wip) this should preform the given action animation
    if(creatureAnimator.GetBool(boolName))
    {
      // set true
      creatureAnimator.SetBool(boolName, false);
      print ("animation turned off");
    }
    else
    {
      // set false
      creatureAnimator.SetBool(boolName, true);
      print ("animation turned on");
    }
    print ("parameter " + boolName + " is currently " + creatureAnimator.GetBool(boolName));
  }

  public void Update ()
  {//(wip) will reset and update the animation movement process
    if (isMoving) 
    {
      print ("character model is moving");
      //run moving animations
      if(arrayPosition < path.Length)
      {
        print ("the array position is not null");
        moveAnimationProcessing (path[arrayPosition]);
      }
      else
      {
        Grid_Character_Movement Grid_Character_Movement = new Grid_Character_Movement();
        Grid_Character_Movement.resetPathOfMovement();

        arrayPosition = 0;
        isMoving = false;

        preformAnimationBool("isMoving");
      }
    }
  }
}
