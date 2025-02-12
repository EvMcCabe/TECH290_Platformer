using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button introButton;

    // This function is used to load a new scene
    public void LoadScene()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    //This funtion sets the button as active or deactive (Button disappears when clicked)
    public void SetIntroActive()
    {
        introButton.gameObject.SetActive(false);
    }
}