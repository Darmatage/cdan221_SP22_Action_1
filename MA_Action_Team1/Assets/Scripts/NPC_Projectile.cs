using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Projectile : MonoBehaviour{
	public GameHandler gameHandlerObj;
    public int damage =1;
	public float speed = 10f;
	private Transform playerTrans;
	private Vector2 target;
	public GameObject hitEffectAnim;
	public float SelfDestructTime = 2.0f;
	
	public bool attackPlayer = true;
	public Transform enemyTrans;


	void Start() {
		//NOTE: transform gets location, but we need Vector2 for direction, so we can use MoveTowards.
		
		if (attackPlayer==true){playerTrans = GameObject.FindGameObjectWithTag("Player").transform;}
		else {playerTrans = enemyTrans;}
		
		target = new Vector2(playerTrans.position.x, playerTrans.position.y);

		if (gameHandlerObj == null){
			gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		}
		StartCoroutine(selfDestruct());
	}

       void Update () {
              transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
       }

       //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
	void OnTriggerEnter2D(Collider2D other){
            
		if (attackPlayer==true){
			//possessed dragon hits player
			if (other.gameObject.tag == "Player") {
				gameHandlerObj.playerGetHit(damage);
			}
		} else {
			//buddy dragon hits enemy
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
				  other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(damage);
            }
		}
		
		if (other.gameObject.tag != "enemyShooter") {
			GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
			Destroy (animEffect, 0.5f);
			Destroy (gameObject);
		}
		
	}

	IEnumerator selfDestruct(){
		yield return new WaitForSeconds(SelfDestructTime);
		Destroy (gameObject);
	}
}