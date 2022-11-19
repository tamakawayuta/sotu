using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class StateTextSystemGB : MonoBehaviour
    {
        private int player1Point;
        private int player2Point;

        private void Awake()
        {
            this.player1Point = 0;
            this.player2Point = 0;
        }

        public void UpdateText(GameObject[] collects)
        {
            this.player1Point = 0;
            this.player2Point = 0;

            foreach (var child in collects)
            {
                if (child.GetComponent<RectTransform>().localPosition.y >= 0)
                {
                    player2Point++;
                }
                else if (child.GetComponent<RectTransform>().localPosition.y <= -80)
                {
                    player1Point++;
                }
            }

            this.gameObject.transform.GetChild(1).GetComponent<Text>().text = this.player2Point.ToString();
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = this.player1Point.ToString();
        }
    }
}