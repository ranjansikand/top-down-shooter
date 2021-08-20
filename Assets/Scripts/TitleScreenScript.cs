using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScreenScript : MonoBehaviour
{
    public void PlayButtonClicked()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
