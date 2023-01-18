using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ヒントの管理
/// </summary>

namespace JigsawPuzzle
{
    public class HintButtonEventsJP : MonoBehaviour
    {
        // 画像を格納するオブジェクトの管理
        [SerializeField]
        private GameObject hint;

        // ゲームで使われる画像の管理
        [SerializeField]
        private Sprite[] sprites;

        // 実際に使われている画像の管理
        private Sprite puzzleImage;

        // ヒント画像のセット
        public void SetHintSprite(string fileName)
        {
            // 対応する画像を取得する
            switch (fileName)
            {
                case "puzzleImage":
                    this.puzzleImage = this.sprites[0];
                    break;
                case "ship":
                    this.puzzleImage = this.sprites[1];
                    break;
                case "KisaragiStation":
                    this.puzzleImage = this.sprites[2];
                    break;
                case "bigBang":
                    this.puzzleImage = this.sprites[3];
                    break;
                case "backroom":
                    this.puzzleImage = this.sprites[4];
                    break;
                default:
                    Debug.LogWarning("Error: Do not Loaded sprite to hintObject");
                    break;
            }

            // 画像をセット
            this.hint.transform.GetChild(0).GetComponent<Image>().sprite = this.puzzleImage;

            // 通常は非アクティブ化
            this.hint.SetActive(false);
        }

        // OnClick定義
        public void OnClickHint()
        {
            this.hint.SetActive(true);
        }
    }
}