using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Warp_PowerDelivery : MonoBehaviour{
	
	//This script creates a VFX buffer between NPC defeat and player powerup loot drop
	
	public GameObject powerUp;
	private SpriteRenderer warpVFXArt;
	public Color WarpColor = Color.white;
	public float endTime = 3f;

	void Start(){
		warpVFXArt = GetComponentInChildren<SpriteRenderer>();
		warpVFXArt.color = WarpColor;
		StartCoroutine(selfDestruct());
	}

	
	IEnumerator selfDestruct(){
        yield return new WaitForSeconds(endTime);
		warpVFXArt.enabled = false;
		Instantiate (powerUp, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(0.5f);
		Destroy (gameObject);
	}
	  
	  
}
