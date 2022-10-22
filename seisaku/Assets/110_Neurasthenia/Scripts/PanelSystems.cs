using UnityEngine;

namespace Neurasthenia
{
    /// <summary>
    /// 選択した2枚のカードの絵柄を見せている間現れるパネル
    /// その間に他のカードを選択できないようにする
    /// </summary>

    [DisallowMultipleComponent]
    public sealed class PanelSystems : MonoBehaviour
    {
        private void Awake()
        {
            //初期設定
            this.gameObject.SetActive(false);
        }

        //SetActiveを切り替える
        public void switchPanelActive()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
    }
}