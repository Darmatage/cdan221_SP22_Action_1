using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour{

      public int damage = 1;
      public GameObject hitEffectAnim;
      public float SelfDestructTime = 3.0f;

      void Start(){
           //StartCoroutine(selfDestruct());
      }

      //if the bullet hits a collider, play the explosion animation, then destroy the effect and the bullet
      void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
                  //gameHandlerObj.playerGetHit(damage);
            }
           if (other.gameObject.tag != "Player") {
                  GameObject animEffect = Instantiate (hitEffectAnim, transform.position, Quaternion.identity);
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
