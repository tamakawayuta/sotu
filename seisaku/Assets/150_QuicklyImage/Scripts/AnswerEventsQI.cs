using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuicklyImage
{
    public class AnswerEventsQI : MonoBehaviour
    {
        public void SetAnswerSprite(Sprite sprite)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.GetComponent<Image>().sprite = sprite;
        }

        public void SetSpriteBlack()
        {
            this.gameObject.GetComponent<Image>().sprite = null;
            this.gameObject.GetComponent<Image>().color = Color.black;
        }
    }
}