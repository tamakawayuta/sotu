using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundManager;

public class BackTitile : MonoBehaviour
{
    [SerializeField]
    private GameObject sound;
    public void TitileBack()
    {
        sound.GetComponent<SoundSystems>().PlaySE(0);
        SceneManager.LoadScene("TitleScene");
    }
}
