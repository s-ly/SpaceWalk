using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour {
  public GameObject block;
  public GameObject Block_Floor;
  public GameObject Block_Wall;
  public GameObject Block_Angle;

  float[,] my2DArray = {
      { 2.3f, 1.0f, 2.0f},
      { 1.3f, 0.0f, 1.1f},
      { 2.2f, 1.2f, 2.1f}
    };

  void Start() {
    ArrayUse();
  }

  void ArrayUse() {
    int loc_z = 0;
    int loc_x = 0;

    for (int i = 0; i < my2DArray.GetLength(0); i++) {
      for (int j = 0; j < my2DArray.GetLength(1); j++) {
        float point_block = my2DArray[i, j];
        int typePart = (int)point_block;
        float floatPart = (point_block - typePart) * 10;
        int intPartRot = Mathf.RoundToInt(floatPart);
        InstanceBlock(loc_z, loc_x, typePart, intPartRot);
        loc_x++;
      }
      loc_x = 0;
      loc_z--;
    }
  }

  void InstanceBlock(int loc_z, int loc_x, int type_block, int rot_block) {
    Vector3 local_block = new Vector3(loc_x, 0, loc_z);
    
    float rotationBlock = 0;
    if (rot_block == 1) { rotationBlock = 90; }
    else if (rot_block == 2) { rotationBlock = 180; }
    else if (rot_block == 3) { rotationBlock = 270; }
    Quaternion rotation = Quaternion.Euler(0, rotationBlock, 0);
    
    if (type_block == 0) {
      Instantiate(Block_Floor, local_block, rotation);
    }
    else if (type_block == 1) {
      Instantiate(Block_Wall, local_block, rotation);
    }
    else {
      Instantiate(Block_Angle, local_block, rotation);
    }
  }
}
