using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    
    public static Transform[] FOLDERS;

    public Transform[] folders;
    

	// Use this for initialization
	void Start () {
        FOLDERS = folders;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Transform GetParFol()
    {
        return FOLDERS[2];
    }
    public static Transform GetAAFol()
    {
        return FOLDERS[3];
    }

}
