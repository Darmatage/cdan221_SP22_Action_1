using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HereIsBuddy : MonoBehaviour{
	
	public bool isBuddy1 = true;
	public bool isBuddy2 = false;
	public bool isBuddy3 = false; 
	
    // Start is called before the first frame update
    void Start(){
		if (isBuddy1){StartCoroutine(addBuddy1());}	
		else if (isBuddy2){StartCoroutine(addBuddy2());}	
		else if (isBuddy3){StartCoroutine(addBuddy3());}	
	}

    IEnumerator addBuddy1(){
		yield return new WaitForSeconds(0.01f);
		GameObject.FindWithTag("GameHandler").GetComponent<WhereIsBuddy>().AddBuddy(gameObject.transform);
    }
	
	IEnumerator addBuddy2(){
		yield return new WaitForSeconds(0.05f);
		GameObject.FindWithTag("GameHandler").GetComponent<WhereIsBuddy>().AddBuddy(gameObject.transform);
    }
	
	IEnumerator addBuddy3(){
		yield return new WaitForSeconds(0.1f);
		GameObject.FindWithTag("GameHandler").GetComponent<WhereIsBuddy>().AddBuddy(gameObject.transform);
    }

}
