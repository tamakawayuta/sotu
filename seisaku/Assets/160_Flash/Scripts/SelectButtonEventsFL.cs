using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 選択肢となるボタンのOnClick定義
/// </summary>

namespace Flash
{
    public class SelectButtonEventsFL : MonoBehaviour
    {

        // OnClickの処理
        public void OnClickSelect()
        {
            // 答えの判定を依頼する
            GameObject.Find("GameDirector").GetComponent<CardSystemsFL>().CheckAnswer(this.gameObject.GetComponent<Image>().sprite);

            // 再選択できないようにする
            this.gameObject.GetComponent<Button>().enabled = false;
        }
    }
}