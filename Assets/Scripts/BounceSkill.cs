using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceSkill : MonoBehaviour
{
    public Button skillButton; // Z�plama skill butonu
    public float bounceForce = 5f; // Z�plama kuvveti (s�n�rl�)
    public float skillDuration = 3f; // Skill'in toplam s�resi
    public float buttonCooldown = 5f; // Buton bekleme s�resi
    private bool isCooldown = false; // Butonun aktiflik durumunu kontrol eder

    public void UseBounceSkill()
    {
        if (isCooldown) return; // E�er bekleme s�resindeyse skill �al��maz

        StartCoroutine(ApplyBounceSkill());
        StartCoroutine(ButtonCooldown());
    }

    private IEnumerator ApplyBounceSkill()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Draggable");
        float elapsedTime = 0f; // Ge�en s�reyi takip etmek i�in

        // Objeleri etkinle�tir ve z�plama i�lemini ba�lat
        foreach (GameObject obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // Yer�ekimini aktif et
            }
        }

        // 3 saniye boyunca z�plat
        while (elapsedTime < skillDuration)
        {
            foreach (GameObject obj in objects)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null && IsOnGround(rb))
                {
                    // Sadece zemine de�di�inde z�plat
                    rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
                }
            }

            elapsedTime += 0.1f; // Zaman ilerlemesi (kontrol aral���)
            yield return new WaitForSeconds(0.1f); // K�sa aral�klarla kontrol et
        }
    }

    private bool IsOnGround(Rigidbody rb)
    {
        // Zemine temas kontrol� (SphereCast kullan�m�)
        RaycastHit hit;
        return Physics.SphereCast(rb.position, 0.2f, Vector3.down, out hit, 0.5f);
    }

    private IEnumerator ButtonCooldown()
    {
        isCooldown = true; // Butonu bekleme s�resine al
        skillButton.interactable = false; // Butonu devre d��� b�rak

        // Buton rengini de�i�tir (iste�e ba�l�)
        ColorBlock colors = skillButton.colors;
        colors.disabledColor = new Color(0.5f, 0.5f, 1f, 0.5f); // �rnek transparan renk
        skillButton.colors = colors;

        yield return new WaitForSeconds(buttonCooldown); // Bekleme s�resi

        skillButton.interactable = true; // Butonu tekrar aktif et
        isCooldown = false;

        // Buton rengini eski haline d�nd�r (iste�e ba�l�)
        colors.normalColor = new Color(1f, 1f, 1f, 1f); // Varsay�lan renk
        skillButton.colors = colors;
    }
}
