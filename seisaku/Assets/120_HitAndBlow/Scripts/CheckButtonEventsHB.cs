using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 必要
using PublicUI;
using SoundManager;

/// <summary>
/// 答えの確認をするボタンのOnClick
/// </summary>

namespace HitAndBlow
{
    public class CheckButtonEventsHB : MonoBehaviour
    {
        // 必要なものの取得
        [SerializeField]
        private GameObject images;
        [SerializeField]
        private GameObject texts;
        [SerializeField]
        private GameObject answerCard;
        [SerializeField]
        private GameObject showHintText;
        [SerializeField]
        private GameObject detail;
        [SerializeField]
        private GameObject endUI;
        [SerializeField]
        private GameObject sound;

        // 回答した数の管理
        private int count = 0;

        private void Awake()
        {
            // 押せないようにする初期化
            this.gameObject.GetComponent<Image>().color = Color.gray;
            this.gameObject.GetComponent<Button>().enabled = false;
        }

        private void Update()
        {
            // 答えを全て入力したら押せるようにする
            if (images.GetComponent<RecordImageSystemsHB>().GetIsImageSetFull())
            {
                this.gameObject.GetComponent<Image>().color = Color.white;
                this.gameObject.GetComponent<Button>().enabled = true;
            }
            else
            {
                this.gameObject.GetComponent<Image>().color = Color.gray;
                this.gameObject.GetComponent<Button>().enabled = false;
            }
        }

        // ボタンを押したときの定義
        public async void OnClickCheck()
        {
            // 判定した結果を受け取る一時的な配列
            int[] amounts = new int[2];

            // 判定
            amounts = answerCard.GetComponent<CardSystemsHB>().CheckAnswer(images.GetComponent<RecordImageSystemsHB>().GetSelectedSprites());

            // 使いやすいように変数に格納
            var hit = amounts[0];
            var blow = amounts[1];

            // 回答数をカウント
            count++;

            // 4ヒットならクリア
            if (hit == 4)
            {
                detail.GetComponent<DetailSystemHB>().DisappearDetail();
                sound.GetComponent<SoundSystems>().PlaySE(3);
                // 答えを表示
                answerCard.GetComponent<CardSystemsHB>().AppearAnswer();
                await Task.Delay(2000);
                // クリア画面を表示
                showHintText.GetComponent<ShowHintSystemHB>().ShowText("4ヒット");
                // ヒットとブローの数を記録する
                texts.GetComponent<RecordTextSystemsHB>().DrawText(hit, blow);
                await Task.Delay(2000);
                // ゲームクリアUIを表示
                endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("おめでとう!!");
            }
            // 回答が8まで行ったらゲームオーバー
            else if (count == 8)
            {
                detail.GetComponent<DetailSystemHB>().DisappearDetail();
                sound.GetComponent<SoundSystems>().PlaySE(3);
                answerCard.GetComponent<CardSystemsHB>().AppearAnswer();
                await Task.Delay(2000);
                // クリア画面を表示
                showHintText.GetComponent<ShowHintSystemHB>().ShowText("おしい...");
                // ヒットとブローの数を記録する
                texts.GetComponent<RecordTextSystemsHB>().DrawText(hit, blow);
                await Task.Delay(2000);
                endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("がんばったね!!");
            }
            // それら以外はヒットとブローの数を表示する
            else
            {
                sound.GetComponent<SoundSystems>().PlaySE(2);
                showHintText.GetComponent<ShowHintSystemHB>().ShowText(hit + "ヒット " + blow + "ブロー");
                // ヒットとブローの数を記録する
                texts.GetComponent<RecordTextSystemsHB>().DrawText(hit, blow);
                // 配列のリセット
                images.GetComponent<RecordImageSystemsHB>().SetImageNow();
                detail.GetComponent<DetailSystemHB>().MoveDetail();
            }
        }
    }
}