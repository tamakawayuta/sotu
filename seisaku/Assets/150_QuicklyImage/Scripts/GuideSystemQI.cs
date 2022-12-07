using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuicklyImage
{
    public class GuideSystemQI : MonoBehaviour
    {
        private void Awake()
        {
            SetText("ÇπÅ[ÇÃ..!");
        }

        public void SetText(string text)
        {
            this.gameObject.GetComponentInChildren<Text>().text = text;
        }
    }
}