using UnityEngine;

/// <summary>
/// �s�����̂Ƃ��A�J�[�h�̑I���𐧌����邽�߂̓����ȃp�l��
/// </summary>

namespace Neurasthenia
{
    public class PanelSystems : MonoBehaviour
    {
        private void Awake()
        {
            // ������
            this.gameObject.SetActive(false);
        }

        // �p�l���̃Z�b�g�A�N�e�B�u��؂�ւ���
        public void switchPanelActive()
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }
    }
}