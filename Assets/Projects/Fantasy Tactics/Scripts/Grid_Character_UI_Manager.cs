using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid_Character_UI_Manager : MonoBehaviour
{

    /*--Description--
      This script should manage any character dependant UI
    */

    /*--Notes--
      This script may be phased out latter
    */

    public MeshRenderer highlight;
    public Text nameText;
    public Text hpText;
    public Text apText;
    public Grid_Character_Turn_Manager Grid_Character_Turn_Manager;
    public Creature_Stats Creature_Stats;

    void Start ()
    {//(wip)
      highlight.enabled = false;
      apText.enabled = false;
      nameText.text = Creature_Stats.Name;
    }

    public void Update ()
    {//(wip)
      if (Grid_Character_Turn_Manager.isTurn)
      {
        if (highlight.enabled == false)
        {
          highlight.enabled = true;
        }
        if (apText.enabled == false)
        {
          apText.enabled = true;
        }
      }
      else
      {
        if (highlight.enabled == true)
        {
          highlight.enabled = false;
        }
        if (apText.enabled == true)
        {
          apText.enabled = false;
        }
      }
      hpText.text = "Hp " + Creature_Stats.currentHealth;
      apText.text = "Ap " + Grid_Character_Turn_Manager.numOfAvailibleActions;
    }
}
