using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class TimeSystemGB : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        public async Task StartCountdown()
        {
            this.gameObject.GetComponent<Image>().fillAmount = 1f;
            while (this.gameObject.GetComponent<Image>().fillAmount > 0)
            {
                this.gameObject.GetComponent<Image>().fillAmount -= 0.05f;
                await Task.Delay(100);
            }
        }
    }
}