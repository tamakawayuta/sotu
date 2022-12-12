using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flash
{
    public class SelectSystemFL : MonoBehaviour
    {
        private GameObject[] buttons;

        private void Awake()
        {
            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                this.buttons[i] = this.gameObject.transform.GetChild(i).gameObject;
            }
        }

        public void SetButtonSprites(Sprite[] sprites)
        {
            for (var i = 0; i < 8; i++)
            {
                this.buttons[i].GetComponent<Image>().sprite = sprites[i];
            }
        }
    }
}