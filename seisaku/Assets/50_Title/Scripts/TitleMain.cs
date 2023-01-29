using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using SoundManager;

namespace Title
{
    public class TitleMain : MonoBehaviour
    {
        [SerializeField]
        private GameObject fade;
        [SerializeField]
        private GameObject sound;

        private float red,green,blue,alfa;

        private void Awake()
        {
            red = fade.GetComponent<Image>().color.r;
            green = fade.GetComponent<Image>().color.g;
            blue = fade.GetComponent<Image>().color.b;
            alfa = 0f;

            fade.GetComponent<Image>().color = new Color(red,green,blue,alfa);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) ||
                Input.GetMouseButtonDown(1))
            {
                LoadHome();
            }
        }

        private async void LoadHome()
        {
            sound.GetComponent<SoundSystems>().PlaySE(0);
            while (alfa < 1.0f)
            {
                fade.GetComponent<Image>().color = new Color(red,green,blue,alfa);
                alfa += 0.05f;
                await Task.Delay(50);
            }

            await Task.Delay(100);
            SceneManager.LoadScene("HomeScene");
        }
    }
}