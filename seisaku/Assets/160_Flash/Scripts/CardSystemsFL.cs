using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

//必要
using PublicUI;
using SoundManager;

/// <summary>
/// ゲームの定義
/// </summary>

namespace Flash
{
    public class CardSystemsFL : MonoBehaviour
    {
        // 画像郡
        [SerializeField]
        private Sprite[] sprites;

        // 必要なものの取得
        [SerializeField]
        private GameObject answer;
        [SerializeField]
        private GameObject button;
        [SerializeField]
        private GameObject count;
        [SerializeField]
        private GameObject clearUI;
        [SerializeField]
        private GameObject praise;
        [SerializeField]
        private GameObject sound;

        // 実際に使用する画像群
        private List<Sprite> useSprites;

        // 答えの管理
        private Queue<Sprite> answers;

        // 正解数の管理
        private int answerAmount = 0;

        // 答えとなる画像の枚数の管理
        private int selectSpriteAmount = 1;

        // 答えを表示する間隔の管理
        private int delayTime = 2000;

        private async void Awake()
        {
            // 選択肢を非表示
            button.SetActive(false);
            answer.SetActive(false);

            // 画像をシャッフル
            ShuffleSprites(this.sprites);

            // 画像を選択する
            SelectSprites(selectSpriteAmount);

            await Task.Delay(2000);

            // 選択した画像を答えとして表示
            await this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites,2000);
            this.answers = new Queue<Sprite>(useSprites);

            // 全てのスプライトをシャッフル
            ShuffleSprites(this.sprites);

            while (true)
            {
                if (this.useSprites.Count == 8)
                {
                    break;
                }

                var index = Random.Range(0, this.sprites.Length);

                if (!this.useSprites.Contains(this.sprites[index]))
                {
                    this.useSprites.Add(this.sprites[index]);
                }
            }

            ShuffleSprites(this.useSprites);

            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            // 答えを示すオブジェクトを非アクティブ化
            this.answer.SetActive(false);

            // 選択肢を表示
            sound.GetComponent<SoundSystems>().PlaySE(2);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.useSprites);
        }

        // フィッシャ―イェーツのシャッフル
        private void ShuffleSprites(Sprite[] sprites)
        {
            for (var i = sprites.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = sprites[i];
                sprites[i] = sprites[j];
                sprites[j] = tmp;
            }
        }

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

        // 使用する画像を選択する
        private void SelectSprites(int selectAmount)
        {
            useSprites = new List<Sprite>();
            
            for (var i = 0; i < selectAmount; i++)
            {
                useSprites.Add(this.sprites[i]);
            }
        }

        // 2問目以降の処理
        private async void ResetSprites()
        {
            // 正解数をカウント
            this.answerAmount++;

            // シャッフルする
            ShuffleSprites(this.sprites);

            // 答えとなる画像の枚数を増やすか判定する
            UpdateSelectSpriteAmount();

            // 答えを選択する
            SelectSprites(selectSpriteAmount);

            // 答えを示す
            await this.answer.GetComponent<AnswerSystemFL>().SetSprites(this.useSprites, 2000);
            this.answers = new Queue<Sprite>(useSprites);

            // 選択肢を設定する
            ShuffleSprites(this.sprites);

            while (true)
            {
                if (this.useSprites.Count == 8)
                {
                    break;
                }

                var index = Random.Range(0, this.sprites.Length);

                if (!this.useSprites.Contains(this.sprites[index]))
                {
                    this.useSprites.Add(this.sprites[index]);
                }
            }

            ShuffleSprites(this.useSprites);

            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            this.answer.SetActive(false);

            sound.GetComponent<SoundSystems>().PlaySE(2);
            this.button.GetComponent<SelectSystemFL>().SetButtonSprites(this.useSprites);
        }

        // 正解となる画像の枚数を増やすか判定
        private void UpdateSelectSpriteAmount()
        {
            // 用意した画像よりも答えとなる画像の枚数が多い場合は、答えを表示する間隔を減らす
            if (this.selectSpriteAmount > 7)
            {
                UpdateDelayTime();
                return;
            }

            // 正解数が3の倍数になるとき、答えとなる画像の枚数を増やす
            if (this.answerAmount % 3 == 0)
            {
                this.selectSpriteAmount++;
            }
        }

        // 答えを表示する間隔の設定
        private void UpdateDelayTime()
        {
            // 下限値の設定
            if (this.delayTime < 0)
            {
                return;
            }

            this.delayTime -= 20;
        }

        // 答えの判定
        public async void CheckAnswer(Sprite sprite)
        {
            var answer = this.answers.Dequeue();

            // 正解の処理
            if (answer == sprite)
            {
                sound.GetComponent<SoundSystems>().PlaySE(1);
                // スコアを加算
                this.count.GetComponent<CountSystemFL>().AddCount();

                // 全て回答されたら次の問題を出す
                if (this.answers.Count == 0)
                {

                    await this.praise.GetComponent<PraiseSystemFL>().AppearText();

                    this.button.SetActive(false);
                    ResetSprites();
                }
            }
            // 不正解の処理
            else
            {
                this.clearUI.GetComponent<GameOverSystems>().AppearGameOverUI(this.count.GetComponent<CountSystemFL>().GetCount());
            }
        }
    }
}