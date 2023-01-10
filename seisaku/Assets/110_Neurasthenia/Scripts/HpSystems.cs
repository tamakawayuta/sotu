using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 必要
using PublicUI;

/// <summary>
/// HPを管理する
/// </summary>

namespace Neurasthenia
{
    public class HpSystems : MonoBehaviour
    {
        // ダメージを受けた画像
        [SerializeField]
        private Sprite damagedHpImage;

        // 通常時の画像
        [SerializeField]
        private Sprite normalHpImage;

        // 必要なオブジェクトの取得
        [SerializeField]
        private GameObject gameoverUI;
        [SerializeField]
        private GameObject counter;

        // HPを管理するためのスタック
        private Stack<Transform> childHps = new Stack<Transform>();
        private Stack<Transform> damagedHps = new Stack<Transform>();

        // このオブジェクトの子供のHPオブジェクトの数を管理する
        private int childAmounts;

        private void Awake()
        {
            // 子供数を取得する
            childAmounts = this.gameObject.transform.childCount;

            // 子供のオブジェクトを通常のスタックに追加する
            for (var i = 0; i < childAmounts; i++)
            {
                childHps.Push(this.gameObject.transform.GetChild(i));
            }

            // 子供のHPオブジェクトを初期化するため、配列に渡す
            Transform[] children = childHps.ToArray();

            foreach (var child in children)
            {
                // 通常時の画像を使用する
                child.gameObject.GetComponent<Image>().sprite = normalHpImage;

                // 色を緑にする
                child.gameObject.GetComponent<Image>().color = Color.green;
            }
        }

        // ダメージを受けたときの処理
        public void DamagedHp()
        {
            // 通常のスタックからHPオブジェクトを取得する
            var childHp = childHps.Pop();

            // 取得したオブジェクトをダメージを受けた画像にする
            childHp.gameObject.GetComponent<Image>().sprite = damagedHpImage;

            // 色を黒くする
            childHp.gameObject.GetComponent<Image>().color = Color.black;

            //ダメージを受けたオブジェクトを管理するスタックに追加する
            damagedHps.Push(childHp);

            // HPが0になったらゲームオーバー
            if (childHps.Count == 0)
            {
                gameoverUI.GetComponent<GameOverSystems>().AppearGameOverUI(counter.GetComponent<CounterSystems>().GetAnswerAmount());
                return;
            }

            // HPの色を更新する
            UpdateHpColor();
        }

        // HPを回復する処理
        public void HealedHp()
        {
            // HPが満タンなら処理しない
            // つまり、ダメージのスタックが0なら
            if (damagedHps.Count == 0)
            {
                return;
            }

            // ダメージのスタックからHPオブジェクトを取り出す
            var childHp = damagedHps.Pop();

            // 取得したオブジェクトの画像を通常のものにする
            childHp.gameObject.GetComponent<Image>().sprite = normalHpImage;

            // 大きさを拡大し、回復した感じを出す
            childHp.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.0f);

            // 大きさを戻す
            MinimumHpImage(childHp.gameObject);

            // 通常のスタックに追加する
            childHps.Push(childHp);

            // HPの色を更新する
            UpdateHpColor();
        }

        // HPオブジェクトの色を更新する
        private void UpdateHpColor()
        {
            // HPに応じて全体の色を変化させる
            // そのために通常のHPオブジェクトを取り出す
            Transform[] children = childHps.ToArray();

            // 変化させる色を管理する
            var hpColor = Color.clear;

            // 変化させる色を決定する
            switch (childHps.Count)
            {
                case 5: // 5または4なら緑
                case 4:
                    hpColor = Color.green;
                    break;
                case 3: // 3または2なら黄色
                case 2:
                    hpColor = Color.yellow;
                    break;
                case 1: // 1なら赤
                    hpColor = Color.red;
                    break;
                default:
                    Debug.LogError("Illegal Value");
                    break;
            }

            // 通常オブジェクトの色を更新する
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }
        }

        // HPを元の大きさに戻す非同期関数
        private async void MinimumHpImage(GameObject hp)
        {
            // 渡されたHPオブジェクトの大きさを取得する
            var scaleX = hp.GetComponent<Transform>().localScale.x;
            var scaleY = hp.GetComponent<Transform>().localScale.y;

            while (scaleX > 1.0f)
            {
                // 大きさを少し縮小する
                scaleX -= 0.1f;
                scaleY -= 0.1f;
                hp.GetComponent<Transform>().localScale = new Vector3(scaleX, scaleY, 1.0f);

                // ディレイをかけてアニメーション
                await Task.Delay(30);
            }
        }
    }
}