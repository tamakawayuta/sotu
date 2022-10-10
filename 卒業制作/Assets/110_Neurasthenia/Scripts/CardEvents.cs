using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class CardEvents : MonoBehaviour
    {
        private Color cardColor;

        public void OnClickCard()
        {
            this.gameObject.GetComponent<Image>().color = this.cardColor;
        }

        public void setCardColor(Color color)
        {
            this.cardColor = color;
        }
    }
}