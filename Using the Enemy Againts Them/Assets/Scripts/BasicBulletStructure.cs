using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletStructure : MonoBehaviour {
    public  enum BULLETTYPE {Null, SmallFireBall, BigFireBall};
    public BULLETTYPE bulletType;
    public float force;
    public float acceleration;
    public int damage;
    public float maxSpeed;
    public Vector3 direction;
    
}
