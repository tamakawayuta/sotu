using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class ShowTurnUIEvents : MonoBehaviour
    {
        /*[SerializeField]
        private Sprite firstPlayerSprite;
        [SerializeField]
        private Sprite secondPlayerSprite;*/

        private bool isFirstPlayerTurn = true;

        private void Awake()
        {
            SetTurnSprite();
        }

        public void SetTurnSprite()
        {
            if (this.isFirstPlayerTurn)
            {
                this.gameObject.GetComponent<Image>().color = Color.blue;
                //this.gameObject.GetComponent<Image>().sprite = this.firstPlayerSprite;
            }
            else
            {
                this.gameObject.GetComponent<Image>().color = Color.red;
                //this.gameObject.GetComponent<Image>().sprite = this.secondPlayerSprite;
            }

            this.isFirstPlayerTurn = !isFirstPlayerTurn;
        }
    }
}