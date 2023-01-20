using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 選択肢となるオブジェクトの管理
/// </summary>

namespace Flash
{
    public class SelectSystemFL : MonoBehaviour
    {
        // 管理する変数
        private GameObject[] buttons;

        private void Awake()
        {
            // 選択肢オブジェクトの取得
            this.buttons = new GameObject[this.gameObject.transform.childCount];

            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                this.buttons[i] = this.gameObject.transform.GetChild(i).gameObject;
            }
        }

        // 選択肢の画像を設定する
        public void SetButtonSprites(List<Sprite> sprites)
        {
            this.gameObject.SetActive(true);

            // ボタンを押せるようにする
            foreach (var button in buttons)
            {
                button.GetComponent<Button>().enabled = true;
            }

            for (var i = 0; i < 8; i++)
            {
                this.buttons[i].GetComponent<Image>().sprite = sprites[i];
            }
        }
    }
}