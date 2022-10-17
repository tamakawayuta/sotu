using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace PublicUI
{
    public class FadeSystems : MonoBehaviour
    {
        private float speed = 0.01f;

        private float alfa;
        private float red, green, blue;

        private void Awake()
        {
            red = this.gameObject.transform.GetChild(0).GetComponent<Image>().color.r;
            green = this.gameObject.transform.GetChild(0).GetComponent<Image>().color.g;
            blue = this.gameObject.transform.GetChild(0).GetComponent<Image>().color.b;
            alfa = this.gameObject.transform.GetChild(0).GetComponent<Image>().color.a;
            FadeIn();
        }
        
        private async void FadeIn()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            while (true)
            {
                await Task.Delay(20);
                this.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(red,green,blue,alfa);
                alfa -= speed;

                if (alfa < 0)
                {
                    this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
        }

        public async void FadeOut()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

            while (true)
            {
                await Task.Delay(20);
                this.gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(red, green, blue, alfa);
                alfa += speed;

                if (alfa >= 1)
                {
                    break;
                }
            }

            return;
        }
    }
}