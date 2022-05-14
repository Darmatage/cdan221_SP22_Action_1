using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dragon_Damage : MonoBehaviour{
	private Renderer rend;
    public Animator anim;
    public GameObject newPowerLoot;
	public GameObject newPowerWarpVFX;
	public Color newPowerColor = Color.white;
	
    public int maxHealth = 100;
	public int maxPossessed = 100;
    public int currentHealth;
    public int currentPossessed;
	public bool amIFriendly;

    void Start(){
		rend = GetComponentInChildren<Renderer> ();
		anim = GetComponent<NPC_Dragon_Main>().anim;
		currentHealth = maxHealth;
		currentPossessed = maxPossessed;
		newPowerColor = GetComponent<NPC_Dragon_Main>().friendlyColor;
    }
	
	void Update(){
		amIFriendly = GetComponent<NPC_Dragon_Main>().amIFriendly();
		anim = GetComponent<NPC_Dragon_Main>().anim;
	}

    public void TakeDamage(int damage){
        if (amIFriendly == true){
			currentHealth -= damage;
			//rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
            //StartCoroutine(ResetColor());
            anim.SetTrigger ("Hurt");
			if (currentHealth <= 0){
               Die();
            }
		}
		else if (amIFriendly == false){
			currentPossessed -= damage;
			if (currentPossessed <= 0){
               GetComponent<NPC_Dragon_Main>().setFriendly();
			   
			   GameObject WarpEffect = Instantiate (newPowerWarpVFX, transform.position, Quaternion.identity);
			   WarpEffect.GetComponent<Warp_PowerDelivery>().WarpColor = newPowerColor;
			   WarpEffect.GetComponent<Warp_PowerDelivery>().powerUp = newPowerLoot;
			   
            }
		}
    }

    void Die(){
              
              //anim.SetBool ("isDead", true);
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
}
