using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWalls : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, transform.GetComponent<BoxCollider>().size);
        
    }
}
