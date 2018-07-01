using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {
    public float timeTillDeath = 2f;

	// Use this for initialization
	void Start () {
        Destroy(transform.gameObject, timeTillDeath);
	}
}
