using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 正解数をカウントする
/// </summary> 

namespace Neurasthenia
{
    public class CounterSystems : MonoBehaviour
    {
        // 正解数を管理する
        private int answerAmount = 0;

        private void Awake()
        {
            // 正解数をを0に初期化
            this.gameObject.GetComponent<Text>().text = 0.ToString();
        }

        // 正解数を更新する
        public void UpdateCount()
        {
            // 正解数を1増やす
            this.answerAmount++;

            // テキストを更新する
            this.gameObject.GetComponent<Text>().text = answerAmount.ToString();
        }

        // 正解数が5の倍数であるか判定して結果を返す
        public bool CheckCount()
        {
            return answerAmount % 5 == 0 && answerAmount != 0;
        }

        // 正解数のゲッター
        public int GetAnswerAmount()
        {
            return this.answerAmount;
        }
    }
}