using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public Transform player;
    public float range = 10f;
    public Transform enemyFol;
    

    // Declaration of objects
    public EnemyType[] enemyTypes;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    	for(int i = 0; i < enemyTypes.Length; i++)
        {
            EnemyType checkingEnemy = enemyTypes[i];
            checkingEnemy.BTWtime += Time.deltaTime;
            if(checkingEnemy.BTWtime >= checkingEnemy.rate && player != null)
            {
                checkingEnemy.BTWtime = 0;
                Vector3 pos = new Vector3(Random.Range(-100, 100) / 10, 2, Random.Range(-100, 100)/ 10);
                pos = Vector3.Normalize(pos);
                pos = pos * range + player.position;
                Instantiate(checkingEnemy.trans, pos, Quaternion.identity, enemyFol);
            }
        }
	}

    
    [System.Serializable]
    public class EnemyType {
        public string name;
        public Transform trans;
        public float rate;
        public float BTWtime;

    }

}
