using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementManager : MonoBehaviour {

   public enum MovementType{
      SIDE_TO_SIDE,
      NOT_MOVING
   };
   public MovementType mCurrentMovementType;

   public ListOfTargetsMovementPattern[] mListOfTargetsMovement;
   
   public float mTargetRadius = .1f;
   public float mSlowRadius = 1f;
   public float mTimeToTarget = 1f;
   public float mMaxAcceleration = 2f;

   private Rigidbody2D mRigidbody2D;

   private BossBulletManager mBulletPatterns;

	void Start () {
      mRigidbody2D = GetComponent<Rigidbody2D>();
      mBulletPatterns = GameObject.Find("BossBulletSpawn").GetComponent<BossBulletManager>();
      //mCurrentMovementType = MovementType.SIDE_TO_SIDE;
	}
	
	void Update () {
      moveToPoints();
   }
   
   void moveToPoints()
   {
      ListOfTargetsMovementPattern currentPattern = mListOfTargetsMovement[(int)mCurrentMovementType];
      Vector2 diff = currentPattern.mTargets[currentPattern.mCurrentTargetIndex] - (Vector2)transform.position;
      if (Mathf.Abs(diff.magnitude) <= mTargetRadius)
      {
         currentPattern.mCurrentTargetIndex++;
         currentPattern.mCurrentTargetIndex %= currentPattern.mTargets.Length;
         mBulletPatterns.mShouldBeFiring = currentPattern.mShootOnArrive[currentPattern.mCurrentTargetIndex];
         mRigidbody2D.velocity = Vector2.zero;
         return;
      }

      float targetSpeed = 0;
      if(diff.magnitude > mSlowRadius)
      {
         targetSpeed = currentPattern.mSpeedToTarget[currentPattern.mCurrentTargetIndex];
      }
      else
      {
         targetSpeed = currentPattern.mSpeedToTarget[currentPattern.mCurrentTargetIndex] * diff.magnitude / mTimeToTarget;
      }

      Vector2 targetVelocity = diff;
      targetVelocity.Normalize();
      targetVelocity *= targetSpeed;

      Vector2 acceleration = targetVelocity - mRigidbody2D.velocity;
      acceleration /= mTimeToTarget;

      if(acceleration.magnitude > mMaxAcceleration)
      {
         acceleration.Normalize();
         acceleration *= mMaxAcceleration;
      }

      mRigidbody2D.AddForce(acceleration);

      mBulletPatterns.mShouldBeFiring = currentPattern.mShootOnTravel[currentPattern.mCurrentTargetIndex];
      return;
      
   }

}
