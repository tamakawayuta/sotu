using UnityEngine;

/// <summary>
/// ヒント画像をクローズする関数の定義
/// </summary>

namespace JigsawPuzzle
{
    public class BackButtonEventsJP : MonoBehaviour
    {
        public void OnClickBack()
        {
            this.gameObject.SetActive(false);
        }
    }
}