using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Transform bullet;

    private float bulletVelocity = 5000f;
    private float speed = 10f;
    private float rotSpeed = 200f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float distFromCam = Camera.main.transform.position.y - transform.position.y;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,distFromCam));
        //Vector2 mousePos = Input.mousePosition;

        Vector3 mouseDir = mousePos - transform.position;

        Debug.Log(mouseDir);

        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(Vector3.back * speed * Time.deltaTime);
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        transform.rotation = Quaternion.LookRotation(mouseDir);

        if (Input.GetAxis("Mouse X") > 0)
        {
            //transform.Rotate(0,rotSpeed * Time.deltaTime,0);
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            //transform.Rotate(0, -rotSpeed * Time.deltaTime, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        GameObject firedBullet = Instantiate(bullet.gameObject,transform.position,Quaternion.identity);
        Rigidbody firedBulletRB = firedBullet.GetComponent<Rigidbody>();

        firedBulletRB.AddForce(transform.forward * bulletVelocity);
    }
}
