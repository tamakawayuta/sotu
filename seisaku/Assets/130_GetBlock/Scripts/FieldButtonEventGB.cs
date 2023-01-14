using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フィールドの駒のOnClickの定義
/// </summary>

namespace GetBlock
{
    public class FieldButtonEventGB : MonoBehaviour
    {
        // 選択された画像の管理
        public static Sprite selectedSprite;

        private void Awake()
        {
            // 初期化
            selectedSprite = null;
        }

        public void OnClickField(int index)
        {
            // 違う画像同士をクリックしても処理しない
            if (selectedSprite != null &&
                selectedSprite != this.gameObject.GetComponent<Image>().sprite)
            {
                return;
            }

            // 選択された駒は再選択できないようにする
            this.gameObject.GetComponent<Image>().color = Color.black;
            this.gameObject.GetComponent<Button>().enabled = false;

            // 選択された画像を記録する
            selectedSprite = this.gameObject.GetComponent<Image>().sprite;

            // フィールドを更新する
            GameObject.Find("GameDirector").GetComponent<GameMainGB>().UpdateGameState(index,this.gameObject.GetComponent<Image>().sprite);
        }
    }
}