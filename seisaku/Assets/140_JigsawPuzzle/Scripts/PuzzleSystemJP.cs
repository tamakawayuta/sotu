using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// 必要
using PublicUI;

/// <summary>
/// ゲームの定義
/// </summary>

namespace JigsawPuzzle
{
    public class PuzzleSystemJP : MonoBehaviour
    {
        // 必要なものの取得
        [SerializeField]
        private GameObject fields;
        [SerializeField]
        private GameObject clear;
        [SerializeField]
        private GameObject hintButton;
        [SerializeField]
        private GameObject answerImage;

        // パズルピースに格納するバラバラの画像の管理
        private List<Sprite> sprites = new List<Sprite>();

        // 答えの管理
        private List<Sprite> answers;

        // 選択されているピースの管理
        private List<GameObject> selected = new List<GameObject>();

        // 使用する画像の名前
        private string fileName;

        private async void Awake()
        {
            // 画像をランダムに選ぶ
            var index = Random.Range(0, 5);
            SetFileName(index);

            // 対応する画像を読み込む
            LoadSprites(this.fileName);
            hintButton.GetComponent<HintButtonEventsJP>().SetHintSprite(this.fileName);
            this.answerImage.GetComponent<AnswerSystemJP>().SetAnswerImage(this.fileName);

            fields.GetComponent<FieldSystemJP>().InstantiateFields(this.sprites.Count);
            fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);

            List<GameObject> fieldState = fields.GetComponent<FieldSystemJP>().GetFields();

            foreach (var field in fieldState)
            {
                field.GetComponent<Image>().color = Color.white;
                field.GetComponent<Button>().enabled = false;
            }

            hintButton.SetActive(false);

            await Task.Delay(5000);

            for (var i = 0; i < 5; i++)
            {
                ShuffleSprites(this.sprites);
                fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);
                await Task.Delay(20);
            }

            // バラバラ画像をシャッフル
            ShuffleSprites(this.sprites);

            // 画像をピースにセット
            fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);

            foreach (var field in fieldState)
            {
                field.GetComponent<Image>().color = new Color(53f, 53f, 53f);
                field.GetComponent<Button>().enabled = true;
            }

            // 既に正しい場所にある画像についての処理
            UpdateTrueFieldState();

            hintButton.SetActive(true);
        }

        // 対応する画像名のセット
        private void SetFileName(int index)
        {
            switch (index)
            {
                case 0:
                    this.fileName = "puzzleImage";
                    break;
                case 1:
                    this.fileName = "ship";
                    break;
                case 2:
                    this.fileName = "KisaragiStation";
                    break;
                case 3:
                    this.fileName = "bigBang";
                    break;
                case 4:
                    this.fileName = "backroom";
                    break;
                default:
                    Debug.LogError("Error: Illegal Index");
                    break;
            }
        }

        // 既に正しい位置にある画像は選択できないようにする
        private void UpdateTrueFieldState()
        {
            List<GameObject> fieldState = fields.GetComponent<FieldSystemJP>().GetFields();

            for (var i = 0; i < fieldState.Count; i++)
            {
                if (this.sprites[i] == answers[i])
                {
                    fieldState[i].GetComponent<Image>().color = Color.white;
                    fieldState[i].GetComponent<Button>().enabled = false;
                }
            }
        }

        // 画像のロード
        private void LoadSprites(string fileName)
        {
            Sprite[] tmp = Resources.LoadAll<Sprite>(fileName);

            foreach (var sprite in tmp)
            {
                this.sprites.Add(sprite);
            }

            this.answers = new List<Sprite>(sprites);
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

        // 画像を入れ替える
        private void ChangeSprite()
        {
            // 選択された2つの画像を取得
            Sprite sprite1 = this.selected[0].GetComponent<Image>().sprite;
            Sprite sprite2 = this.selected[1].GetComponent<Image>().sprite;

            // スワッピング
            this.selected[0].GetComponent<Image>().sprite = sprite2;
            this.selected[1].GetComponent<Image>().sprite = sprite1;

            // 再選択できるようにする
            foreach (var field in selected)
            {
                field.GetComponent<Button>().enabled = true;
            }

            // 正しい位置にある画像は選択できないようにする
            CheckPosition();

            // 答えの判定
            CheckAnswer();

            // リセット
            this.selected.Clear();
        }

        // 答えとフィールドが一致するか判定する
        private async void CheckAnswer()
        {
            List<GameObject> fieldState = fields.GetComponent<FieldSystemJP>().GetFields();

            for (var i = 0; i < 50; i++)
            {
                // 各位置の画像で不一致があれば正解でない
                if (fieldState[i].GetComponent<Image>().sprite != this.answers[i])
                {
                    return;
                }
            }

            this.answerImage.SetActive(true);

            await Task.Delay(3000);

            // 正解のときはクリア画面を呼び出す
            clear.GetComponent<GameOverSystems>().AppearUIOnlyText("おめでとう!!");
        }

        // 正しい位置かの判定
        private void CheckPosition()
        {
            foreach (var field in selected)
            {
                // 選択されたオブジェクトのインデックスを取得
                var index = field.GetComponent<FieldButtonEventsJP>().GetIndex();

                // 画像が一致するなら再選択できないようにする
                if (field.GetComponent<Image>().sprite == this.answers[index])
                {
                    field.GetComponent<Button>().enabled = false;
                }
            }
        }

        // 選択されたオブジェクトを管理する変数の追加関数
        public void AddSelected(GameObject obj)
        {
            this.selected.Add(obj);

            // 2つになったらスワッピングする
            if (this.selected.Count == 2)
            {
                ChangeSprite();
            }
        }
    }
}