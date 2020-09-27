using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Script : MonoBehaviour
{
  /*--Description--
    This script will hold everything needed for the main menu
  */

  /*--Notes--
    This script may have to change over time as the ui is changed
  */

  public Light fireLight;
  public float minPulse,maxPulse;
  [SerializeField]
  private float currentPulse;
  private bool isCountingUp;

    public void onClickLoadScene (int selection)
    {//(wip) this script should be called when the scene is selected to load
      SceneManager.LoadScene(selection);
    }

    public void onClickConfirmQuit ()
    {//(wip) this script should be called when the quit button is clicked
      Application.Quit();
    }

    public void pulse ()
    {//(wip) this script should control the pulsing of the fire
      if (currentPulse >= maxPulse) isCountingUp = false;
      if (currentPulse <= minPulse) isCountingUp = true;

      fireLight.intensity = currentPulse;

      if (isCountingUp) currentPulse += Time.deltaTime;
      else currentPulse -= Time.deltaTime;
    }

    public void Start ()
    {//(wip)
      currentPulse = minPulse;
    }

    public void Update ()
    {//(wip) this function will be called each frame
      pulse();
    }
}
