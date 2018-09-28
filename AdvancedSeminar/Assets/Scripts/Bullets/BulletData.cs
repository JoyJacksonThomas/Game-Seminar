using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBullet", menuName = "BulletType")]
public class BulletData : ScriptableObject{
   public enum FireType
   {
      STRAIGHT,
      SEEK_PERFECT
   };
   public int damage;
   public float life;
   public float speed;
   public float startForce;
   public FireType fireType;
   public float fireRate;
   public Sprite sprite;
   public Color color;
   public float scale;
}
