using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementZone : MonoBehaviour
{
    private List<GameObject> placedObjects = new List<GameObject>(); // Yerleþtirilen objeleri tutar

    public Vector3 LeftOffset = new Vector3(-0.5f, 0f, 0f);
    public Vector3 RightOffset = new Vector3(0.5f, 0f, 0f);

    public void PlaceObject(GameObject obj)
    {
        if (placedObjects.Count >= 2) return; // Maksimum 2 obje alabilir

        // Fizik özelliklerini devre dýþý býrak
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Fizik etkilerini kapat
            rb.useGravity = false; // Yerçekimini devre dýþý býrak
            rb.velocity = Vector3.zero; // Hýzýný sýfýrla
            rb.angularVelocity = Vector3.zero; // Dönme hýzýný sýfýrla
        }

        // Objeyi Placement Zone içinde sabit bir pozisyona taþý
        if (placedObjects.Count == 0)
        {
            obj.transform.position = transform.position + LeftOffset; // Sol tarafa yerleþtir
        }
        else
        {
            obj.transform.position = transform.position + RightOffset; // Sað tarafa yerleþtir
        }

        obj.transform.rotation = Quaternion.identity; // Rotasyonu sýfýrla

        // Sürükleme yeteneðini devre dýþý býrak
        ObjectMover mover = obj.GetComponent<ObjectMover>();
        if (mover != null)
        {
            mover.enabled = false;
        }

        placedObjects.Add(obj); // Objeyi listeye ekle

        if (placedObjects.Count == 2) // Ýki obje yerleþtirildiðinde
        {
            CheckMatch();
        }
    }

    public void RemoveObject(GameObject obj)
    {
        if (placedObjects.Contains(obj))
        {
            // Fizik özelliklerini geri yükle
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Fizik etkilerini aç
                rb.useGravity = true; // Yerçekimini etkinleþtir
                rb.velocity = Vector3.zero; // Hýzý sýfýrla
                rb.angularVelocity = Vector3.zero; // Dönme hýzýný sýfýrla
            }

            // Sürükleme yeteneðini tekrar aç
            ObjectMover mover = obj.GetComponent<ObjectMover>();
            if (mover != null)
            {
                mover.enabled = true;
            }

            placedObjects.Remove(obj); // Objeyi listeden çýkar
        }
    }

    private void CheckMatch()
    {
        if (placedObjects.Count == 2)
        {
            // Ýki objeyi al
            GameObject obj1 = placedObjects[0];
            GameObject obj2 = placedObjects[1];

            // Objelerin ID'lerini kontrol et
            ObjectMover mover1 = obj1.GetComponent<ObjectMover>();
            ObjectMover mover2 = obj2.GetComponent<ObjectMover>();

            if (mover1 != null && mover2 != null && mover1.PairID == mover2.PairID)
            {
                // ID'ler eþleþiyorsa objeleri yok et
                Destroy(obj1);
                Destroy(obj2);

                placedObjects.Clear(); // Listeyi temizle

                // Puan ekle (GameManager üzerinden)
                GameManager.Instance.AddScore(10);

                Debug.Log("Match found! Objects destroyed.");
            }
            else
            {
                // ID'ler eþleþmiyorsa objeleri geri at
                Debug.Log("No match! Objects removed.");

                // Ýlk objeye kuvvet uygula
                Rigidbody rb1 = obj1.GetComponent<Rigidbody>();
                if (rb1 != null)
                {
                    rb1.isKinematic = false; // Fizik etkilerini aktif et
                    rb1.useGravity = true;   // Yerçekimini aktif et
                    rb1.velocity = Vector3.zero; // Hýz sýfýrla
                    rb1.angularVelocity = Vector3.zero; // Açýsal hýz sýfýrla

                    // Rastgele bir kuvvet uygula
                    Vector3 forceDirection1 = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
                    rb1.AddForce(forceDirection1 * 9f, ForceMode.Impulse); // Kuvvet uygula
                }

                // Ýkinci objeye kuvvet uygula
                Rigidbody rb2 = obj2.GetComponent<Rigidbody>();
                if (rb2 != null)
                {
                    rb2.isKinematic = false; // Fizik etkilerini aktif et
                    rb2.useGravity = true;   // Yerçekimini aktif et
                    rb2.velocity = Vector3.zero; // Hýz sýfýrla
                    rb2.angularVelocity = Vector3.zero; // Açýsal hýz sýfýrla

                    // Rastgele bir kuvvet uygula
                    Vector3 forceDirection2 = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
                    rb2.AddForce(forceDirection2 * 9f, ForceMode.Impulse); // Kuvvet uygula
                }

                RemoveObject(obj1);
                RemoveObject(obj2);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0) && other.CompareTag("Draggable"))
        {
            if (!placedObjects.Contains(other.gameObject))
            {
                PlaceObject(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            RemoveObject(other.gameObject);
        }
    }
}
