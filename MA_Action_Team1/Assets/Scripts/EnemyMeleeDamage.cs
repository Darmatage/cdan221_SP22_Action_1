using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour {
	private Renderer rend;
	public Animator anim;
	public GameObject healthLoot;
	public int maxHealth = 100;
	public int currentHealth;

	public GameObject iceBlock1;
	public GameObject iceBlock2;
	public GameObject iceBlock3;
	public GameObject iceBlock4;

	public bool iGotFrozen = false;
	public bool isPosessedBuddy = false;

	void Start(){
		rend = GetComponentInChildren<Renderer> ();
		anim = GetComponentInChildren<Animator> ();
		currentHealth = maxHealth;
			  
		iceBlock1.SetActive(false);
		iceBlock2.SetActive(false);
		iceBlock3.SetActive(false);
		iceBlock4.SetActive(false); 
		  
	}

	public void TakeDamage(int damage){
		if (isPosessedBuddy == false){
			currentHealth -= damage;
			rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
			StartCoroutine(ResetColor());
			anim.SetTrigger ("Hurt");
			if (currentHealth <= 0){
				Die();
			}
		}
		else {
			//relay damage to the Buddy Damage script
			GetComponent<NPC_Dragon_Damage>().TakeDamage(damage);
		}
	}

	void Die(){
		Instantiate (healthLoot, transform.position, Quaternion.identity);
		anim.SetBool ("Die", true);
		GetComponent<Collider2D>().enabled = false;
		StartCoroutine(Death());
	}

	IEnumerator Death(){
		yield return new WaitForSeconds(0.5f);
		Debug.Log("You Killed a baddie. You deserve loot!");
		Destroy(gameObject);
	}

	IEnumerator ResetColor(){
        yield return new WaitForSeconds(0.5f);
        rend.material.color = Color.white;
	}
	
	//functions for getting frozen
	public void GetFrozen(){
		iGotFrozen = true;
		iceBlock1.SetActive(true);
		StartCoroutine(meltingTime());
	}	

	IEnumerator meltingTime(){
		rend.material.color = new Color(0f, 2.0f, 2.55f, 1f);
		yield return new WaitForSeconds(1f);
		iceBlock1.SetActive(false);
		iceBlock2.SetActive(true);
		yield return new WaitForSeconds(1f);
		iceBlock2.SetActive(false);
		iceBlock3.SetActive(true);
		yield return new WaitForSeconds(1f);
		iceBlock3.SetActive(false);
		iceBlock4.SetActive(true);
		yield return new WaitForSeconds(1f);
		iceBlock4.SetActive(false);
		iGotFrozen = false;
		rend.material.color = Color.white;
	}
	
}
