using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    // this camera should control the camera

    [SerializeField]
    private Transform Self; // this will be the transform of the object so I can have all the scripts on one object

    public float RotationSpeed = 1; // the rotation speed of the camera
    public Transform Target, Player; // the player object and pivot point for the camera
    float mouseX, mouseY; // the input from the mouse position
    // NOTE will need to make seperate input for controller inputs

    public Transform Obstruction; // this will be any wall that is blocking the camera
    public float zoomInSpeed = 2f, zoomOutSpeed = 2f, zoomOutTime = 1f; // this is how fast the camera will zoom in or out

    private float zoomClock = 0f;

    [SerializeField]
    private float cameraDistance = 4.5f, obstructionDistance = 3f, targetDistance = 1.5f;
    /*
      cameraDistance : this will be the distance from the player that the camera is
      obstructionDistance : this will be the distance from the wall that the camera zoom in will trigger
      targetDistance : this will be as close to the player that the camera will zoom in
    */

    void Start () // will be called once at the start of the scene
    {
      Obstruction = Target; // default starting value for obstruction function
      Cursor.visible = false; // makes the cursor invisible
      Cursor.lockState = CursorLockMode.Locked; // prevents the cusor from moving.
    }

    void LateUpdate () // will be called once per update after normal update
    {
      CamControl(); // calls the CamControl function
      ViewObstructed(); // calls the ViewObstructed function
    }

    void CamControl () // will control the camera
    {
      // Getting inputs
      mouseX += Input.GetAxis("Mouse X") * RotationSpeed; //gets input based on Mouse X axis, adds it to mouseX and multiplies by RotationSpeed
      mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed; //gets input based on Mouse Y axis, adds it to mouseY and multiplies by RotationSpeed
      mouseY = Mathf.Clamp(mouseY, -35, 60); // prevents the camera from flipping over the player

      //transform.LookAt(Target); // will keep the camera looking at Target
      // NOTE this might be redundant

      Target.rotation = Quaternion.Euler(mouseY, mouseX, 0); // will rotate the Target along its x and y axis
      Player.rotation = Quaternion.Euler(0,mouseX, 0); // will rotate the Player along its y axis
    }

    void ViewObstructed() // will control the camera for when the player is blocked by a wall
    {
      RaycastHit hit; // sets up a RaycastHit variable named hit

      if(Physics.Raycast(Self.position, Target.position - Self.position, out hit, cameraDistance))// sends out a raycast between current position and target position, if hits something it will trigger
      {
        if (hit.collider.gameObject.tag != "Player") // checks to see if the gameObject is not the player
        {
          Obstruction = hit.transform; // sets the obstruction to the new target
          Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly; // makes object invisable but keeps Shadows

          if(Vector3.Distance(Obstruction.position, Self.position) >= obstructionDistance && Vector3.Distance(Self.position, Target.position) >= targetDistance) // will control if the camera zooms in or not based on distance between player and wall blocking the player
          {
            zoomClock = 0f;

            Debug.DrawRay(Self.position,Target.position - Self.position,Color.red,0.5f,false);

            Self.Translate(Vector3.forward * zoomInSpeed * Time.deltaTime); // zooms the camera in
          }
        }
        else
        {
          Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On; // makes the wall visable again
          if(Vector3.Distance(Self.position, Target.position) < cameraDistance) // checks to see if the camera has been zoomed in
          {
            zoomClock += Time.deltaTime;

            if(zoomClock >= zoomOutTime)
            {
              Self.Translate(Vector3.back * zoomOutSpeed * Time.deltaTime); // resents camera to default position
            }
          }
        }
      }
    }
}
