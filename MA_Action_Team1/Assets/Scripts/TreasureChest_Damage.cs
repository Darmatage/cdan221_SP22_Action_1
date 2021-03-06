using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TreasureChest_Damage : MonoBehaviour {
       private Renderer rend;
       //public Animator anim;
       //public GameObject healthLoot;
       public int maxHealth = 40;
       public int currentHealth;
	   
	public GameObject closedArt;
	public GameObject openArt;	

	public GameObject treasure;


	void Start(){
		rend = GetComponentInChildren<Renderer> ();
		//anim = GetComponentInChildren<Animator> ();
		currentHealth = maxHealth;
			  
		closedArt.SetActive(true);
		openArt.SetActive(false);
	}

       public void TakeDamage(int damage){
              currentHealth -= damage;
              rend.material.color = new Color(2.4f, 0.9f, 0.9f, 1f);
              StartCoroutine(ResetColor());
              //anim.SetTrigger ("Hurt");
              if (currentHealth <= 0){
                     Blast();
              }
       }

       void Blast(){
            //Instantiate (healthLoot, transform.position, Quaternion.identity);
            //anim.SetBool ("Die", true);
            GetComponent<Collider2D>().enabled = false;
			closedArt.SetActive(false);
			openArt.SetActive(true);
			
			Instantiate (treasure, transform.position, Quaternion.identity);
			  
              //StartCoroutine(Death());
       }

       //IEnumerator Death(){
              //yield return new WaitForSeconds(0.1f);
              //Debug.Log("You burnt a log");
              //Destroy(gameObject);
       //}

       IEnumerator ResetColor(){
              yield return new WaitForSeconds(0.1f);
              rend.material.color = Color.white;
       }
}
