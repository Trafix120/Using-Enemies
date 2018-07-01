using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnviroment : MonoBehaviour {
    public Transform bushes;
    public Sprite[] bushesImages;
    public int range = 100;
    public float probability = 20;

    public int xINT, yINT;
    public Transform bushesFol;
	// Use this for initialization
	void Start () {
        
        for (int y = -range; y < range; y++)
        {
            yINT = y;
            for (int x = -range; x < range; x++)
            {
                xINT = x;
                if (Random.Range(0, 100) < probability)
                {
                    InstantiateBush();
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
    
    private void InstantiateBush()
    {
        Transform bush = Instantiate(bushes, new Vector3(xINT,5 ,yINT), Quaternion.Euler(new Vector3(90, 0)), bushesFol);
        bush.GetComponent<SpriteRenderer>().sprite = bushesImages[Random.Range(0, bushesImages.Length)];
    }
}
