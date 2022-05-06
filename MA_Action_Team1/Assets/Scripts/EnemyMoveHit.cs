using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

       public Animator anim;
	   public Rigidbody2D rb2D; 
       public float speed = 4f;
       private Transform target;
       public int damage = 10;
	   //public float attackPush = 3.5f;
	   public float knockBackForce = 20f;

       //public int EnemyLives = 3;
       private GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

	//Attack timer variables
	private float timeBtwHits;
	public float startTimeBtwHits = 1;

	void Start () {
		anim = GetComponentInChildren<Animator>();
		rb2D = GetComponentInChildren<Rigidbody2D> (); 
		scaleX = gameObject.transform.localScale.x;

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		}

		if (GameObject.FindWithTag ("GameHandler") != null) {
			gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
		}
	}

	private void FixedUpdate(){
		float DistToPlayer = Vector3.Distance(transform.position, target.position);
		
		//Timer for attacking player at intervals 
		if ((target != null) && (DistToPlayer <= attackRange)){
			if (timeBtwHits <= 0) {
				isAttacking = true;
				//anim.SetBool("Attack", true);
				//rotate towards enemy target?
				timeBtwHits = startTimeBtwHits;
			} else {
				timeBtwHits -= Time.deltaTime;
				//anim.SetBool("Attack", true);
			}
		}
		
		if (isAttacking){
			transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			if (isAttacking == false) {
				anim.SetBool("Walk", true);
				//flip enemy to face player direction. Wrong direction? Swap the * -1.
				if (target.position.x > gameObject.transform.position.x){
					gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
				} else {
					gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
				}
			}
			else  { anim.SetBool("Walk", false);}
		}
		else { anim.SetBool("Walk", false);}
	}


	public void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			anim.SetBool("Attack", true);
			gameHandler.playerGetHit(damage);
			isAttacking = false;
			
			//knockback			
			Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
			Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position; 
			pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * -1, ForceMode2D.Impulse); 
			StartCoroutine(EndKnockBack(pushRB));
		}			 
	}

	public void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			anim.SetBool("Attack", false);
		}
	}
	
	IEnumerator EndKnockBack(Rigidbody2D otherRB){
		yield return new WaitForSeconds(0.2f);
		otherRB.velocity= new Vector3(0,0,0);
	}

       //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}