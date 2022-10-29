using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneNames;
using PublicUI;

namespace Home
{
    public class SelectButtonEventsHome : MonoBehaviour
    {
        private int buttonIndex;

        public async void OnClickSelect()
        {
            switch (buttonIndex)
            {
                case 0:
                    SceneData.sceneNames = SceneName.Neurasthenia;
                    break;
                case 1:
                    SceneData.sceneNames = SceneName.HitAndBlow;
                    break;
                default:
                    Debug.LogError("IllegalValue");
                    break;
            }

            var fade = GameObject.Find("FadeUI");
            fade.GetComponent<FadeSystems>().FadeOut();

            await Task.Delay(3000);

            SceneManager.LoadScene("DescriptionScene");
        }

        public void SetButtonIndex(int value)
        {
            this.buttonIndex = value;
        }
    }
}