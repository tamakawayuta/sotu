using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicUI;
using System.Threading.Tasks;

namespace Neurasthenia
{
    /// <summary>
    /// 体力を管理するオブジェクト
    /// </summary>

    [DisallowMultipleComponent]
    public sealed class HpSystems : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("ダメージを受けたHP画像を入れる")]
        private Sprite damagedHpImage;
        [SerializeField, TooltipAttribute("通常状態のHP画像を入れる")]
        private Sprite normalHpImage;
        [SerializeField, TooltipAttribute("ゲーム終了時に表示するUIを入れる")]
        private GameObject gameoverUI;
        [SerializeField, TooltipAttribute("スコアを記録するオブジェクトを入れる")]
        private GameObject counter;

        //通常状態のHPオブジェクトを管理する
        private Stack<Transform> childHps = new Stack<Transform>();
        //ダメージを受けたHPオブジェクトを管理する
        private Stack<Transform> damagedHps = new Stack<Transform>();

        //HP数
        private int childAmounts;

        private void Awake()
        {
            //HP数の取得
            childAmounts = this.gameObject.transform.childCount;

            //HPオブジェクトの取得
            for (var i = 0; i < childAmounts; i++)
            {
                childHps.Push(this.gameObject.transform.GetChild(i));
            }

            //HPオブジェクトの初期化
            Transform[] children = childHps.ToArray();
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().sprite = normalHpImage;
                child.gameObject.GetComponent<Image>().color = Color.green;
            }
        }

        //ダメージ処理
        public void DamagedHp()
        {
            //HPオブジェクトを1つ取り出す
            var childHp = childHps.Pop();
            //ダメージを受けたイメージにする
            childHp.gameObject.GetComponent<Image>().sprite = damagedHpImage;
            //色を黒にする
            childHp.gameObject.GetComponent<Image>().color = Color.black;

            //HPが0になったら終了のためUIを表示する
            if (childHps.Count == 0)
            {
                gameoverUI.GetComponent<GameOverSystems>().AppearGameOverUI(counter.GetComponent<CounterSystems>().GetAnswerAmount());
                return;
            }

            //残りHPに合わせて色を決める
            Transform[] children = childHps.ToArray();
            var hpColor = Color.clear;
            //3以下は黄色、1以下は赤
            switch (childHps.Count)
            {
                case 4:
                    hpColor = Color.green;
                    break;
                case 3:
                    hpColor = Color.yellow;
                    break;
                case 2:
                    hpColor = Color.yellow;
                    break;
                case 1:
                    hpColor = Color.red;
                    break;
                default:
                    break;
            }
            //色の更新
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }

            //ダメージを受けたオブジェクトを格納
            damagedHps.Push(childHp);
        }

        //回復処理
        public async void HealedHp()
        {
            //体力が満タンなら無視
            if (damagedHps.Count == 0)
            {
                return;
            }

            await Task.Delay(30);

            //ダメージを受けているHPオブジェクトを1つ取り出す
            var childHp = damagedHps.Pop();
            //画像を通常状態のものにセット
            childHp.gameObject.GetComponent<Image>().sprite = normalHpImage;
            //画像を一時的に拡大して元に戻す
            childHp.gameObject.GetComponent<Transform>().localScale = new Vector3(1.5f,1.5f,1.0f);
            MinimizeHpImage(childHp.gameObject);
            //回復したHPオブジェクトを格納する
            childHps.Push(childHp);

            //残りHPによって色を決める
            Transform[] children = childHps.ToArray();
            var hpColor = Color.clear;
            switch (childHps.Count)
            {
                case 5:
                    hpColor = Color.green;
                    break;
                case 4:
                    hpColor = Color.green;
                    break;
                case 3:
                    hpColor = Color.yellow;
                    break;
                case 2:
                    hpColor = Color.yellow;
                    break;
                default:
                    break;
            }
            //色の更新
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }
        }

        private async void MinimizeHpImage(GameObject hpGauge)
        {
            //渡されたオブジェクトのスケールを取得
            var scaleX = hpGauge.GetComponent<Transform>().localScale.x;
            var scaleY = hpGauge.GetComponent<Transform>().localScale.y;

            //他のHPオブジェクトと同じ大きさになるまで縮小する
            while (scaleX > 1.0f)
            {
                hpGauge.GetComponent<Transform>().localScale = new Vector3(scaleX,scaleY,1.0f);
                scaleX -= 0.01f;
                scaleY -= 0.01f;
                //処理をディレイして縮小速度を調整
                await Task.Delay(20);
            }
        }
    }
}