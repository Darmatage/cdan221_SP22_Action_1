using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      public int damage = 10;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 3.0f;
      private SpriteRenderer projectileArt;

      void Start(){
        projectileArt = GetComponentInChildren<SpriteRenderer>();
           //StartCoroutine(selfDestruct());
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

           if ((other.gameObject.tag != "Player") && (other.gameObject.layer != LayerMask.NameToLayer("Friendlies"))){
                  GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
                  projectileArt.enabled = false;
                  StartCoroutine (selfDestruct(animEffect));
                  //Destroy (gameObject);
            }
      }

      IEnumerator selfDestruct(GameObject VFX){
            yield return new WaitForSeconds(SelfDestructTime);
        Destroy(VFX);
        Destroy (gameObject);

      }
}
