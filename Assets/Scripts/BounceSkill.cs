using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceSkill : MonoBehaviour
{
    public Button skillButton; // Zýplama skill butonu
    public float bounceForce = 5f; // Zýplama kuvveti (sýnýrlý)
    public float skillDuration = 3f; // Skill'in toplam süresi
    public float buttonCooldown = 5f; // Buton bekleme süresi
    private bool isCooldown = false; // Butonun aktiflik durumunu kontrol eder

    public void UseBounceSkill()
    {
        if (isCooldown) return; // Eðer bekleme süresindeyse skill çalýþmaz

        StartCoroutine(ApplyBounceSkill());
        StartCoroutine(ButtonCooldown());
    }

    private IEnumerator ApplyBounceSkill()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Draggable");
        float elapsedTime = 0f; // Geçen süreyi takip etmek için

        // Objeleri etkinleþtir ve zýplama iþlemini baþlat
        foreach (GameObject obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true; // Yerçekimini aktif et
            }
        }

        // 3 saniye boyunca zýplat
        while (elapsedTime < skillDuration)
        {
            foreach (GameObject obj in objects)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null && IsOnGround(rb))
                {
                    // Sadece zemine deðdiðinde zýplat
                    rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
                }
            }

            elapsedTime += 0.1f; // Zaman ilerlemesi (kontrol aralýðý)
            yield return new WaitForSeconds(0.1f); // Kýsa aralýklarla kontrol et
        }
    }

    private bool IsOnGround(Rigidbody rb)
    {
        // Zemine temas kontrolü (SphereCast kullanýmý)
        RaycastHit hit;
        return Physics.SphereCast(rb.position, 0.2f, Vector3.down, out hit, 0.5f);
    }

    private IEnumerator ButtonCooldown()
    {
        isCooldown = true; // Butonu bekleme süresine al
        skillButton.interactable = false; // Butonu devre dýþý býrak

        // Buton rengini deðiþtir (isteðe baðlý)
        ColorBlock colors = skillButton.colors;
        colors.disabledColor = new Color(0.5f, 0.5f, 1f, 0.5f); // Örnek transparan renk
        skillButton.colors = colors;

        yield return new WaitForSeconds(buttonCooldown); // Bekleme süresi

        skillButton.interactable = true; // Butonu tekrar aktif et
        isCooldown = false;

        // Buton rengini eski haline döndür (isteðe baðlý)
        colors.normalColor = new Color(1f, 1f, 1f, 1f); // Varsayýlan renk
        skillButton.colors = colors;
    }
}
