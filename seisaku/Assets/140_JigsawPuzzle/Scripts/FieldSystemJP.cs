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
            for (var i = 0; i < amount; i++)
            {
                var obj = Instantiate(this.test);
                obj.transform.SetParent(this.gameObject.transform);
                fields.Add(obj);
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
    }
}