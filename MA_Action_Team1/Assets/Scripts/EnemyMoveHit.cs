using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

       public Animator anim;
       public float speed = 4f;
       public Transform target;
       public int damage = 10;
	   public float attackPush = 3.5f;

       public int EnemyLives = 3;
       public GameHandler gameHandler;

       public float attackRange = 10;
       public bool isAttacking = false;
       private float scaleX;

       void Start () {
              anim = GetComponentInChildren<Animator>();
              scaleX = gameObject.transform.localScale.x;

              if (GameObject.FindGameObjectWithTag ("Player") != null) {
                     target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
              }

              if (GameObject.FindWithTag ("GameHandler") != null) {
                  gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
              }
       }

       void Update () {
              float DistToPlayer = Vector3.Distance(transform.position, target.position);

              if ((target != null) && (DistToPlayer <= attackRange)){
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
			isAttacking = true;
			anim.SetBool("Attack", true);
			gameHandler.playerGetHit(damage);
					 
			float pushBackX = 0f;
			if (other.gameObject.transform.position.x > gameObject.transform.position.x){
				pushBackX = attackPush;
			}
			else {
				pushBackX = attackPush * -1;
			}
			
			float pushBackY = 0f;
			if (other.gameObject.transform.position.y > gameObject.transform.position.y){
				pushBackY = attackPush;
			}
			else {
				pushBackY = attackPush * -1;
			}
			
			other.gameObject. transform.position = new Vector3(transform.position.x + pushBackX, transform.position.y + pushBackY, 0);
		}			 
	}

	public void OnCollisionExit2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			isAttacking = false;
			anim.SetBool("Attack", false);
		}
	}

       //DISPLAY the range of enemy's attack when selected in the Editor
       void OnDrawGizmosSelected(){
              Gizmos.DrawWireSphere(transform.position, attackRange);
       }
}