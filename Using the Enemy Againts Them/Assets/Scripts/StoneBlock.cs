using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : BasicBuildingStruct {
    
    private Vector3 attackedBulletDirection;

    [SerializeField] private float speed;
   
    private bool move;
    private Vector3 point;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (move)
        {
            Debug.Log("Move");
            transform.position += (point - transform.position) / speed * Time.deltaTime;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {

        GameObject colObj = collision.gameObject;
        if(colObj.tag == "Projectile")
        {
            
            Vector3 direction = colObj.GetComponent<BasicBulletStructure>().direction;

            

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("Hit something");
                
                move = true;
                point = hitInfo.point;

                point = new Vector3((int)point.x, transform.position.y, (int)point.z);

            }
            else
            {
                Debug.Log("Ray did not hit anything");
            }
            print(direction);
            
            Destroy(colObj);
        }
        Debug.Log("working");
        

    }

    private void OnTriggerEnter(Collider other)
    {

            Debug.Log("Not Moving anymore");
            move = false;
        
    }

}
