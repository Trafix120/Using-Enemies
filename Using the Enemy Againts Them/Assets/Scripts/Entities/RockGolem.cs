using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RockGolem : BasicEnemyStructure {
    private bool attackedByFireBall;
    private Rigidbody rb;
    public float attackedByBigFireBallForce;
    private float BTWTimeAttackedFireBall;
    private float agentSpeed;
    private bool navmeshAttackFix = true;
    private Vector3 size;
    private Animator animator;
    private Vector3 knockBackTarget;
    
    // Use this for initialization
    public override void Start () {
        base.Start();
        
        attackSpeed = GetComponent<BasicEnemyStructure>().attackSpeed;
        rb = GetComponent<Rigidbody>();
        agentSpeed = agent.speed;
        size = transform.localScale;
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per 
    public override void Update()
    {
        base.Update();

        IsAttackedFireBall();

        IsAttackable(animator);



    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die(rb, animator);
        }
        else if(!attackedByFireBall)
        {
            MoveToPlayer(agentSpeed, animator);

            IsCorrectDirection(size);
        }
    }

    private void IsAttackedFireBall()
    {

        if(attackedBulletType == BasicBulletStructure.BULLETTYPE.BigFireBall)
        {
            knockBackTarget = (attackedDirection.normalized * 10) + transform.position;
            attackedByFireBall = true;

        }
        if (attackedByFireBall)
        {
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
        if(collision.gameObject.tag == "Enemy" && attackedByFireBall)
        {
            collision.gameObject.GetComponent<BasicEnemyStructure>().health -= 10;
        }

        if(collision.gameObject.tag != "Enemy" && attackedByFireBall)
        {
            attackedByFireBall = false;
        }
    }


}
