using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementZone : MonoBehaviour
{
    private GameObject placedObject;

    void OnTriggerEnter(Collider other)
    {
        if (placedObject == null && other.CompareTag("Draggable"))
        {
            placedObject = other.gameObject;
            placedObject.transform.position = transform.position;
            placedObject.GetComponent<ObjectMover>().enabled = false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
