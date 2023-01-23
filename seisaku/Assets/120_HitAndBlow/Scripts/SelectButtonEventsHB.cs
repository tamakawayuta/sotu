using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 回答の選択肢を管理する
/// </summary>

namespace HitAndBlow
{
    public class SelectButtonEventsHB : MonoBehaviour
    {
        // 候補となる画像たち
        [SerializeField]
        private Sprite[] images;
        [SerializeField]
        private GameObject detail;

        // 今選択されている画像
        private Sprite selectSpriteNow;

        // 選択されている候補オブジェクト
        private GameObject selectNow;

        private void Awake()
        {
            // 候補となる画像をセットする
            for (var i = 0; i < images.Length; i++)
            {
                this.gameObject.transform.GetChild(i).GetComponent<Image>().sprite = this.images[i];
                this.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

        private void Update()
        {
            // 選択されている候補オブジェクトのハイライトを固定する
            if (selectNow != null)
            {
                EventSystem.current.SetSelectedGameObject(selectNow);
                this.gameObject.GetComponent<Image>().enabled = false;
            }
        }

        // OnClick関数
        public void OnClickSelect(int cardNum)
        {

            detail.GetComponent<DetailSystemHB>().ApeearDetail();

            // 選択された候補オブジェクトのスプライトを記録する
            this.selectSpriteNow = this.gameObject.transform.GetChild(cardNum).GetComponent<Image>().sprite;

            // 選択された候補オブジェクトを記録する
            selectNow = this.gameObject.transform.GetChild(cardNum).gameObject;
        }

        // 選択されている画像のゲッター
        public Sprite GetSelectSpriteNow()
        {
            return this.selectSpriteNow;
        }
    }
}