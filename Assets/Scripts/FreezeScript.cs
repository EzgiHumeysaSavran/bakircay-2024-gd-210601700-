using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeScript : MonoBehaviour
{
    public Button skillButton; // Skill 1 butonu
    public float freezeDuration = 3f; // Donma süresi
    public float buttonCooldown = 5f; // Buton bekleme süresi
    private bool isCooldown = false;

    public void UseFreezeSkill()
    {
        if (isCooldown) return; // Eðer bekleme süresindeyse skill çalýþmaz

        StartCoroutine(FreezeObjects());
        StartCoroutine(ButtonCooldown());
    }

    private IEnumerator FreezeObjects()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Draggable");
        List<ObjectMover> movers = new List<ObjectMover>();

        // Objeleri dondur ve animasyonu baþlat
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
                mover.enabled = false; // ObjectMover scriptini devre dýþý býrak
            }

            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("IsFrozen", true); // Donma animasyonunu baþlat
            }
        }

        // Donma süresi (örneðin 3 saniye)
        yield return new WaitForSeconds(3f);

        // Objeleri çöz ve animasyonu durdur
        foreach (GameObject obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Objeyi çöz
            }

            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("IsFrozen", false); // Donma animasyonunu durdur
            }

        }
        // ObjectMover scriptlerini yeniden etkinleþtir
        foreach (ObjectMover mover in movers)
        {
            mover.enabled = true;
        }
    }

    private IEnumerator ButtonCooldown()
    {
        isCooldown = true; // Butonu bekleme süresine al
        skillButton.interactable = false; // Butonu devre dýþý býrak

        ColorBlock colors = skillButton.colors;
        colors.disabledColor = new Color(0f, 1f, 0.9f, 0.1f); // RGB(0, 255, 230), %40 opaklýk
        colors.normalColor = new Color(0f, 1f, 0.9f, 1f); // RGB(0, 255, 230), tam opak
        skillButton.colors = colors;

        Debug.Log("Skill button disabled.");
        yield return new WaitForSeconds(buttonCooldown);

        skillButton.interactable = true;

        colors.normalColor = new Color(0f, 1f, 0.9f, 1f); // RGB(0, 255, 230), tam opak
        colors.disabledColor = new Color(0f, 1f, 0.9f, 0.4f); // Ayný transparan renk
        skillButton.colors = colors;

        isCooldown = false;

        Debug.Log("Skill button enabled.");
    }
}