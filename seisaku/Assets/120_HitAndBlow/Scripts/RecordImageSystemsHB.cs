using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ユーザーが入力した画像を管理する
/// </summary>

namespace HitAndBlow
{
    public class RecordImageSystemsHB : MonoBehaviour
    {
        // 必要なものの取得
        [SerializeField]
        private GameObject selectButton;

        // 今使われている入力オブジェクト
        private GameObject imageNow;

        // 回答を受け付けるゲームオブジェクトの取得
        private Queue<GameObject> images = new Queue<GameObject>();

        // 入力された画像の管理
        private Sprite[] selectedSprite = new Sprite[4];
        
        // 全て回答が入力されたか管理
        private bool isImageSetFull = false;

        private void Awake()
        {
            // 入力オブジェクトのボタンを押せない様に初期化
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    this.gameObject.transform.GetChild(i).transform.GetChild(j).GetComponent<Button>().enabled = false;
                }
            }

            // 入力オブジェクトの取得
            for (var i = 0; i < 8; i++)
            {
                images.Enqueue(this.gameObject.transform.GetChild(i).gameObject);
            }

            // 使用する入力オブジェクトを取り出す
            imageNow = images.Dequeue();

            for (var i = 0; i < 4; i++)
            {
                this.imageNow.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

        private void Update()
        {
            // ユーザーが画像を選択しているなら入力を受け付ける
            if (selectButton.GetComponent<SelectButtonEventsHB>().GetSelectSpriteNow() != null)
            {
                for (var i = 0; i < 4; i++)
                {
                    imageNow.transform.GetChild(i).GetComponent<Button>().enabled = true;
                }
            }
        }

        // 入力オブジェクトのOnClick関数
        public void OnClickRecord(int index)
        {
            // 対応する入力オブジェクトのスプライトに選択されたスプライトを入れる
            imageNow.transform.GetChild(index).GetComponent<Image>().sprite = selectButton.GetComponent<SelectButtonEventsHB>().GetSelectSpriteNow();
            
            // 入力されたスプライトを記録する
            selectedSprite[index] = imageNow.transform.GetChild(index).GetComponent<Image>().sprite;

            // 色を戻す
            imageNow.transform.GetChild(index).GetComponent<Image>().color = Color.white;

            // 回答が全て入力されたか確認する
            SetIsImageSetFull();
        }

        // リセット関数
        public void SetImageNow()
        {
            // 入力された画像を記録する配列をリセット
            selectedSprite = new Sprite[4];

            // 入力済みの古い入力オブジェクトを無効化
            SetActiveImageNow(false);

            // 新しい入力オブジェクトを取得
            imageNow = images.Dequeue();

            // 新しい入力オブジェクトの入力を有効化
            SetActiveImageNow(true);

            for (var i = 0; i < 4; i++)
            {
                this.imageNow.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }

            // フラグをリセット
            this.isImageSetFull = false;
        }

        // 入力された画像配列のゲッター
        public Sprite[] GetSelectedSprites()
        {
            return this.selectedSprite;
        }

        // フラグのゲッター
        public bool GetIsImageSetFull()
        {
            return this.isImageSetFull;
        }

        // フラグの判定
        private void SetIsImageSetFull()
        {
            for (var i = 0; i < 4; i++)
            {
                // 有効化された入力オブジェクトの全てに回答が入力されていないならフラグは立たない
                if (imageNow.transform.GetChild(i).GetComponent<Image>().sprite == null)
                {
                    isImageSetFull = false;
                    return;
                }
            }

            isImageSetFull = true;
        }

        // 入力オブジェクトの有効化と無効化
        private void SetActiveImageNow(bool val)
        {
            for (var i = 0; i < 4; i++)
            {
                imageNow.transform.GetChild(i).GetComponent<Button>().enabled = val;
            }
        }
    }
}