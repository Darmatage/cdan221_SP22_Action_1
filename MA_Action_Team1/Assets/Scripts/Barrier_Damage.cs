using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Barrier_Damage : MonoBehaviour {
       private Renderer rend;
       //public Animator anim;
       //public GameObject healthLoot;
       public int maxHealth = 40;
       public int currentHealth;
	   
	public GameObject barrierArt;
	public GameObject barrierBlastedArt;	

	void Start(){
		rend = GetComponentInChildren<Renderer> ();
		//anim = GetComponentInChildren<Animator> ();
		currentHealth = maxHealth;
			  
		barrierArt.SetActive(true);
		barrierBlastedArt.SetActive(false);
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
			barrierArt.SetActive(false);
			barrierBlastedArt.SetActive(true);
			  
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
