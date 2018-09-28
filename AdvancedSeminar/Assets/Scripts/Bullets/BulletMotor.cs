using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMotor : MonoBehaviour {

   public BulletData mBulletData;

   public string mTargetID = "";
   public GameObject mTarget;
   public float mTargetRadius = .1f;
   public float mSlowRadius = 1f;
   public float mTimeToTarget = 1f;
   public float mMaxAcceleration = 2f;
   
   private Rigidbody2D mRigidbody2D;

   float mFramesPast = 0;

   void Start()
   {
      mRigidbody2D = GetComponent<Rigidbody2D>();
      GetComponent<SpriteRenderer>().color = mBulletData.color;
      mTarget = GameObject.Find(mTargetID);

      if(mBulletData.fireType == BulletData.FireType.SEEK_PERFECT)
      {
         mRigidbody2D.AddForce(transform.up * mBulletData.startForce);
      }
      transform.localScale *= mBulletData.scale;
   }

   void Update ()
   {
      TrackLife();
      MoveForward();
      SeekPerfect();
	}

   void TrackLife()
   {
      mFramesPast++;
      if (mFramesPast >= mBulletData.life)
      {
         Destroy(gameObject);
      }
   }

   void MoveForward()
   {
      if(mBulletData.fireType == BulletData.FireType.STRAIGHT)
      {
         transform.Translate(transform.up * mBulletData.speed * Time.deltaTime);
      }
   }

   void SeekPerfect()
   {
      if (mBulletData.fireType == BulletData.FireType.SEEK_PERFECT)
      {
         Debug.Log("seeking");
         Vector2 diff = (Vector2)mTarget.transform.position - (Vector2)transform.position;
         if (Mathf.Abs(diff.magnitude) <= mTargetRadius)
         {
            Debug.Log("targethit");
            mRigidbody2D.velocity = Vector2.zero;
            return;
         }

         float targetSpeed = 0;
         if (diff.magnitude > mSlowRadius)
         {
            targetSpeed = mBulletData.speed;
         }
         else
         {
            targetSpeed = mBulletData.speed * diff.magnitude / mTimeToTarget;
         }

         Vector2 targetVelocity = diff;
         targetVelocity.Normalize();
         targetVelocity *= targetSpeed;

         Vector2 acceleration = targetVelocity - mRigidbody2D.velocity;
         acceleration /= mTimeToTarget;

         if (acceleration.magnitude > mMaxAcceleration)
         {
            acceleration.Normalize();
            acceleration *= mMaxAcceleration;
         }

         mRigidbody2D.AddForce(acceleration);
         
         return;
      }
   }
   
   private void OnTriggerEnter2D(Collider2D col)
   {
      if(col.gameObject.tag == "BulletCatch")
      {
         Destroy(gameObject);
      }

      if(col.gameObject.name == mTargetID)
      {
         if(mTargetID == "Boss")
         {
            mTarget.GetComponent<BossHealth>().damage(mBulletData.damage);
         }
         if (mTargetID == "Player")
         {
            mTarget.GetComponent<Player>().damage(mBulletData.damage);
         }
         Destroy(gameObject);
      }
   }

}