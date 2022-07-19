using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI Variables")]
    public Button restartButton;
    public Text waveText;

    public void loadScene(int index)
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
