using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float FlyingSpeed;
    public float LifeTime;
    public GameObject explosion;

    public void InitAndShoot(Vector3 Direction)
    {
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();

        rigidbody.velocity = Direction * FlyingSpeed;

        Invoke("KillYourself", LifeTime);
    }

    public void KillYourself()
    {
        GameObject.Destroy(this.gameObject);
    }

    public float damageValue = 15;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Troll"){
            other.gameObject.SendMessage("Hit", damageValue);
        }
        Debug.Log("other: " + other);
        explosion.gameObject.transform.parent = null;
        explosion.gameObject.SetActive(true);

        KillYourself();
    }
}