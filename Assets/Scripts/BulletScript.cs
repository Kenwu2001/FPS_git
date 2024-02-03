using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float FlyingSpeed = 80;
    public float LifeTime = 5;

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
        other.gameObject.SendMessage("Hit", damageValue);
        KillYourself();
    }
}