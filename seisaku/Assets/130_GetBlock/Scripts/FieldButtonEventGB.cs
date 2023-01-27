using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SoundManager;

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

        public async void OnClickField(int index)
        {
            // 違う画像同士をクリックしても処理しない
            if (selectedSprite != null &&
                selectedSprite != this.gameObject.GetComponent<Image>().sprite)
            {
                return;
            }

            GameObject.Find("SoundManager").GetComponent<SoundSystems>().PlaySE(0);

            // 選択された駒は再選択できないようにする
            this.gameObject.GetComponent<Button>().enabled = false;

            // 選択された画像を記録する
            selectedSprite = this.gameObject.GetComponent<Image>().sprite;

            // フィールドを更新する
            GameObject.Find("GameDirector").GetComponent<GameMainGB>().UpdateGameState(index,this.gameObject.GetComponent<Image>().sprite);

            var alpha = this.gameObject.GetComponent<Image>().color.a;
            while (alpha > 0f)
            {
                alpha -= 0.1f;
                this.gameObject.GetComponent<Image>().color = new Color(255f, 255f, 255f, alpha);
                await Task.Delay(100);
            }
        }
    }
}