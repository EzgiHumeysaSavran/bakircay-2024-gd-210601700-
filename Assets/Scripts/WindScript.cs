using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindScript : MonoBehaviour
{
    public Button windButton; // Rüzgar skill butonu
    public float windForce = 10f; // Uygulanacak rüzgar kuvveti
    public float buttonCooldown = 5f; // Buton bekleme süresi
    private bool isCooldown = false;
    public AudioSource WindSound;

    public void UseWindSkill()
    {
        if (WindSound != null)
        {
            WindSound.Play();
        }

        if (isCooldown) return; // Bekleme süresindeyse skill çalýþmaz

        StartCoroutine(ApplyWindEffect());
        StartCoroutine(ButtonCooldown());
    }

    private IEnumerator ApplyWindEffect()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Draggable");

        foreach (GameObject obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Rastgele bir yön oluþtur
                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f), // X ekseni
                    Random.Range(0.2f, 0.5f), // Y ekseni (biraz yukarýya doðru)
                    Random.Range(-1f, 1f)  // Z ekseni
                ).normalized;

                // Kuvvet uygula
                rb.AddForce(randomDirection * windForce, ForceMode.Impulse);
            }
        }

        yield return null; // Animasyon veya ek süre gerekirse burada bekleyebilirsiniz
    }

    private IEnumerator ButtonCooldown()
    {
        isCooldown = true; // Bekleme süresi aktif
        windButton.interactable = false; // Butonu devre dýþý býrak

        // Buton rengini transparan yap
        ColorBlock colors = windButton.colors;
        colors.disabledColor = new Color(1f, 0.5f, 0f, 0.4f); // RGB(255, 128, 0), %40 opaklýk
        colors.normalColor = new Color(1f, 0.5f, 0f, 1f); // Normal turuncu renk
        windButton.colors = colors;

        yield return new WaitForSeconds(buttonCooldown);

        windButton.interactable = true; // Butonu yeniden etkinleþtir

        // Buton rengini eski haline döndür
        colors.normalColor = new Color(1f, 0.5f, 0f, 1f);
        colors.disabledColor = new Color(1f, 0.5f, 0f, 0.4f);
        windButton.colors = colors;

        isCooldown = false;
    }
}
