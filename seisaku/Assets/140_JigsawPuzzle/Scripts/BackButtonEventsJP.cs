using UnityEngine;
using SoundManager;

/// <summary>
/// ヒント画像をクローズする関数の定義
/// </summary>

namespace JigsawPuzzle
{
    public class BackButtonEventsJP : MonoBehaviour
    {
        [SerializeField]
        private GameObject sound;

        public void OnClickBack()
        {
            sound.GetComponent<SoundSystems>().PlaySE(3);
            this.gameObject.SetActive(false);
        }
    }
}