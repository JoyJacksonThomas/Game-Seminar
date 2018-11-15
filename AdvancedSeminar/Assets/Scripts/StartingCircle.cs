using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartingCircle : MonoBehaviour {

   public bool mPlayerOver = false;

   public int TIME_TO_START = 4;
   private float mTimeToStart = 3;
   
   private TextMeshProUGUI cirleTimerText;
   private bool mAudioPlayed = false;

   void Start()
   {
      cirleTimerText = GameObject.Find("CircleTimerText").GetComponent<TextMeshProUGUI>();
   }

   void Update () {
      if (mAudioPlayed)
      {
         if (AudioManager.instance.getIsPlaying("GameStart") == false)
         {
            SceneManager.LoadScene(1);
         }
      }
      else
      {
         if (mPlayerOver == true)
         {
            mTimeToStart -= Time.deltaTime;
            cirleTimerText.text = ((int)mTimeToStart).ToString();
         }
         else
         {
            mTimeToStart = TIME_TO_START;
            cirleTimerText.text = "";
         }
      }
      

      if(mTimeToStart < 1 && mAudioPlayed == false)
      {
         AudioManager.instance.playSound("GameStart");
         mAudioPlayed = true;
      }
      
	}
   

   private void OnTriggerEnter2D(Collider2D col)
   {
      if(col.tag == "Player")
      {
         mPlayerOver = true;
      }
   }

   private void OnTriggerExit2D(Collider2D col)
   {
      if (col.tag == "Player")
      {
         mPlayerOver = false;
      }
   }
}
