using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : BasicEntityStructure {

    public int startingHealth = 3; 

    // Movements
    private float moveSpeed = 10f;
    private Vector3 input;

    // Shooting
    private float bulletSpeed = 700f;
    private float BTWshootingTime;

    // Hearts
    private int tillHeartMax;

    // Holding shot 
    public float holdingShotTime;
    private Transform currentParticle;

    // Keys and Doors
    public int numKeys;

    // Declaration of Objects
    private Rigidbody rb;
    public Camera cam;
    public Transform projectileFol;
    public Transform holdingShotParticle;
    public Transform particleFol;
    public Sprite transparentSprite;
    public Transform[] hearts;
    public Transform[] bullets;
    public float[] fireRate;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //tillHeartMax = hearts.Capacity - health;
        tillHeartMax = hearts.Length;
        AddHealth(startingHealth);
	}
	
	// Update is called once per frame
	void Update () {

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        input = ((Vector3.forward * v) + (Vector3.right * h));
        //tillHeartMax = hearts.Capacity - health; TEMP not needed
        Shoot();
        IsDead();
    }

    private void FixedUpdate()
    {
        Move();
        BTWshootingTime += Time.deltaTime;
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Debug.Log("trigger enter");
        if(other.gameObject.tag == "Item")
        {
            // Picking Up Health
            if(other.GetComponent<BasicItemStruct>().itemType == BasicItemStruct.ITEMTYPE.Heart)
            {
                Debug.Log("Added health");
                Destroy(other.gameObject);  // TODO: when it is close enough let the hearts be attracted to the player
                AddHealth(1);
            }
            // Picking Up Keys
            if(other.GetComponent<BasicItemStruct>().itemType == BasicItemStruct.ITEMTYPE.Key)
            {
                Debug.Log("Attained KEY");
                Destroy(other.gameObject); // TODO: Maybe add a anim
                AddKey();
            }
        }
    }

    private void AddKey()
    {
        numKeys++;
    }

    public void AddHealth(int h)
    {
       
        for (int i = 0; i < h; i++)
        {
            // TODO: fix if max heart is too much
            if(health + i + 1> hearts.Length)
            {
                health += i;
                return;
                
            }
            Transform heart = hearts[health + i];
            
            
            heart.GetComponentInChildren<Animator>().SetBool("IsAlive", true);
            tillHeartMax--;
        }
        health += h;
    }

    public void MinusHealth(int h)
    {
        for (int i = 0; i < h; i++)
        {
            if(hearts.Length - tillHeartMax <= 0)
            {
                health = 0;
                return;
            }
            hearts[hearts.Length - tillHeartMax - 1].GetComponentInChildren<Animator>().SetBool("IsAlive", false);
            health -= 1;
            tillHeartMax++;
        }
       
        
    }




    private void IsDead()
    {
        if (health <= 0)
        {
            Debug.Log("Player is dead");
        }
    }
    

    private void Shoot()
    {
        
        
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E)) && BTWshootingTime >= fireRate[0])
        {
            BTWshootingTime = 0;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayinfo;
            if (Physics.Raycast(ray, out rayinfo))
            {
                // Ray calculation
                Vector3 target = rayinfo.point;
                target = new Vector3(target.x, transform.position.y, target.z);
                Vector3 distance3D = target - transform.position;
                Vector3 direction = Vector3.Normalize(distance3D);

                // Quaternion calculation
                Quaternion quar = Quaternion.FromToRotation(Vector3.right, direction);

                // Temporary position fix
                Vector3 offSet = direction * 2;

                Transform bullet = Instantiate(bullets[0], offSet + transform.position + Vector3.up, quar, particleFol);
                Vector3 force = (direction /** bulletSpeed*/);
                bullet.GetComponent<Rigidbody>().AddForce(force);
            }
        }
        BigFireBallCheck();
        
    }

    private void BigFireBallCheck()
    {
        if (Input.GetButton("Fire2"))
        {
            holdingShotTime += Time.deltaTime;
            
        }
        if (Input.GetButtonDown("Fire2"))
        {
            currentParticle = Instantiate(holdingShotParticle, transform.position + Vector3.up, Quaternion.identity, projectileFol);
        }
        
        if (!Input.GetButton("Fire2") && holdingShotTime >= 3)
        {
            holdingShotTime = 0;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayinfo;
            if (Physics.Raycast(ray, out rayinfo))
            {
                // Ray calculation
                Vector3 target = rayinfo.point;
                target = new Vector3(target.x, transform.position.y, target.z);
                Vector3 distance3D = target - transform.position;
                Vector3 direction = Vector3.Normalize(distance3D);

                // Quaternion calculation
                Quaternion quar = Quaternion.FromToRotation(Vector3.right, direction);

                // Temporary position fix
                Vector3 offSet = direction * 2;

                Transform bullet = Instantiate(bullets[1], offSet + transform.position + Vector3.up, quar, projectileFol);
                bullet.GetComponent<BasicBulletStructure>().direction = direction.normalized;
                Vector3 force = (direction /** bulletSpeed*/);
                
                bullet.GetComponent<Rigidbody>().AddForce(force);
            }
        }
        if (!Input.GetButton("Fire2"))
        {
            holdingShotTime = 0;
            if (currentParticle != null)
            {
                Destroy(currentParticle.gameObject);
            }
        }
    }

    private void Move()
    {

        Vector3 force = input * moveSpeed;
        transform.position += force * Time.deltaTime;
    }

}
