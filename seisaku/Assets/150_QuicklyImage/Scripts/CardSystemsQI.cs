using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 必要
using PublicUI;
using SoundManager;

/// <summary>
/// ゲームの定義
/// </summary>

namespace QuicklyImage
{
    public class CardSystemsQI : MonoBehaviour
    {
        // カードの画像たち
        [SerializeField]
        private Sprite[] sprites;

        // 必要なものの取得
        [SerializeField]
        private GameObject select;
        [SerializeField]
        private GameObject answerObject;
        [SerializeField]
        private GameObject time;
        [SerializeField]
        private GameObject guide;
        [SerializeField]
        private GameObject score;
        [SerializeField]
        private GameObject clearUI;
        [SerializeField]
        private GameObject sound;

        // 選択肢となるオブジェクトたち
        private List<GameObject> selectButtons = new List<GameObject>();

        // 実際に使用する画像の管理
        private List<Sprite> useSprites = new List<Sprite>();

        // 制限時間
        private int delayTime = 100;

        // シャッフルアニメーションのフラグ
        private bool isShuffleTime = true;

        private async void Awake()
        {
            // 選択肢となるオブジェクトを取得
            for (var i = 0; i < select.transform.childCount; i++)
            {
                this.selectButtons.Add(this.select.transform.GetChild(i).gameObject);
            }

            // 使用する画像をリストで管理
            foreach (var sprite in sprites)
            {
                this.useSprites.Add(sprite);
            }

            sound.GetComponent<SoundSystems>().PlaySE(0);
            ShuffleAnimation();

            // 画像をシャッフル
            ShuffleSprites(useSprites);

            //フェードインが終わるまで待機
            await Task.Delay(2000);

            // ポーズ中なら解除されるまで待機
            while (Time.timeScale == 0)
            {
                await Task.Delay(1000);
            }

            this.isShuffleTime = false;

            // 選択肢をセット
            for (var i = 0; i < selectButtons.Count; i++)
            {
                selectButtons[i].GetComponent<SelectButtonEventsQI>().SetSprite(useSprites[i]);
            }

            // 正解をセット
            SetAnswer();

            // カウントダウンを開始する
            this.time.GetComponent<TimeSystemsQI>().StartCountdown(delayTime);
        }

        // 2問目以降の処理
        private async void ReShuffle()
        {
            // 画像をもう一度シャッフルする
            ShuffleSprites(useSprites);

            // 少し間を開けてプレイヤーに一呼吸させる
            await Task.Delay(1000);

            // ポーズ中なら解除されるまで待機
            while (Time.timeScale == 0)
            {
                await Task.Delay(1000);
            }

            sound.GetComponent<SoundSystems>().PlaySE(0);
            ShuffleAnimation();

            await Task.Delay(700);

            // 問題を出すことを伝える
            this.guide.GetComponent<GuideSystemQI>().SetText("せーの..!");
            await Task.Delay(1000);

            this.isShuffleTime = false;

            // 選択肢をセット
            for (var i = 0; i < selectButtons.Count; i++)
            {
                selectButtons[i].GetComponent<SelectButtonEventsQI>().SetSprite(useSprites[i]);
            }

            // 答えをセット
            SetAnswer();

            // カウントダウンを開始
            this.time.GetComponent<TimeSystemsQI>().StartCountdown(delayTime);
        }

        // フィッシャ―イェーツのシャッフル
        private void ShuffleSprites(List<Sprite> sprites)
        {
            for (var i = sprites.Count - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = sprites[i];
                sprites[i] = sprites[j];
                sprites[j] = tmp;
            }
        }

        // 答えを設定する
        private void SetAnswer()
        {
            // 選択肢として設定した4つの中から1つを答えとする
            var index = Random.Range(0, 4);
            this.answerObject.GetComponent<AnswerEventsQI>().SetAnswerSprite(this.useSprites[index]);

            this.guide.GetComponent<GuideSystemQI>().SetText("これをえらべ！");
        }

        //  シャッフルのアニメーション
        private async void ShuffleAnimation()
        {
            this.isShuffleTime = true;

            while (this.isShuffleTime)
            {
                foreach (var button in selectButtons)
                {
                    var index = Random.Range(0, this.sprites.Length);
                    button.GetComponent<Image>().sprite = this.sprites[index];
                }

                var answerIndex = Random.Range(0, this.sprites.Length);
                this.answerObject.GetComponent<Image>().sprite = this.sprites[answerIndex];

                await Task.Delay(20);
            }
        }

        // レベルデザイン
        // コールするほど制限時間が短くなる
        private void UpdateDelayTime()
        {
            if (this.delayTime < 10)
            {
                return;
            }

            this.delayTime -= 3;
        }

        // 答えの判定
        public async void CheckAnswer(Sprite sprite)
        {
            // 正解の処理
            if (sprite == this.answerObject.GetComponent<Image>().sprite)
            {
                sound.GetComponent<SoundSystems>().PlaySE(1);
                this.guide.GetComponent<GuideSystemQI>().SetText("いいね！");
                this.time.GetComponent<TimeSystemsQI>().SetIsTimeUp();

                // 制限時間を減らす
                UpdateDelayTime();

                // 時間に応じてスコアを加算する
                float bonusValue = this.time.GetComponent<Image>().fillAmount;
                bonusValue *= 100;
                int newScore = (int)bonusValue;

                for (var i=1;i < newScore + 1; i++)
                {
                    this.score.GetComponent<ScoreSystemsQI>().AddScore(i);
                    await Task.Delay(20);
                }
            }
            // 不正解ならゲームオーバー
            else
            {
                AppearClearUI();
            }

            // 再シャッフル
            ReShuffle();
        }

        // ゲーム終了時のウィンドウを表示する
        public void AppearClearUI()
        {
            this.clearUI.GetComponent<GameOverSystems>().AppearGameOverUI(this.score.GetComponent<ScoreSystemsQI>().GetScore());
        }
    }
}