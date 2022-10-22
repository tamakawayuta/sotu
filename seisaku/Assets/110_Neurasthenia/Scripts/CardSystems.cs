using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using SoundManager;

namespace Neurasthenia
{
    /// <summary>
    /// ゲームの定義
    /// </summary>

    [DisallowMultipleComponent]
    public sealed class CardSystems : MonoBehaviour
    {
        [SerializeField,TooltipAttribute("カードの裏面のスプライト")]
        private Sprite backImage;         
        [SerializeField,TooltipAttribute("カードの表面のスプライト")]
        private Sprite[] frontImages;       

        [SerializeField, TooltipAttribute("HPゲージを入れる")]
        private GameObject hpGauge;
        [SerializeField, TooltipAttribute("スコアを記録するテキストを入れる")]
        private GameObject counter;
        [SerializeField, TooltipAttribute("パネルを入れる")]
        private GameObject panel;
        [SerializeField, TooltipAttribute("音オブジェクトを入れる")]
        private GameObject soundManager;

        //カードオブジェクトの管理
        private List<GameObject> cards = new List<GameObject>();
        //選択されたカードの管理
        private List<GameObject> clickCards = new List<GameObject>();
        //選択された画像素材の管理
        private Sprite[] selectImages;

        private void Awake()
        {
            //カードオブジェクトの取得
            for (var i = 0;i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            //カードオブジェクトを裏面にする
            foreach(var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
            }

            //画像素材をシャッフルする
            ShuffleCardImages(frontImages);
            //その中から画像を選択する
            SelectCardImages();
            //選択した画像をシャッフルする
            ShuffleCardImages(selectImages);

            //初期化
            this.clickCards.Clear();
        }

        private void ShuffleCardImages(Sprite[] images)
        {
            //フィッシャー-イェーツのシャッフル法
            //高速でメモリ使用量も固定
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        private void SelectCardImages()
        {
            //カードオブジェクトと同じ要素数の配列を用意
            selectImages = new Sprite[cards.Count];

            for (int i = 0; i < cards.Count/2; i++)
            {
                //カードを選択して格納する
                //同じ画像が2枚必要なので格納する
                selectImages[i] = frontImages[i];
                selectImages[i + cards.Count / 2] = frontImages[i];
            }
        }

        //選択されたカードがあっているかの判定処理
        public void CheckAnswers()
        {
            //選択されたカードの画像が同じ
            if (this.clickCards[0].GetComponent<Image>().sprite == this.clickCards[1].GetComponent<Image>().sprite)
            {
                //選択されたカード記録をリセット
                this.clickCards.Clear();
                //正解のSEの再生
                soundManager.GetComponent<SoundSystems>().PlaySE(2);
                //スコア加算
                counter.GetComponent<CounterSystems>().UpdateCount();
                //全てめくられたらカードを再配置する
                if (counter.GetComponent<CounterSystems>().CheckCount())
                {
                    ResetCards();
                }
            }
            //選択されたカードの画像が違う
            else
            {
                //カードを裏返しに戻す
                RemoveCards();
                //パネルをアクティブ化
                panel.GetComponent<PanelSystems>().switchPanelActive();
                //間違いのSEを再生
                soundManager.GetComponent<SoundSystems>().PlaySE(3);
                //Hpを減らす
                hpGauge.GetComponent<HpSystems>().DamagedHp();
            }
        }

        //選択されたカードオブジェクトが持つ画像を返す
        public Sprite GetCardImages(int cardNum)
        {
            return this.selectImages[cardNum];
        }

        public void SetClickCards(GameObject clicked)
        {
            //カードをめくるSEの再生
            soundManager.GetComponent<SoundSystems>().PlaySE(0);
            //選択されたカードオブジェクトの保存
            clickCards.Add(clicked);
        }

        //何枚カードオブジェクトが選択されているか返す
        public int GetClickCardsCount()
        {
            return this.clickCards.Count;
        }

        private async void RemoveCards()
        {
            //カードの画像を見せるためのディレイ
            await Task.Delay(1000);

            //ポーズ画面やゲームオーバーが表示されているときは処理を中断
            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            //選択されたカードを裏面に戻す
            foreach (var card in clickCards)
            {
                card.GetComponent<Image>().sprite = backImage;
                //カードをまた選べるようにする
                card.GetComponent<Button>().enabled = true;
            }

            //選択した記録のリセット
            this.clickCards.Clear();
            //パネルを非アクティブ化
            panel.GetComponent<PanelSystems>().switchPanelActive();
        }

        private async void ResetCards()
        {
            //再配置するSEを再生
            soundManager.GetComponent<SoundSystems>().PlaySE(1);

            //体力を2つ回復する
            hpGauge.GetComponent<HpSystems>().HealedHp();
            hpGauge.GetComponent<HpSystems>().HealedHp();

            //少し待ってからカードを裏返す
            await Task.Delay(700);
            foreach (var card in cards)
            {
                card.GetComponent<Image>().sprite = backImage;
                //カードを選べるようにする
                card.GetComponent<Button>().enabled = true;
            }

            //画像素材の再選択
            ShuffleCardImages(frontImages);
            SelectCardImages();
            ShuffleCardImages(selectImages);
        }
    }
}