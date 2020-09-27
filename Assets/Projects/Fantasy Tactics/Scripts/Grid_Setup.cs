using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Setup : MonoBehaviour
{

  public bool setupIsActive = false;

  public Grid_Generator_Script Grid_Generator_Script;
  public Grid_Turn_Manager Grid_Turn_Manager;

    // Start is called before the first frame update
    void Start()
      {
        setUpGrid();
      }

    private void setUpGrid()
      {
        if (setupIsActive)
          {
            int rows = Grid_Generator_Script.rows;
            int columns = Grid_Generator_Script.columns;
            int layers = Grid_Generator_Script.layers;
            int unitSpacing = Grid_Generator_Script.unitSpacing;
            int startX = Grid_Generator_Script.startX;
            int startY = Grid_Generator_Script.startY;
            int startZ = Grid_Generator_Script.startZ;

            Grid_Generator_Script.GenerateGrid(rows,columns,layers,startX,startY,startZ,unitSpacing);

            Grid_Turn_Manager.setUpTurnOrder();
            Grid_Turn_Manager.setUpGrid();
            Grid_Turn_Manager.nextTurn();

            print ("grid should be good to go");
          }
        else
          {
            print ("you will need to set up the grid your self");
          }
      }
}
