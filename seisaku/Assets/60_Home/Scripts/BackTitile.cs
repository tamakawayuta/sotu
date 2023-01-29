using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoundManager;
using PublicUI;


public class BackTitile : MonoBehaviour
{
    [SerializeField]
    private GameObject sound;
    public async void TitileBack()
    {
        sound.GetComponent<SoundSystems>().PlaySE(0);
        var fade = GameObject.Find("FadeUI");
        fade.GetComponent<FadeSystems>().FadeOut();
        await Task.Delay(3000);
        SceneManager.LoadScene("TitleScene");
    }
}
