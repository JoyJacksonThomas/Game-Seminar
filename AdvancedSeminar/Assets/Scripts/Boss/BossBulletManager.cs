using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager: MonoBehaviour {

   public enum GunType{
      STAR_BURST,
      NUM_GUNS
   }

   public enum AimingType
   {
      SPINNING,
      FACING_FORWARD,
      LOOK_AT_PLAYER,
      NUM_AIMING_TYPES
   }

   public Gun mGun;
   public GunType mCurrentGunType = GunType.STAR_BURST;

   public AimingType mCurrentAimingType = AimingType.SPINNING;

   private int[] mTimePast;
   public float mRotationSpeed;
   public bool mShouldBeFiring = false;

   public GameObject mBulletPrefab;
   
	void Start () {

      mTimePast = new int[mGun.bullets.Length];
      for (int i = 0; i < mTimePast.Length; i++)
      {
         mTimePast[i] = 0;
      }
   }
	
	void Update ()
   {
      for (int i = 0; i < mTimePast.Length; i++)
      {
         mTimePast[i]++;
      }
      if (mShouldBeFiring)
      {
         Aim();
         Fire();
      }
	}

   void Aim()
   {
      if (mCurrentAimingType == AimingType.SPINNING)
      {
         transform.Rotate(Vector3.forward * mRotationSpeed * Time.deltaTime);
      }
      else if (mCurrentAimingType == AimingType.FACING_FORWARD)
      {
         transform.rotation = Quaternion.Euler(0,0,0);
      }
      else if (mCurrentAimingType == AimingType.LOOK_AT_PLAYER)
      {

      }
   }

   void Fire()
   {
      for (int i = 0; i < mGun.bullets.Length; i++)
      {
         if (mTimePast[i] > mGun.bullets[i].fireRate)
         {
            Vector2 offset = mGun.bulletSpawnOffSets[i];
            GameObject pew = Instantiate(mBulletPrefab, transform.position + (Vector3)offset,
               Quaternion.Euler(0f, 0f, mGun.bulletRotationOffSets[i] + transform.rotation.eulerAngles.z)) as GameObject;
            pew.GetComponent<BulletMotor>().mBulletData = mGun.bullets[i];
            mTimePast[i] = 0;
         }
      }
   }
}
