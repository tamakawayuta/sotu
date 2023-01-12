using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ヒットとブローを記録する
/// </summary>

namespace HitAndBlow
{
    public class RecordTextSystemsHB : MonoBehaviour
    {
        // ヒットとブローを記録するオブジェクトの管理
        private Queue<GameObject> texts = new Queue<GameObject>();

        // 今使われている記録オブジェクト
        private GameObject textNow;

        private void Awake()
        {
            // 記録オブジェクトの初期化と取得
            for (var i = 0; i < 8; i++)
            {
                this.gameObject.transform.GetChild(i).GetComponent<Text>().text = "";
                texts.Enqueue(this.gameObject.transform.GetChild(i).gameObject);
            }

            // 使用する記録オブジェクトを取り出す
            textNow = texts.Dequeue();
        }

        // ヒットとブローの記録
        public void DrawText(int hit, int blow)
        {
            textNow.GetComponent<Text>().text = hit.ToString() + " " + blow.ToString();

            // 次の記録オブジェクトを取り出す
            textNow = texts.Dequeue();
        }
    }
}