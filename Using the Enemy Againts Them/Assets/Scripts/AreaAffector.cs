using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAffector : MonoBehaviour {

    public enum AREATYPE { DamageToPlayer, DamageToEnemies, DamageToEntities};
    public AREATYPE areaType;
    public float TimeTillDeath;
    public int damage;
    public int force;

    

    private void Start()
    {
        Destroy(gameObject, TimeTillDeath);
    }
}
