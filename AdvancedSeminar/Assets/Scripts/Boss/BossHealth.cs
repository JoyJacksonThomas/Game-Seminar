using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

   public int mBossHealth = 100;

	void Update () {
      if (mBossHealth <= 0)
      {
         Destroy(gameObject);
      }
	}

   public void damage(int damageToTake)
   {
      mBossHealth -= damageToTake;
   }
}
