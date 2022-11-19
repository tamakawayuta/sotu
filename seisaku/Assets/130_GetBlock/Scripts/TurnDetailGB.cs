using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class TurnDetailGB : MonoBehaviour
    {
        private void Awake()
        {
            ShowPlayer1();
        }

        public void ShowPlayer1()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void ShowPlayer2()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}