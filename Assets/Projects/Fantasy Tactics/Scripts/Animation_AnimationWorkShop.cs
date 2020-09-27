using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_AnimationWorkShop : MonoBehaviour
{
/*--Description---
  This function will control all the dev stuff needed in the animation model works space
*/

/*--Notes---
  This code does not need to be clean since this sence will not be in the final game
*/


//----Variables---------

    public GameObject Camera;

    public GameObject[] Models;

    private Animator[] AnimationControllers;

    public int currentSelection, maxSelection;
    public string animationIntName;

//----Set up Funtions-----
    public void Start()
    {
      AnimationControllers = new Animator[Models.Length];
      for (int i = 0; i < Models.Length; i++)
      {
        AnimationControllers[i] = Models[i].GetComponent<Animator>();
      }
    }
//----Model Functions-----

    public void RotateModel (float degrees)
    {// will rotate the model around the y axis
      foreach (GameObject Model in Models)
      {
        Model.transform.Rotate(0,degrees,0);
      }
    }

    public void AnimationSelector (string name, int currentNumber, int maxNumber)
    {
      if (currentNumber > maxNumber)
      {
        currentSelection = 0;
      }
      if (currentNumber < 0)
      {
        currentSelection = maxSelection;
      }

      foreach (Animator AnimationControl in AnimationControllers)
      {
        AnimationControl.SetInteger(animationIntName, currentSelection);
      }
    }

//-----Input------------

    public void Update ()
    {// checks for any inputs
      if (Input.GetKeyDown("a") == true) RotateModel(90.0f);
      if (Input.GetKeyDown("d") == true) RotateModel(-90.0f);
      if (Input.GetKeyDown("w") == true)
      {
        currentSelection++;
        AnimationSelector(animationIntName, currentSelection, maxSelection);
      }
      if (Input.GetKeyDown("s") == true)
      {
        currentSelection--;
        AnimationSelector(animationIntName, currentSelection, maxSelection);
      }
    }
}
