using UnityEngine;

namespace Neurasthenia
{
    /// <summary>
    /// �I������2���̃J�[�h�̊G���������Ă���Ԍ����p�l��
    /// ���̊Ԃɑ��̃J�[�h��I���ł��Ȃ��悤�ɂ���
    /// </summary>

    [DisallowMultipleComponent]
    public sealed class PanelSystems : MonoBehaviour
    {
        private void Awake()
        {
            //�����ݒ�
            this.gameObject.SetActive(false);
        }

        //SetActive��؂�ւ���
        public void switchPanelActive()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
    }
}