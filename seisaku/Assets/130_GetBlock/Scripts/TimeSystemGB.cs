using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace GetBlock
{
    public class TimeSystemGB : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.GetComponent<Image>().fillAmount = 0.0f;
        }

        public async void StartTimeCount()
        {
            this.gameObject.GetComponent<Image>().fillAmount = 1.0f;
            while (this.gameObject.GetComponent<Image>().fillAmount > 0f)
            {
                this.gameObject.GetComponent<Image>().fillAmount -= 0.07f;
                await Task.Delay(150);
            }
        }
    }
}