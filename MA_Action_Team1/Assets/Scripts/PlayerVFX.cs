using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerVFX : MonoBehaviour{

	public GameObject powerUp1;
	public GameObject powerUp2;
	
	public Color fireColor;
	public Color iceColor;
	public Color lightningColor;
	
	
	private SpriteRenderer PU1color;
	private SpriteRenderer PU2color;

	void Start(){
		powerUp1.SetActive(false);
		powerUp2.SetActive(false);
		PU1color = powerUp1.GetComponent<SpriteRenderer>();
		PU2color = powerUp2.GetComponent<SpriteRenderer>();
	}
		
	public void powerup(string color){
		Debug.Log("PowerUp VFX!");
			  
		if (color == "red"){
			  PU1color.color = fireColor;
			  PU2color.color = fireColor;
		}
		else if (color == "blue"){
			  PU1color.color = iceColor;
			  PU2color.color = iceColor;
		}
		else if (color == "yellow"){
			  PU1color.color = lightningColor;
			  PU2color.color = lightningColor;
		}
			  
			  
              StartCoroutine(playVFX1());
	}

        IEnumerator playVFX1(){
              powerUp1.SetActive(true);
              powerUp2.SetActive(true);
              yield return new WaitForSeconds(3.0f);
              powerUp1.SetActive(false);
              powerUp2.SetActive(false);
        }

} 