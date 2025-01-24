using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeScript : MonoBehaviour
{
    public Button skillButton; // Skill 1 butonu
    public float freezeDuration = 3f; // Donma s�resi
    public float buttonCooldown = 5f; // Buton bekleme s�resi
    private bool isCooldown = false;

    public void UseFreezeSkill()
    {
        if (isCooldown) return; // E�er bekleme s�resindeyse skill �al��maz

        StartCoroutine(FreezeObjects());
        StartCoroutine(ButtonCooldown());
    }

    private IEnumerator FreezeObjects()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Draggable");
        List<ObjectMover> movers = new List<ObjectMover>();

        // Objeleri dondur ve animasyonu ba�lat
        foreach (GameObject obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Objeyi dondur
            }

            ObjectMover mover = obj.GetComponent<ObjectMover>();
            if (mover != null)
            {
                movers.Add(mover);
                mover.enabled = false; // ObjectMover scriptini devre d��� b�rak
            }

            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("IsFrozen", true); // Donma animasyonunu ba�lat
            }
        }

        // Donma s�resi (�rne�in 3 saniye)
        yield return new WaitForSeconds(3f);

        // Objeleri ��z ve animasyonu durdur
        foreach (GameObject obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Objeyi ��z
            }

            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("IsFrozen", false); // Donma animasyonunu durdur
            }

        }
        // ObjectMover scriptlerini yeniden etkinle�tir
        foreach (ObjectMover mover in movers)
        {
            mover.enabled = true;
        }
    }

    private IEnumerator ButtonCooldown()
    {
        isCooldown = true; // Butonu bekleme s�resine al
        skillButton.interactable = false; // Butonu devre d��� b�rak

        ColorBlock colors = skillButton.colors;
        colors.disabledColor = new Color(0f, 1f, 0.9f, 0.1f); // RGB(0, 255, 230), %40 opakl�k
        colors.normalColor = new Color(0f, 1f, 0.9f, 1f); // RGB(0, 255, 230), tam opak
        skillButton.colors = colors;

        Debug.Log("Skill button disabled.");
        yield return new WaitForSeconds(buttonCooldown);

        skillButton.interactable = true;

        colors.normalColor = new Color(0f, 1f, 0.9f, 1f); // RGB(0, 255, 230), tam opak
        colors.disabledColor = new Color(0f, 1f, 0.9f, 0.4f); // Ayn� transparan renk
        skillButton.colors = colors;

        isCooldown = false;

        Debug.Log("Skill button enabled.");
    }
}