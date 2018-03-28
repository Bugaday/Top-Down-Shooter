using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 14.5f));

        transform.position = mousePos;
    }
}
