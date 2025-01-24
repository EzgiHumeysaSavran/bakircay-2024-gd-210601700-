using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityScript : MonoBehaviour
{
    public Button skillButton; // Skill butonu
    public float skillDuration = 3f; // Yerçekiminin devre dýþý kalma süresi
    public float cooldownDuration = 5f; // Buton bekleme süresi
    public float bounceForce = 5f; // Zýplama kuvveti

    public void UseBounceSkill()
    {
        // Sahnedeki tüm "Draggable" objeleri bul
        GameObject[] draggableObjects = GameObject.FindGameObjectsWithTag("Draggable");

        if (draggableObjects.Length > 0)
        {
            StartCoroutine(BounceObjects(draggableObjects));
            StartCoroutine(ButtonCooldown());
        }
        else
        {
            Debug.LogWarning("No objects with tag 'Draggable' found!");
        }
    }

    private IEnumerator BounceObjects(GameObject[] draggableObjects)
    {
        // Objelerin zýplamasýný saðla
        foreach (GameObject obj in draggableObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Mevcut hýzý sýfýrla
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse); // Yukarý kuvvet uygula
                rb.useGravity = false; // Yerçekimini devre dýþý býrak
            }
        }

        yield return new WaitForSeconds(skillDuration); // Yerçekimi devre dýþý süresi

        // Yerçekimini geri getir
        foreach (GameObject obj in draggableObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // Yerçekimini geri getir
            }
        }
    }

    private IEnumerator ButtonCooldown()
    {
        skillButton.interactable = false; // Butonu devre dýþý býrak
        yield return new WaitForSeconds(cooldownDuration); // Bekleme süresi
        skillButton.interactable = true; // Butonu tekrar etkinleþtir
    }
}
