using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class FieldManagerCH : MonoBehaviour
    {
        [SerializeField]
        private GameObject fieldPrefab;

        private List<GameObject> fields = new List<GameObject>();

        private void Awake()
        {
            var PosX = -240f;
            var PosY = 160f;

            while (this.gameObject.transform.childCount != 35)
            {
                GameObject tmp = Instantiate(fieldPrefab, new Vector3(), Quaternion.identity);
                tmp.transform.SetParent(this.gameObject.transform);
                tmp.GetComponent<RectTransform>().localPosition = new Vector3(PosX, PosY, 0f);
                tmp.GetComponent<FieldEventsCH>().SetIndex(this.gameObject.transform.childCount - 1);

                PosX += 80f;
                if (PosX > 240f)
                {
                    PosY -= 80f;
                    PosX = -240f;
                }
            }

            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                fields.Add(this.gameObject.transform.GetChild(i).gameObject);
            }
        }

        public GameObject GetFields(int index)
        {
            return this.fields[index];
        }
    }
}