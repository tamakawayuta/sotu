using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フィールドの管理
/// </summary>

namespace GetBlock
{
    public class FieldSystemGB : MonoBehaviour
    {
        // コマの画像
        [SerializeField]
        private Sprite[] sprites;

        // フィールドオブジェクトの管理
        private GameObject[] fields;

        private void Awake()
        {
            // フィールドオブジェクトの取得
            fields = new GameObject[this.gameObject.transform.childCount];

            for (var i=0;i<this.gameObject.transform.childCount; i++)
            {
                fields[i] = this.gameObject.transform.GetChild(i).gameObject;
            }

            // フィールドオブジェクトと同数の画像を配列に格納する
            Sprite[] tmpSprites = new Sprite[this.gameObject.transform.childCount];

            for (var i = 0; i < sprites.Length; i++)
            {
                tmpSprites[i] = this.sprites[0];
                tmpSprites[i+5] = this.sprites[1];
                tmpSprites[i+10] = this.sprites[2];
                tmpSprites[i+15] = this.sprites[3];
                tmpSprites[i+20] = this.sprites[4];
            }

            // シャッフルする
            ShuffleImages(tmpSprites);

            // フィールドオブジェクトに対応する画像を格納する
            for (var i = 0; i < tmpSprites.Length; i++)
            {
                this.fields[i].GetComponent<Image>().sprite = tmpSprites[i];
                this.fields[i].GetComponent<Image>().color = Color.gray;

                // ボタンは押せない様に初期化
                this.fields[i].GetComponent<Button>().enabled = false;
            }

            // 四隅のフィールドオブジェクトのみ選択できるようにする
            this.fields[0].GetComponent<Image>().color = Color.white;
            this.fields[4].GetComponent<Image>().color = Color.white;
            this.fields[20].GetComponent<Image>().color = Color.white;
            this.fields[24].GetComponent<Image>().color = Color.white;

            this.fields[0].GetComponent<Button>().enabled = true;
            this.fields[4].GetComponent<Button>().enabled = true;
            this.fields[20].GetComponent<Button>().enabled = true;
            this.fields[24].GetComponent<Button>().enabled = true;
        }

        // フィッシャ―イェーツのシャッフル
        private void ShuffleImages(Sprite[] images)
        {
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        // フィールドオブジェクトのゲッター
        public GameObject GetFields(int index)
        {
            return this.fields[index];
        }
    }
}