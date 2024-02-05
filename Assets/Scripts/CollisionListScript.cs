using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListScript : MonoBehaviour
{
    public List<Collider> CollisionObjects;

    public void OnTriggerEnter(Collider other)
    {
        CollisionObjects.Add(other);
        // Debug.Log("111111111111111111");
    }

    public void OnTriggerExit(Collider other)
    {
        CollisionObjects.Remove(other);
        // Debug.Log("2222222222222222222222222");
    }
}