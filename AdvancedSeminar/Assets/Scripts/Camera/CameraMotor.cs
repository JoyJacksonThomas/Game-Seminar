using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

   public GameObject[] mThingsToEnable;

	void Start () {
      for(int i = 0; i < mThingsToEnable.Length; i++)
      {
         if (!mThingsToEnable[i].active)
         {
            mThingsToEnable[i].SetActive(true);
         }
      }
		
	}
	
	void Update () {
		
	}
}
