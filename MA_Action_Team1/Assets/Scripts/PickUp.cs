using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      public GameHandler gameHandler;
      //public playerVFX playerPowerupVFX;
      public bool isHealthPickUp = true;
      public bool isDiamond = false;
	  public bool isDiamondDoorOpen = false;
      public bool isPowerFire = false; // destroy log barriars and attack, small attack, fast recharge
	  public bool isPowerLightning = false; // destroy rock barriars, powerful attack, slow recharge
      public bool isPowerIce = false; // freeze lava or water to allow player to pass over, briefly freeze enemy	  
      public AudioSource Diamond_PickUpSFX;
    public AudioSource PowerUp_Fire_SFX;
    public AudioSource PowerUp_ice_SFX;
    public AudioSource PowerUp_Thunder_SFX;

      public int healthBoost = 10;
      //public float speedBoost = 2f;
      //public float speedTime = 2f;

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                  //GetComponent<AudioSource>().Play();
                  StartCoroutine(DestroyThis());

                if (isHealthPickUp == true) {
                        gameHandler.playerGetHit(healthBoost * -1);
                        //playerPowerupVFX.powerup();
                }

				if (isDiamond == true) {
                      gameHandler.playerGetDiamonds(1);
                //if(!Diamond_PickUp.isPlaying){
                Diamond_PickUpSFX.Play();

                }
				
				if (isDiamondDoorOpen == true) {
                      gameHandler.playerGetDiamonds(100);
					  //opening door
                }

                if (isPowerFire == true) {
                      gameHandler.GetNewPower("fire");
                //if(!PowerUp_Fire.isPlaying){
                PowerUp_Fire_SFX.Play();

                }
				if (isPowerIce == true) {
                      gameHandler.GetNewPower("ice");
                //if(!PowerUp_Ice.isPlaying){
                PowerUp_ice_SFX.Play();

                }
				if (isPowerLightning == true) {
                      gameHandler.GetNewPower("lightning");
                //if(!PowerUp_Thunder.isPlaying){
                PowerUp_Thunder_SFX.Play();
                }
            }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
      }

}
