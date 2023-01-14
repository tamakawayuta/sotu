using UnityEngine;
using UnityEngine.UI;

// 必要
using PublicUI;

/// <summary>
/// ゲーム状況を示すオブジェクトの定義
/// </summary>

namespace GetBlock
{
    public class CollectSystemGB : MonoBehaviour
    {
        // 必要なものの取得
        [SerializeField]
        private GameObject clearUI;
        [SerializeField]
        private GameObject text;

        // 状況オブジェクトの管理
        private GameObject[] collectChildren;

        private void Awake()
        {
            // 状況オブジェクトの取得
            collectChildren = new GameObject[this.gameObject.transform.childCount];

            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                collectChildren[i] = this.gameObject.transform.GetChild(i).gameObject;
            }
        }

        // 状況オブジェクトの更新
        public void UpdateCollectState(Sprite sprite,bool isFirstPlayer)
        {
            // どのオブジェクトを更新するか管理
            var index = 0;

            // 受け取ったスプライトから更新するオブジェクトを決定する
            if (sprite == collectChildren[0].GetComponent<Image>().sprite)
            {
                index = 0;
            }
            else if (sprite == collectChildren[1].GetComponent<Image>().sprite)
            {
                index = 1;
            }
            else if (sprite == collectChildren[2].GetComponent<Image>().sprite)
            {
                index = 2;
            }
            else if (sprite == collectChildren[3].GetComponent<Image>().sprite)
            {
                index = 3;
            }
            else if (sprite == collectChildren[4].GetComponent<Image>().sprite)
            {
                index = 4;
            }

            // 更新するオブジェクトの位置を取得
            var x = collectChildren[index].transform.localPosition.x;
            var y = collectChildren[index].transform.localPosition.y;

            // 先手と後手で対応する分動かす
            if (isFirstPlayer)
            {
                y -= 40;
            }
            else
            {
                y += 40;
            }

            // 位置を更新する
            collectChildren[index].GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0f);

            // スコアを更新する
            this.text.GetComponent<StateTextSystemGB>().UpdateText(collectChildren);

            // 勝敗の判定
            if (collectChildren[index].GetComponent<RectTransform>().localPosition.y >= 160)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("プレイヤー2のかち");
            }
            else if (collectChildren[index].GetComponent<RectTransform>().localPosition.y <= -240)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("プレイヤー1のかち");
            }
        }

        // 勝敗判定
        // 駒が無くなったときに呼び出される
        public void Judge()
        {
            // スコアの管理
            var playerField = 0;
            var enemyField = 0;

            foreach (var field in collectChildren)
            {
                // 位置によりスコアを加算
                if (field.transform.localPosition.y < 0)
                {
                    playerField++;
                }
                else if (field.transform.localPosition.y > 0)
                {
                    enemyField++;
                }
            }

            // スコアが大きい方の勝ち
            if (playerField > enemyField)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("プレイヤー1のかち");
            }
            else if (playerField < enemyField)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("プレイヤー2のかち");
            }
            else
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("ひきわけ");
            }
        }
    }
}