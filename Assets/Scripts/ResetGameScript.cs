using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetGameScript : MonoBehaviour
{
    public void ResetGame()
    {
        // Sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Game has been reset!");
    }

}
