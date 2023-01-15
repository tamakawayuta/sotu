using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアを管理する
/// </summary>

namespace QuicklyImage
{
    public class ScoreSystemsQI : MonoBehaviour
    {
        // スコアを記録する
        private int score = 0;

        private void Awake()
        {
            // 初期化
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = "0";
        }

        // スコアを記録しテキストに示す
        public void AddScore(int addValue)
        {
            this.score += addValue;
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = this.score.ToString();
        }

        // スコアのゲッター
        public int GetScore()
        {
            return this.score;
        }
    }
}