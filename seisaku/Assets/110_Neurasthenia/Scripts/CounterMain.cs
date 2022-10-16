using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class CounterMain : MonoBehaviour
    {
        private int answerAmount = 0;
        private bool isClear = false;

        private void Awake()
        {
            this.gameObject.GetComponent<Text>().text = 0.ToString();
        }

        public void AddAnswers()
        {
            answerAmount++;
            this.gameObject.GetComponent<Text>().text = answerAmount.ToString();

            if (answerAmount % 5 == 0 &&
                answerAmount != 0)
            {
                isClear = true;
            }
        }

        public int GetAnswers()
        {
            return this.answerAmount;
        }

        public void ResetIsClear()
        {
            this.isClear = false;
        }

        public bool GetIsClear()
        {
            return this.isClear;
        }
    }
}