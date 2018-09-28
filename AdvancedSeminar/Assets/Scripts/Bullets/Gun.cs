using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGunType", menuName = "GunType")]
public class Gun : ScriptableObject
{
   public BulletData[] bullets;
   public Vector2[] bulletSpawnOffSets;
   public float[] bulletRotationOffSets;
}
