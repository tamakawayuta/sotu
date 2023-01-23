using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// �񓚂̑I�������Ǘ�����
/// </summary>

namespace HitAndBlow
{
    public class SelectButtonEventsHB : MonoBehaviour
    {
        // ���ƂȂ�摜����
        [SerializeField]
        private Sprite[] images;
        [SerializeField]
        private GameObject detail;

        // ���I������Ă���摜
        private Sprite selectSpriteNow;

        // �I������Ă�����I�u�W�F�N�g
        private GameObject selectNow;

        private void Awake()
        {
            // ���ƂȂ�摜���Z�b�g����
            for (var i = 0; i < images.Length; i++)
            {
                this.gameObject.transform.GetChild(i).GetComponent<Image>().sprite = this.images[i];
                this.gameObject.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

        private void Update()
        {
            // �I������Ă�����I�u�W�F�N�g�̃n�C���C�g���Œ肷��
            if (selectNow != null)
            {
                EventSystem.current.SetSelectedGameObject(selectNow);
                this.gameObject.GetComponent<Image>().enabled = false;
            }
        }

        // OnClick�֐�
        public void OnClickSelect(int cardNum)
        {

            detail.GetComponent<DetailSystemHB>().ApeearDetail();

            // �I�����ꂽ���I�u�W�F�N�g�̃X�v���C�g���L�^����
            this.selectSpriteNow = this.gameObject.transform.GetChild(cardNum).GetComponent<Image>().sprite;

            // �I�����ꂽ���I�u�W�F�N�g���L�^����
            selectNow = this.gameObject.transform.GetChild(cardNum).gameObject;
        }

        // �I������Ă���摜�̃Q�b�^�[
        public Sprite GetSelectSpriteNow()
        {
            return this.selectSpriteNow;
        }
    }
}