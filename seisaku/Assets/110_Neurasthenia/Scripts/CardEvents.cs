using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class CardEvents : MonoBehaviour
    {
        [SerializeField]
        private GameObject director;

        private Color cardColor;

        public void OnClickCard()
        {
            this.gameObject.GetComponent<Image>().color = this.cardColor;
            director.GetComponent<GameMain>().SetSelectGameObject(this.gameObject);
            this.gameObject.GetComponent<Button>().enabled = false;
        }

        public void SetCardColor(Color color)
        {
            this.cardColor = color;
        }

        public Color GetCardColor()
        {
            return this.cardColor;
        }
    }
}