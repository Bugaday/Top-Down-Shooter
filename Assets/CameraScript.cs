using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;
    private Vector3 velocity = Vector3.zero;

    private float smoothTime = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        float step = smoothTime * Time.deltaTime;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos,ref velocity, step);
        //transform.position = new Vector3(player.position.x,transform.position.y,player.position.z);
	}
}
