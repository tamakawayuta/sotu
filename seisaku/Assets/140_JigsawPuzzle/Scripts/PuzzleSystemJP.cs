using System.Collections.Generic;
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

        // パズルピースに格納するバラバラの画像の管理
        private List<Sprite> sprites = new List<Sprite>();

        // 答えの管理
        private List<Sprite> answers;

        // 選択されているピースの管理
        private List<GameObject> selected = new List<GameObject>();

        // 使用する画像の名前
        private string fileName;

        private void Awake()
        {
            // 画像をランダムに選ぶ
            var index = Random.Range(0, 1);
            SetFileName(index);

            // 対応する画像を読み込む
            LoadSprites(this.fileName);
            hintButton.GetComponent<HintButtonEventsJP>().SetHintSprite(this.fileName);

            // バラバラ画像をシャッフル
            ShuffleSprites(this.sprites);

            // 画像をピースにセット
            fields.GetComponent<FieldSystemJP>().InstantiateFields(this.sprites.Count);
            fields.GetComponent<FieldSystemJP>().DrawSprites(this.sprites);

            // 既に正しい場所にある画像についての処理
            UpdateTrueFieldState();
        }

        // 対応する画像名のセット
        private void SetFileName(int index)
        {
            switch (index)
            {
                case 0:
                    this.fileName = "puzzleImage";
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
        private void CheckAnswer()
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