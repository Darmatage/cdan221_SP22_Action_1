using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dragon_Damage : MonoBehaviour{
	private Renderer rend;
    //public Animator anim;
    public GameObject newPowerLoot;
    public int maxHealth = 100;
	public int maxPossessed = 100;
    public static int currentHealth;
    public static int currentPossessed;
	public bool amIFriendly;

    void Start(){
		rend = GetComponentInChildren<Renderer> ();
		//anim = GetComponentInChildren<Animator> ();
		currentHealth = maxHealth;
		currentPossessed = maxPossessed;
    }
	
	void Update(){
		amIFriendly = GetComponent<NPC_Dragon_Main>().isFriendly;
	}

    public void TakeDamage(int damage){
        if (amIFriendly == true){
			currentHealth -= damage;
			//rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
            //StartCoroutine(ResetColor());
            //anim.SetTrigger ("Hurt");
			if (currentHealth <= 0){
               Die();
            }
		}
		else if (amIFriendly == false){
			currentPossessed -= damage;
			if (currentPossessed <= 0){
               GetComponent<NPC_Dragon_Main>().isFriendly = true;
			   Instantiate (newPowerLoot, transform.position, Quaternion.identity);
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
