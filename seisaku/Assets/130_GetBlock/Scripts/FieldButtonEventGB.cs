using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class FieldButtonEventGB : MonoBehaviour
    {
        public static Sprite selectedSprite;

        private void Awake()
        {
            selectedSprite = null;
        }

        public void OnClickField(int index)
        {
            if (selectedSprite != null &&
                selectedSprite != this.gameObject.GetComponent<Image>().sprite)
            {
                return;
            }

            this.gameObject.GetComponent<Image>().color = Color.black;
            this.gameObject.GetComponent<Button>().enabled = false;
            selectedSprite = this.gameObject.GetComponent<Image>().sprite;
            GameObject.Find("GameDirector").GetComponent<GameMainGB>().UpdateGameState(index,this.gameObject.GetComponent<Image>().sprite);
        }
    }
}