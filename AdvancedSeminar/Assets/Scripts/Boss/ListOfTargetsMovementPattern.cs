using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTargetPattern", menuName = "ListOfTargetsMovementPattern")]
public class ListOfTargetsMovementPattern : ScriptableObject{
   public Vector2[] mTargets;
   public float[] mSpeedToTarget;
   public bool[] mShootOnTravel;
   public bool[] mShootOnArrive;
   public int mCurrentTargetIndex = 0;
}
