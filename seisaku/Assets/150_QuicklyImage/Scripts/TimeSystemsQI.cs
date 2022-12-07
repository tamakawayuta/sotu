using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace QuicklyImage
{
    public class TimeSystemsQI : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        public async void StartCountdown(int delayTime)
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.GetComponent<Image>().fillAmount = 1f;

            while (this.gameObject.GetComponent<Image>().fillAmount > 0f)
            {
                this.gameObject.GetComponent<Image>().fillAmount -= 0.05f;

                await Task.Delay(delayTime);
            }

            if (this.gameObject.GetComponent<Image>().enabled)
            {
                Debug.Log("timeout");
            }
        }
    }
}