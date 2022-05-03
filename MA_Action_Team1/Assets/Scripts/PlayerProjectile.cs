using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

	public int damage = 10;
	public GameObject hitEffectAnim;
	public GameObject fizzleAnim;
	public float destroyTime = 0.8f;
	public float SelfDestructTime = 2.0f;
	private SpriteRenderer projectileArt;

	void Start(){
		projectileArt = GetComponentInChildren<SpriteRenderer>();
		StartCoroutine(selfDestruct());
	}

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
				  other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(damage);
            }
			else if (other.gameObject.layer == LayerMask.NameToLayer("Possessed")) {
				  other.gameObject.GetComponent<NPC_Dragon_Damage>().TakeDamage(damage);
				  Debug.Log("I hit a possessed NPC dragon");
            }

			if (other.gameObject.layer == LayerMask.NameToLayer("BarrierWood")) {
				  other.gameObject.GetComponent<Barrier_Damage>().TakeDamage(damage);
            }


           if ((other.gameObject.tag != "Player") && (other.gameObject.layer != LayerMask.NameToLayer("Friendlies"))){
                  GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
                  projectileArt.enabled = false;
				  StopCoroutine (selfDestruct());
                  StartCoroutine (destroyBlast(animEffect));
                  //Destroy (gameObject);
            }
      }

	IEnumerator destroyBlast(GameObject VFX){
		yield return new WaitForSeconds(destroyTime);
        Destroy(VFX);
        Destroy (gameObject);
	}
	  
	IEnumerator selfDestruct(){
        yield return new WaitForSeconds(SelfDestructTime);
		GameObject fizzleEffect = Instantiate (fizzleAnim, transform.position, Quaternion.identity);
		fizzleEffect.transform.parent = gameObject.transform;
		projectileArt.enabled = false;
		yield return new WaitForSeconds(0.5f);
        Destroy (fizzleEffect);
		Destroy (gameObject);
	}
	  
	  
}
