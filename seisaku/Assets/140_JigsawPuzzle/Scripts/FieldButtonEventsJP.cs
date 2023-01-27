using UnityEngine;
using UnityEngine.UI;
using SoundManager;

/// <summary>
/// パズルピースのOnClick
/// </summary>

namespace JigsawPuzzle
{
    public class FieldButtonEventsJP : MonoBehaviour
    {
        // フィールドを管理するオブジェクト
        private PuzzleSystemJP field;

        // 各ピースが持つインデックス
        private int index;

        private void Awake()
        {
            // 取得
            this.field = GameObject.Find("Background_Image").GetComponent<PuzzleSystemJP>();
        }

        // インデックスのセッター
        public void SetIndex(int index)
        {
            this.index = index;
        }

        // インデックスのゲッター
        public int GetIndex()
        {
            return this.index;
        }

        // OnClick定義
        public void OnClickfield()
        {
            // このピースを再選択できないようにする
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponent<Image>().color = Color.white;

            GameObject.Find("SoundManager").GetComponent<SoundSystems>().PlaySE(0);

            // 選択されたことを通知する
            field.AddSelected(this.gameObject);
        }
    }
}