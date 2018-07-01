using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BasicEnemyStructure : BasicEntityStructure {
    public int damage;
    public float attackSpeed;
    public float attackForce;
    public float firstSeeingRange;
    public float seeingRange;
    public int chanceOfDroppingHeart;
    public int heartDropCount;
    public Transform heart;
    public bool seeingRight;


    public Vector3 attackedDirection;
    public BasicBulletStructure.BULLETTYPE attackedBulletType;

    private bool finishedAttack = true;
    private bool IsDroppedHeart;
    private bool playerSeen;

    protected Vector3 directionToPlayer;
    protected float BTWTimeAttack;
    protected Transform player;
    protected NavMeshAgent agent;
    protected bool move;

    public void MinusHealth(int h)
    {
        health -= h;
    }

    private IEnumerator AttackingIsFalseAndApplyingForce(float f, Animator animator)
    {
        // TODO: fix this nonsense
        yield return new WaitForSeconds(f);
        animator.SetBool("IsAttacking", false);
        Vector3 force = (player.position - transform.position).normalized;
        player.GetComponent<Rigidbody>().AddForce(new Vector3(force.x, 0, force.z) * attackForce * 10);
        player.GetComponent<Player>().MinusHealth(damage);
        finishedAttack = true;

    }

    public virtual void IsAttackable( Animator animator)
    {
        if (player != null)
        {
            Vector3 distance3D = player.position - transform.position;
            
            float distance = distance3D.magnitude;
            if (BTWTimeAttack >= attackSpeed)
            {
                if((distance3D.magnitude - 1 <= agent.stoppingDistance) && finishedAttack)
                {
                    finishedAttack = false;
                    BTWTimeAttack = 0;


                    animator.SetBool("IsAttacking", true);
                    StartCoroutine(AttackingIsFalseAndApplyingForce(1f, animator));
                }

            }
        }
    }

    public virtual void IsAttackable()
    {
        if (player != null)
        {
            Vector3 distance3D = player.position - transform.position;

            float distance = distance3D.magnitude;
            if (BTWTimeAttack >= attackSpeed)
            {
                if (distance3D.magnitude - 1 <= agent.stoppingDistance)
                {
                    BTWTimeAttack = 0;


                    Vector3 force = (player.position - transform.position).normalized;
                    player.GetComponent<Rigidbody>().AddForce(new Vector3(force.x, 0, force.z) * attackForce * 10);
                    player.GetComponent<Player>().MinusHealth(damage);
                    
                }

            }
        }
    }

    public virtual void Attack()
    {
        if(BTWTimeAttack >= attackSpeed)
        {

        }
    }

    public virtual void MoveToPlayer(float agentSpeed)
    {

        // || Move towards agent ||
        if (!playerSeen)
        {


            RaycastHit[] hitsInfo = Physics.RaycastAll(transform.position, directionToPlayer, firstSeeingRange);
            
            if (hitsInfo.Length == 0)
            {
                move = false;
                return;
            }
            for (int i = 0; i < hitsInfo.Length; i++)
            {
                if (hitsInfo[i].transform.tag != "Enemy")
                {
                    if (hitsInfo[i].transform.tag != "Player")
                    {
                        move = false;
                        break;
                    }
                    else
                    {
                        move = true;
                        agent.SetDestination(player.position);
                        playerSeen = true;
                        break;
                    }
                }
            }
        }
        if (playerSeen)
        {
            RaycastHit[] hitsInfo = Physics.RaycastAll(transform.position, directionToPlayer, seeingRange);
            if (hitsInfo.Length == 0)
            {
                move = false;
                playerSeen = false;
                return;
            }
            for (int i = hitsInfo.Length - 1; i >= 0; i--)
            {
                if (hitsInfo[i].transform.tag != "Enemy")
                {
                    if (hitsInfo[i].transform.tag != "Player")
                    {
                        move = false;
                        return;
                    }
                    else
                    {
                        move = true;
                        agent.SetDestination(player.position);
                        break;
                    }
                }
            }
        }
    }


    public virtual void MoveToPlayer(float agentSpeed, Animator anim)
    {
        // || Move towards agent ||
        if (!playerSeen)
        { 

            
            RaycastHit[] hitsInfo = Physics.RaycastAll(transform.position, directionToPlayer, firstSeeingRange);
            if (hitsInfo.Length == 0)
            {
                move = false;
                anim.SetBool("IsRunning", false);
                return;
            }
            for (int i = 0; i < hitsInfo.Length; i++)
            {
                if (hitsInfo[i].transform.tag != "Enemy")
                {
                    if (hitsInfo[i].transform.tag != "Player")
                    {
                        move = false;
                        anim.SetBool("IsRunning", false);
                        break;
                    }
                    else
                    {
                        move = true;
                        anim.SetBool("IsRunning", true);
                        agent.SetDestination(player.position);
                        playerSeen = true;
                        break;
                    }
                }
            }
        }
        if (playerSeen)
        {
            RaycastHit[] hitsInfo = Physics.RaycastAll(transform.position, directionToPlayer, seeingRange);
            if (hitsInfo.Length == 0)
            {
                move = false;
                anim.SetBool("IsRunning", false);
                playerSeen = false;
                return;
            }
            for (int i = hitsInfo.Length - 1; i >= 0; i--)
            {
                if (hitsInfo[i].transform.tag != "Enemy")
                {
                    if (hitsInfo[i].transform.tag != "Player")
                    {
                        move = false;
                        anim.SetBool("IsRunning", false);
                        return;
                    }
                    else
                    {
                        move = true;
                        anim.SetBool("IsRunning", true);
                        agent.SetDestination(player.position);
                        break;
                    }
                }
            }
        }


    }

    public virtual void IsCorrectDirection(Vector3 size)
    {
        
        float distancePlusMinus = directionToPlayer.x + directionToPlayer.z;
        if (!seeingRight)
        {
            if (distancePlusMinus > 0)
            {
                transform.localScale = new Vector3(-size.x, size.y, size.z);
            }
            if (distancePlusMinus <= 0)
            {
                transform.localScale = size;
            }
        }
        else
        {
            if (distancePlusMinus < 0)
            {
                transform.localScale = new Vector3(-size.x, size.y, size.z);
            }
            if (distancePlusMinus >= 0)
            {
                transform.localScale = size;
            }
        }
    }

    public virtual void Die(Rigidbody rb)
    {
        rb.isKinematic = true;
        agent.enabled = false;
        DropHeart();
        Destroy(gameObject);
        // TODO: Make the enemy drop hearts
    }

    public virtual void Die(Rigidbody rb, Animator anim)
    {
        rb.isKinematic = true;
        agent.enabled = false;
        anim.SetBool("IsDead", true);
        Destroy(gameObject, 1.5f);
        DropHeart();
    }



    private void DropHeart()
    {
        if (!IsDroppedHeart)
        {

            IsDroppedHeart = true;
            for (int i = 0; i < heartDropCount; i++)
            {
                int x = Random.Range(0, 100);
                if (x < chanceOfDroppingHeart)
                {
                    float offSet = Random.Range(-2, 2);
                    Instantiate(heart, transform.position + Vector3.back * 1  + Vector3.right * offSet, Quaternion.identity);
                    
                }
            }
        }

    }
    public virtual void Update()
    {
        BTWTimeAttack += Time.deltaTime;
        directionToPlayer = (player.position - transform.position).normalized;
    }

    public virtual void LateUpdate()
    {
        agent.enabled = move;
    }

    public virtual void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }



    private void OnDrawGizmosSelected()
    {
        
        if (!playerSeen)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * firstSeeingRange);
        }
        if (playerSeen)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * seeingRange);
        }
       
       

    }
}
