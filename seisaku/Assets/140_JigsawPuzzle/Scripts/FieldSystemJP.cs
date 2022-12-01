using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JigsawPuzzle
{
    public class FieldSystemJP : MonoBehaviour
    {
        [SerializeField]
        private GameObject test;

        private List<GameObject> fields = new List<GameObject>();

        public void InstantiateFields(int amount)
        {
            var x = -369f;
            var y = 270f;

            for (var i = 0; i < amount; i++)
            {
                var obj = Instantiate(this.test);
                obj.transform.SetParent(this.gameObject.transform);
                obj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0f);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0f);
                fields.Add(obj);

                x += 161f;

                if (x > 275f)
                {
                    y -= 59f;
                    x = -369f;
                }
            }
        }

        public void DrawSprites(List<Sprite> sprites)
        {
            var index = 0;
            foreach (var field in fields)
            {
                field.GetComponent<Image>().sprite = sprites[index];
                index++;
            }
        }

        public List<GameObject> GetFields()
        {
            return this.fields;
        }
    }
}