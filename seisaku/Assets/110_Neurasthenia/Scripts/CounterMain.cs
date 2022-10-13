using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class CounterMain : MonoBehaviour
    {
        private int answerAmount = 0;

        private void Awake()
        {
            this.gameObject.GetComponent<Text>().text = 0.ToString();
        }

        public void AddAnswers()
        {
            answerAmount++;
            this.gameObject.GetComponent<Text>().text = answerAmount.ToString();
        }

        public int GetAnswers()
        {
            return this.answerAmount;
        }
    }
}