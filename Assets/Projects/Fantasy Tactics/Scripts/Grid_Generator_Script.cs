using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Generator_Script : MonoBehaviour
{
    /*-- Description --
      This script will generate a 3d grid of what ever is set as the gridUnitPrefab along the set rows columns and layers.

    */

    /*-- Notes --
        This script will generate the grid on start, perhaps the code should be reworked to be called whenever.
        Make it more user freindly
    */

    public int rows = 2, columns = 2, layers = 1, unitSpacing = 2, startX = 0, startY = 0, startZ = 0;

    public Transform gridUnitPrefab, gridHolder; // gridUnitPrefab : what is made at each grid cordinate.  gridHolder : what holds all the gridUnitPrefabs

    public int numOfGridUnits = 0; // this variable is just used in testing, maybe I can delete it or add a toggle

    public bool debugLogs_01 = false, debugLogs_02 = false;

    public void GenerateGrid (int rows = 2, int columns = 2, int layers = 1, int startX = 0, int startY = 0, int startZ = 0, int unitSpacing = 2)
    { //this function generates a grid using its given variables
      int x = startX, y = startY, z = startZ;
      for (int l = 0; l < layers; l++)
      {
        x = startX;
        for (int r = 0; r < rows; r++ )
        {
          z = startZ;
          for (int c = 0; c < columns; c++)
          {

            Transform gridUnitPrefabClone = Instantiate(gridUnitPrefab, new Vector3(x,y,z), Quaternion.LookRotation(Vector3.down,Vector3.up), gridHolder);
            Grid_Unit_Script gridUnitPrefabCloneScript = gridUnitPrefabClone.GetComponent<Grid_Unit_Script>();
            gridUnitPrefabCloneScript.x = x - startX;
            gridUnitPrefabCloneScript.x = gridUnitPrefabCloneScript.x/unitSpacing;
            gridUnitPrefabCloneScript.y = y - startY;
            gridUnitPrefabCloneScript.y = gridUnitPrefabCloneScript.y/unitSpacing;
            gridUnitPrefabCloneScript.z = z - startZ;
            gridUnitPrefabCloneScript.z = gridUnitPrefabCloneScript.z/unitSpacing;

            z += unitSpacing;

            numOfGridUnits++;
            if(debugLogs_01) print ("The current number of grid units is " + numOfGridUnits + " units.");
          }
          x += unitSpacing;
        }
        y += unitSpacing;
      }

      if(debugLogs_01 || debugLogs_02) print ("The total number of grid units should be " + numOfGridUnits + " units.");
    }


}
