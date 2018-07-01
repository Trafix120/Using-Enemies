using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEntityStructure : MonoBehaviour {

    public int health;

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Area Affector")
        {
            if(other.GetComponent<AreaAffector>().areaType == AreaAffector.AREATYPE.DamageToPlayer)
            {

                
                if(transform.tag == "Player")
                {
                    GetComponent<Player>().MinusHealth(other.GetComponent<AreaAffector>().damage);
                    Debug.Log("Damage is done by bomber");
                }
            }

            if (other.GetComponent<AreaAffector>().areaType == AreaAffector.AREATYPE.DamageToEnemies)
            {
                if (transform.tag == "Enemy")
                {
                    health -= other.GetComponent<AreaAffector>().damage;
                }
            }

            if (other.GetComponent<AreaAffector>().areaType == AreaAffector.AREATYPE.DamageToEntities)
            {


                if (transform.tag == "Player")
                {
                    GetComponent<Player>().MinusHealth(other.GetComponent<AreaAffector>().damage);
                }
                if (transform.tag == "Enemy")
                {
                    GetComponent<BasicEnemyStructure>().MinusHealth(other.GetComponent<AreaAffector>().damage);
                }
            }


        }

    }

}
