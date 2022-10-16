using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class CounterSystems : MonoBehaviour
    {
        private int answerAmount = 0;

        private void Awake()
        {
            this.gameObject.GetComponent<Text>().text = 0.ToString();
        }

        public void UpdateCount()
        {
            this.answerAmount++;
            this.gameObject.GetComponent<Text>().text = answerAmount.ToString();
        }

        public bool CheckCount()
        {
            return answerAmount % 5 == 0 && answerAmount != 0;
        }
    }
}