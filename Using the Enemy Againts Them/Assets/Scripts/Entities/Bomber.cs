using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bomber : BasicEnemyStructure {

    // Being Hit by Big Fireball
    private bool attackedByFireBall;
    private Vector3 knockBackTarget;
    private bool OUCreatedDamageAffector;

    // Exploding 
    private bool OUExplode;
    

    // MovingToPlayer
    private float agentSpeed;
    private Vector3 size;

    // Declaration of Objects
    private Rigidbody rb;
    private Animator animator;
    public Transform AADamageToPlayer;
    public Transform AADamageToEnemies;
    public Transform parDustExplode;


	// Use this for initialization
	public override void Start () {
        base.Start();
        agentSpeed = agent.speed;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        size = transform.localScale;
	}
	
	// Update is called once per frame
	public override void Update() {
        base.Update();
        IsAttackable();
        IsAttackedFireBall();
	}

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die(rb);
        }
        else
        {
            MoveToPlayer(agentSpeed, animator);
            IsCorrectDirection(size);
        }
    }

    private new void IsAttackable()
    {
        if (player != null)
        {
            Vector3 distance3D = player.position - transform.position;

            float distance = distance3D.magnitude;
            if (distance3D.magnitude - 1 <= agent.stoppingDistance && !OUExplode)
            {
                Debug.Log("Instatiating area affector");
                // TODO add folder
                Instantiate(AADamageToPlayer, transform.position, Quaternion.identity, GM.GetAAFol());
                Instantiate(parDustExplode, transform.position + Vector3.up * 3, Quaternion.identity, GM.GetParFol());
                Destroy(gameObject);
                OUExplode = true;
            }

            
        }
    }

    private void IsAttackedFireBall()
    {

        if (attackedBulletType == BasicBulletStructure.BULLETTYPE.BigFireBall)
        {
            knockBackTarget = (attackedDirection.normalized * 10) + transform.position;
            attackedByFireBall = true;

        }
        if (attackedByFireBall)
        {
            // TODO fix rock golem box collider glitching
            transform.position = Vector3.Lerp(transform.position, knockBackTarget, 0.1f);
            attackedBulletType = BasicBulletStructure.BULLETTYPE.Null;
            float diff = Vector3.Distance(knockBackTarget, transform.position);
            if (diff <= 2)
            {
                attackedByFireBall = false;

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(attackedByFireBall && collision.gameObject.tag == "Enemy" && !OUCreatedDamageAffector)
        {
            Debug.Log("Instatiating area affector damage");
            Instantiate(AADamageToEnemies, transform.position, Quaternion.identity, GM.GetAAFol());
            Instantiate(parDustExplode, transform.position + Vector3.up * 3, Quaternion.identity, GM.GetParFol());
            OUCreatedDamageAffector = true;
        }
    }


}
