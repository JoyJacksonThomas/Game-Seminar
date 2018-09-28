using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

   public int mHealth = 10;

   public Gun mGun;
   public GameObject mBulletPrefab;

   Rigidbody2D mRigidbody2D;
   int[] mTimePast = new int[0];

   public float mSpeedFactor = 1;
   private Vector2 mLastFrameMousePos;

   void Start()
   {
      mRigidbody2D = GetComponent<Rigidbody2D>();
      mLastFrameMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.position = mLastFrameMousePos;
      mTimePast = new int[mGun.bullets.Length];
      for(int i = 0; i < mTimePast.Length; i++)
      {
         mTimePast[i] = 0;
      }
   }

   void Update ()
   {
      if (mHealth <= 0)
      {
         Destroy(gameObject);
      }

      for (int i = 0; i < mTimePast.Length; i++)
      {
         mTimePast[i]++;
      }
      //Movement();
      transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 3);
      HandleInput();
   }

   void HandleInput()
   {
      if(Input.GetButton("Fire1"))
      {
         Fire();
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

   void Movement()
   {
      Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 diff = mousePos - mLastFrameMousePos;
      diff *= mSpeedFactor;
      transform.position += new Vector3(diff.x, diff.y, 0);
      mLastFrameMousePos = mousePos;
   }

   public void damage(int damageToTake)
   {
      mHealth -= damageToTake;
   }

   void OnTriggerExit2D(Collider2D col)
   {
      if(col.tag == "PlayArea")
      {
         mSpeedFactor = .3f;
      }
   }
   void OnTriggerStay2D(Collider2D col)
   {
      if (col.tag == "PlayArea")
      {
         mSpeedFactor = 1f;
      }
   }


}
