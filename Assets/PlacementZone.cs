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
            GetComponent<Renderer>().material.color = Color.black;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (placedObject == null && other.CompareTag("Draggable"))
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void PlaceObject(GameObject obj)
    {
        placedObject = obj;
        placedObject.transform.position = transform.position;
        placedObject.GetComponent<ObjectMover>().SetPlacementZone(this);
        placedObject.GetComponent<ObjectMover>().enabled = false;

        Rigidbody rb = placedObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void RemoveObject()
    {
        if (placedObject != null)
        {
            placedObject.GetComponent<ObjectMover>().enabled = true;

            Rigidbody rb = placedObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            placedObject = null;
        }
    }
}
