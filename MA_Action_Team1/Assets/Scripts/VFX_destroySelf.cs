using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_destroySelf : MonoBehaviour{

    void Start(){
     StartCoroutine(destroySelf());   
    }

    IEnumerator destroySelf(){
        yield return new WaitForSeconds(2.5f);
		Destroy(gameObject);
    }
}
