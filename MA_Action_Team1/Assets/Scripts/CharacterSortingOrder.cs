using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSortingOrder : MonoBehaviour {

      // Update is called once per frame
      void Update() {
            GetComponentInChildren<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
      }
}
