using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuicklyImage
{
    public class SelectButtonEventsQI : MonoBehaviour
    {
        public void OnClickSelect()
        {
            var director = GameObject.Find("GameDirector");
            director.GetComponent<CardSystemsQI>().CheckAnswer(this.gameObject.GetComponent<Image>().sprite);
        }

        public void SetSpriteToBlack()
        {
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponent<Image>().sprite = null;
            this.gameObject.GetComponent<Image>().color = Color.black;
        }

        public void SetSprite(Sprite sprite)
        {
            this.gameObject.GetComponent<Image>().color = Color.white;
            this.gameObject.GetComponent<Image>().sprite = sprite;
            this.gameObject.GetComponent<Button>().enabled = true;
        }
    }
}