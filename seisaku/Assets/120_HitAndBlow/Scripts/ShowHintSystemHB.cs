using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace HitAndBlow
{
    public class ShowHintSystemHB : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        public async void ShowText(string text)
        {
            this.gameObject.SetActive(true);
            this.gameObject.GetComponentInChildren<Text>().text = text;
            await Task.Delay(1500);
            this.gameObject.SetActive(false);
        }
    }
}