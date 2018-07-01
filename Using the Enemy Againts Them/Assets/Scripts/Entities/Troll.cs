using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troll : BasicEnemyStructure {

    private float agentSpeed;
    private Rigidbody rb;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        agentSpeed = agent.speed;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        

        IsAttackable();


    }


    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Die(rb);
        }
        else
        {
            MoveToPlayer(agentSpeed);
            // TODO: add direction when there is animation
        }
    }


}
