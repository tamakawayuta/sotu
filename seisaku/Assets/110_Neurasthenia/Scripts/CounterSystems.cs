using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    /// <summary>
    /// スコアを記録するテキスト
    /// </summary>
    
    [DisallowMultipleComponent]
    public sealed class CounterSystems : MonoBehaviour
    {
        //スコアを記録する
        private int answerAmount = 0;

        private void Awake()
        {
            //初期化
            this.gameObject.GetComponent<Text>().text = 0.ToString();
        }

        //スコアを加算してUIを加算する
        public void UpdateCount()
        {
            this.answerAmount++;
            this.gameObject.GetComponent<Text>().text = answerAmount.ToString();
        }

        //全てカードがめくられているかの判定
        public bool CheckCount()
        {
            return answerAmount % 5 == 0 && answerAmount != 0;
        }

        //スコアを取得する
        public int GetAnswerAmount()
        {
            return this.answerAmount;
        }
    }
}