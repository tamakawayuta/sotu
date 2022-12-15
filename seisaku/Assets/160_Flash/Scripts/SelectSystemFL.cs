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
            this.buttons = new GameObject[this.gameObject.transform.childCount];

            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                this.buttons[i] = this.gameObject.transform.GetChild(i).gameObject;
            }
        }

        public void SetButtonSprites(Sprite[] sprites)
        {
            this.gameObject.SetActive(true);

            foreach (var button in buttons)
            {
                button.GetComponent<Button>().enabled = true;
            }

            for (var i = 0; i < 8; i++)
            {
                this.buttons[i].GetComponent<Image>().sprite = sprites[i];
            }
        }
    }
}