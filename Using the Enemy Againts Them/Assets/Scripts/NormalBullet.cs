using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour {

    private int damage;
    private float acceleration = 100f;
    private float maxSpeed = 1f;
    private float force = 100f;
    private Vector3 direction;
    private Rigidbody rb;

    private BasicBulletStructure.BULLETTYPE bulletType;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        direction = GetComponent<BasicBulletStructure>().direction;

        
        bulletType = GetComponent<BasicBulletStructure>().bulletType;

        force = GetComponent<BasicBulletStructure>().force;
        acceleration = GetComponent<BasicBulletStructure>().acceleration;
        damage = GetComponent<BasicBulletStructure>().damage;
        maxSpeed = GetComponent<BasicBulletStructure>().maxSpeed;
}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.AddForce(rb.velocity.normalized * acceleration);
        if(rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed * Time.deltaTime;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<BasicEnemyStructure>().health -= damage;
            collision.gameObject.GetComponent<BasicEnemyStructure>().attackedDirection = direction;
            collision.gameObject.GetComponent<BasicEnemyStructure>().attackedBulletType = bulletType;
            
            collision.rigidbody.AddForce(direction * force);
            Destroy(transform.gameObject);
        }
    }
}
