using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 必要
using PublicUI;

/// <summary>
/// 答えの設定とその判定
/// </summary>

namespace HitAndBlow
{
    public class CardSystemsHB : MonoBehaviour
    {
        // カードの裏面の画像
        [SerializeField]
        private Sprite backImage;

        // カードの表面の画像たち
        [SerializeField]
        private Sprite[] frontImages;

        // 必要なものの取得
        [SerializeField]
        private GameObject endUI;

        // 子供のカードオブジェクトの管理
        private List<GameObject> cards = new List<GameObject>();

        // 実際に使用する表面の画像の管理
        private Sprite[] selectImages;

        private void Awake()
        {
            // 子供の取得
            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                cards.Add(this.gameObject.transform.GetChild(i).gameObject);
            }

            foreach (var card in cards)
            {
                // 画像を裏面にする
                card.GetComponent<Image>().sprite = backImage;
                card.GetComponent<Image>().color = Color.white;
            }

            // 表面の画像たちをシャッフル
            ShuffleCardImages(frontImages);

            // 使用する画像を選択する
            SelectCardImages();

            // 選択した画像をさらにシャッフル
            ShuffleCardImages(selectImages);

            for (var i = 0; i < 4; i++)
            {
                this.cards[i].GetComponent<Image>().sprite = selectImages[i];
            }
        }

        // 受け取った配列をシャッフルする
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

        // 使用する画像を選択する
        private void SelectCardImages()
        {
            // 子供の数と同数選ぶように初期化
            selectImages = new Sprite[cards.Count];

            for (int i = 0; i < 4; i++)
            {
                // ランダムなインデックスを選ぶ
                // 同じ画像が選ばれることもある
                var randomIndex = Random.Range(0, frontImages.Length);
                selectImages[i] = frontImages[randomIndex];
            }
        }

        // カードを表にして答えを示す
        public async void AppearAnswer()
        {
            // 各子供について呼び出す
            for (var i = 0; i < selectImages.Length; i++)
            {
                AppearAnimation(cards[i], i);
            }

            await Task.Delay(3800);

            // ゲームクリアUIを表示
            endUI.GetComponent<GameOverSystems>().AppearUIOnlyText("おめでとう!!");
        }

        // カードを表にするアニメーション
        private async void AppearAnimation(GameObject card,int index)
        {
            // 受け取ったカードオブジェクトの大きさを取得
            var scaleX = card.GetComponent<RectTransform>().localScale.x;
            var scaleY = card.GetComponent<RectTransform>().localScale.y;

            // Xを減らしてめくる感じにする
            while (scaleX > 0f)
            {
                card.GetComponent<RectTransform>().localScale = new Vector3(scaleX,scaleY,1f);
                scaleX -= 0.05f;
                await Task.Delay(30);
            }

            // 対応する表の画像をセット
            card.GetComponent<Image>().sprite = selectImages[index];

            // Xを元に戻す
            while (scaleX < 1f)
            {
                card.GetComponent<RectTransform>().localScale = new Vector3(scaleX, scaleY, 1f);
                scaleX += 0.05f;
                await Task.Delay(30);
            }
        }

        public int[] CheckAnswer(Sprite[] answers)
        {
            int[] hitAndBlowAmounts = new int[2];

            // 受け取った配列の長さがおかしいときは-1を返す
            if (answers.Length != 4)
            {
                Debug.LogError("argsments length not equals 4");
                return hitAndBlowAmounts;
            }

            // ヒット数の管理
            var hitAmount = 0;

            // ブロー数の管理
            var blowAmount = 0;

            // 既にヒットと判定されたところはブローの判定をしない
            bool[] didAddAmount = { false, false, false, false };

            // ヒットの判定
            for (var i = 0; i < 4; i++)
            {
                // 同じインデックスに同じ画像があるならヒット
                if (selectImages[i] == answers[i])
                {
                    hitAmount++;
                    didAddAmount[i] = true;
                }
            }

            // ブローの判定
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    // 違うインデックスに同じ画像があり
                    // かつそれがヒット判定された画像でないならブロー
                    if (answers[i] == selectImages[j] && 
                        i != j &&
                        !didAddAmount[j])
                    {
                        blowAmount++;
                        didAddAmount[j] = true;
                    }
                }
            }

            // 求めた数をセットする
            hitAndBlowAmounts[0] = hitAmount;
            hitAndBlowAmounts[1] = blowAmount;

            return hitAndBlowAmounts;
        }
    }
}