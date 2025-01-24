using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityScript : MonoBehaviour
{
    public Button skillButton; // Skill butonu
    public float skillDuration = 3f; // Yer�ekiminin devre d��� kalma s�resi
    public float cooldownDuration = 5f; // Buton bekleme s�resi
    public float bounceForce = 5f; // Z�plama kuvveti

    public void UseBounceSkill()
    {
        // Sahnedeki t�m "Draggable" objeleri bul
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
        // Objelerin z�plamas�n� sa�la
        foreach (GameObject obj in draggableObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Mevcut h�z� s�f�rla
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse); // Yukar� kuvvet uygula
                rb.useGravity = false; // Yer�ekimini devre d��� b�rak
            }
        }

        yield return new WaitForSeconds(skillDuration); // Yer�ekimi devre d��� s�resi

        // Yer�ekimini geri getir
        foreach (GameObject obj in draggableObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // Yer�ekimini geri getir
            }
        }
    }

    private IEnumerator ButtonCooldown()
    {
        skillButton.interactable = false; // Butonu devre d��� b�rak
        yield return new WaitForSeconds(cooldownDuration); // Bekleme s�resi
        skillButton.interactable = true; // Butonu tekrar etkinle�tir
    }
}
