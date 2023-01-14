using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアの管理
/// </summary>

namespace GetBlock
{
    public class StateTextSystemGB : MonoBehaviour
    {
        // 各プレイヤーのスコアの管理
        private int player1Point;
        private int player2Point;

        private void Awake()
        {
            // 初期化
            this.player1Point = 0;
            this.player2Point = 0;
        }

        // スコアを更新する
        public void UpdateText(GameObject[] collects)
        {
            // 一度0に初期化する
            this.player1Point = 0;
            this.player2Point = 0;

            // 状況オブジェクトのy位置から対応する変数にカウント
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

            // テキストに反映
            this.gameObject.transform.GetChild(1).GetComponent<Text>().text = this.player2Point.ToString();
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = this.player1Point.ToString();
        }
    }
}