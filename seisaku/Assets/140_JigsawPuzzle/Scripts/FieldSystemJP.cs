using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フィールドの管理
/// </summary>

namespace JigsawPuzzle
{
    public class FieldSystemJP : MonoBehaviour
    {
        // パズルピース
        [SerializeField]
        private GameObject test;

        // パズルピースの管理
        private List<GameObject> fields = new List<GameObject>();

        // フィールドの生成
        public void InstantiateFields(int amount)
        {
            var x = -369f;
            var y = 269f;

            for (var i = 0; i < amount; i++)
            {
                // 生成して子供に設定する
                var obj = Instantiate(this.test);
                obj.transform.SetParent(this.gameObject.transform);

                // 大きさと位置を設定する
                obj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0f);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0f);

                // インデックスを設定する
                obj.GetComponent<FieldButtonEventsJP>().SetIndex(i);

                // リストで管理する
                fields.Add(obj);

                // 座標の更新
                x += 161f;

                if (x > 275f)
                {
                    y -= 60f;
                    x = -369f;
                }
            }
        }

        // ピースに画像をセットする
        public void DrawSprites(List<Sprite> sprites)
        {
            var index = 0;
            foreach (var field in fields)
            {
                field.GetComponent<Image>().sprite = sprites[index];
                index++;
            }
        }

        // パズルピースを管理する変数のゲッター
        public List<GameObject> GetFields()
        {
            return this.fields;
        }
    }
}