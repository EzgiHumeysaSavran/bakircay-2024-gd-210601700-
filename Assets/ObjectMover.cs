using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private Rigidbody rb;
    private PlacementZone currentPlacementZone;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    private void OnMouseDown()
    {
        if (currentPlacementZone != null)
        {
            currentPlacementZone.RemoveObject();
            currentPlacementZone = null;
        }

        rb.isKinematic = true;
        offset = transform.position - GetMouseWorldPosition();
        transform.position += new Vector3(0, 0.25f, 0);
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = GetMouseWorldPosition() + offset;
        newPosition.y = transform.position.y;
        rb.MovePosition(newPosition);
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    public void SetPlacementZone(PlacementZone zone)
    {
        currentPlacementZone = zone;
    }
}
