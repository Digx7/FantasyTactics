using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
  // This script should control the player character

  [SerializeField]
  private Transform Self; // this will be the transform of the object so I can have all the scripts on one object

  public float Speed = 10; // Will be the speed of the player movement

  void Update ()
  {
    PlayerMovement(); // calls the player movement once per update
  }

  void PlayerMovement () // the player movement function
  {
    //grabing inputs
    // Update to new controller inputs (keyboard/controller)
    float hor = Input.GetAxis("Horizontal"); // grabs input from the input axis named : Horizontal
    float ver = Input.GetAxis("Vertical"); // grabs input from the input axis named : Vertical

    //applying movents
    Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime; // creats a vector3 based off inputs, uses Time.deltaTime to stay frame independant
    Self.Translate(playerMovement, Space.Self); // moves player via transform based off the vector3 playerMovement
    // NOTE : transform.Translate does not work well with physics.
  }
}
