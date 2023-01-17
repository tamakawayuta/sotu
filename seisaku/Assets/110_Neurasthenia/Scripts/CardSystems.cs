using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 必要
using SoundManager;

/// <summary>
/// メインゲームの定義
/// </summary>

namespace Neurasthenia
{
    public class CardSystems : MonoBehaviour
    {
        // カードの裏面の画像
        [SerializeField]
        private Sprite backImage;

        // カードの表面の画像たち
        [SerializeField]
        private Sprite[] frontImages;

        // 必要なオブジェクトの取得
        [SerializeField]
        private GameObject hpGauge;
        [SerializeField]
        private GameObject counter;
        [SerializeField]
        private GameObject panel;
        [SerializeField]
        private GameObject soundManager;

        // 選ばれたカードの表面の画像を管理する配列
        private Sprite[] selectImages;

        // このオブジェクト配下の子供のゲームオブジェクトを管理するリスト
        private List<GameObject> cards = new List<GameObject>();

        // 選択されたカードオブジェクトを管理するリスト
        private List<GameObject> clickCards = new List<GameObject>();

        private void Awake()
        {
            // 初期化
            this.clickCards.Clear();

            // 子供のカードオブジェクトを取得する
            for (var i = 0;i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            // 取得した子供のイメージを裏面の画像にする
            foreach(var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
            }

            // 使用する表面の画像を選択する
            SelectCards();
        }

        // 渡された画像の配列をシャッフルする関数
        // フィッシャ―イェーツのシャッフル
        private void ShuffleCardImages(Sprite[] images)
        {
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        // 使用するカードを選択する
        private void SelectCardImages()
        {
            // 初期化
            selectImages = new Sprite[cards.Count];

            // インデックスが小さい方から規定数選ぶ
            for (int i = 0; i < cards.Count/2; i++)
            {
                selectImages[i] = frontImages[i];
                // 神経衰弱は同じ画像が2つペアになるため、もう1枚追加する
                selectImages[i + cards.Count / 2] = frontImages[i];
            }
        }

        private void SelectCards()
        {
            // 表面になる候補の画像たちをシャッフルする
            ShuffleCardImages(frontImages);
            // シャッフルした画像たちから使用する画像を選ぶ
            SelectCardImages();
            // 選んだ画像をさらに混ぜる
            ShuffleCardImages(selectImages);
        }

        // 不正解のときカードを戻す非同期関数
        private async void RemoveCards()
        {
            // カードの絵柄を覚えさせるため、ディレイを入れる
            await Task.Delay(1000);

            // ポーズ中にカードを裏返さない様にする
            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            // パネルをアクティブにして他のカードを選択させないようにする
            panel.GetComponent<PanelSystems>().switchPanelActive();

            foreach (var card in clickCards)
            {
                // 選択されたカードを再選択できるようにする
                card.GetComponent<Button>().enabled = true;

                // 選択されたカードを裏面にする
                card.GetComponent<Image>().sprite = backImage;
            }

            // リストをリセット
            this.clickCards.Clear();
        }

        // カードを再配置する非同期関数
        private async void ResetCards()
        {
            // 再配置するようなSEを流す
            soundManager.GetComponent<SoundSystems>().PlaySE(1);

            // HPを2つ回復する
            hpGauge.GetComponent<HpSystems>().HealedHp();
            hpGauge.GetComponent<HpSystems>().HealedHp();

            foreach (var card in cards)
            {
                // 場のカードを裏面にする
                card.GetComponent<CardEvents>().TurnBack(this.backImage);
            }

            // 再配置された感を出すために少しディレイ
            await Task.Delay(700);

            // 使用する表面の画像を再選択する
            SelectCards();

            foreach (var card in cards)
            {
                // 場のカードを選択できるようにする
                card.GetComponent<Button>().enabled = true;
            }
        }

        // 選択された画像が同じか判定する
        public void CheckAnswers()
        {
            // 画像が同じであるとき
            if (this.clickCards[0].GetComponent<Image>().sprite == this.clickCards[1].GetComponent<Image>().sprite)
            {
                // リストをリセット
                this.clickCards.Clear();

                // 正解のSEを再生する
                soundManager.GetComponent<SoundSystems>().PlaySE(2);

                // カウントオブジェクトに正解数を増やすよう命令する
                counter.GetComponent<CounterSystems>().UpdateCount();

                // もし場のカードが全て表になったら
                // つまり、正解数が5の倍数になったら
                if (counter.GetComponent<CounterSystems>().CheckCount())
                {
                    // カードを再配置する
                    ResetCards();
                }
            }
            else
            {
                // カードを裏面に戻す
                RemoveCards();
                
                // パネルを戻す
                panel.GetComponent<PanelSystems>().switchPanelActive();

                // 不正解のSEを再生する
                soundManager.GetComponent<SoundSystems>().PlaySE(3);

                // HPを減らす
                hpGauge.GetComponent<HpSystems>().DamagedHp();
            }
        }

        // 選択されたカードの画像のゲッター
        public Sprite GetCardImages(int cardNum)
        {
            return this.selectImages[cardNum];
        }

        // 選択されたカードオブジェクトを追加する
        public void SetClickCards(GameObject clicked)
        { 
            clickCards.Add(clicked);
            
            //選択したときのSEを再生する
            soundManager.GetComponent<SoundSystems>().PlaySE(0);
        }

        // 選択されたカードオブジェクトの数を返す
        public int GetClickCardsCount()
        {
            return this.clickCards.Count;
        }
    }
}