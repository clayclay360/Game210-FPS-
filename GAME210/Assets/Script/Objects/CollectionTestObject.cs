using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTestObject : MonoBehaviour, ICollectable
{
    public string objName;

    public void Collect()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }
}
