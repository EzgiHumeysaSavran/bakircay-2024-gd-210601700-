using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelect : MonoBehaviour
{
    public void LoadFirstGameMode()
    {
        SceneManager.LoadScene("BallsScene");
    }

    public void LoadSecondGameMode()
    {
        SceneManager.LoadScene("FruitScene");
    }
}
