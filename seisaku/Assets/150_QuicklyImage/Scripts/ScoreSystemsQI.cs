using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuicklyImage
{
    public class ScoreSystemsQI : MonoBehaviour
    {
        private int score = 0;

        private void Awake()
        {
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = "0";
        }

        public void AddScore(int addValue)
        {
            this.score += addValue;
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = this.score.ToString();
        }
    }
}