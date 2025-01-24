using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private Rigidbody rb;
    private PlacementZone currentPlacementZone;

    public int PairID; // Bu obje �iftine �zg� e�le�me ID'si
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        ResetPhysics();
    }

    private void OnMouseDown()
    {
        if (currentPlacementZone != null)
        {
            // Placement Zone'dan objeyi kald�r
            currentPlacementZone.RemoveObject(gameObject);
            currentPlacementZone = null;
        }

        rb.isKinematic = true; // S�r�kleme s�ras�nda fizik kapal�
        offset = transform.position - GetMouseWorldPosition();
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

    public void ResetPhysics()
    {
        if (rb != null)
        {
            rb.isKinematic = false; // Fizik etkilerini a�
            rb.useGravity = true; // Yer�ekimini etkinle�tir
            rb.velocity = Vector3.zero; // Hareket h�z�n� s�f�rla
            rb.angularVelocity = Vector3.zero; // D�nme h�z�n� s�f�rla
        }
    }
}
