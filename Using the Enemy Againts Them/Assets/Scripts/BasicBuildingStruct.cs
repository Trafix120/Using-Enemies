using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuildingStruct : MonoBehaviour {

    public enum Building { Door, BlockOfStone};
    public Building building;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject colObject = collision.gameObject;
        // These statements are for those behaviors that are simple enough to write
        if (colObject.tag == "Player")
        {
            if (building == Building.Door && colObject.GetComponent<Player>().numKeys > 0)
            {
                Debug.Log("Open Door");
                OpenDoor();
            }
        }


    }
    // These methods are for those behaviors that are simple enough to write
    private void OpenDoor()
    {
        Destroy(gameObject);
    }
}
