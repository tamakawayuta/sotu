using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HitAndBlow
{
    public class RecordTextSystemsHB : MonoBehaviour
    {
        private Queue<GameObject> texts = new Queue<GameObject>();
        private GameObject textNow;

        private void Awake()
        {
            for (var i = 0; i < 8; i++)
            {
                this.gameObject.transform.GetChild(i).GetComponent<Text>().text = "";
                texts.Enqueue(this.gameObject.transform.GetChild(i).gameObject);
            }
            textNow = texts.Dequeue();
        }

        public void DrawText(int hit, int blow)
        {
            textNow.GetComponent<Text>().text = hit.ToString() + " ";

            if (blow - hit > 0)
            {
                var realBlow = blow - hit;

                textNow.GetComponent<Text>().text += realBlow.ToString();
            }
            else
            {
                textNow.GetComponent<Text>().text += "0";
            }

            textNow = texts.Dequeue();
        }
    }
}