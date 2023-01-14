using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ǂ���̔Ԃ��������I�u�W�F�N�g�̊Ǘ�
/// </summary>

namespace GetBlock
{
    public class TurnDetailGB : MonoBehaviour
    {
        private void Awake()
        {
            // ������
            ShowPlayer1();
        }

        // �v���C���[1�̔Ԃɂ���
        public void ShowPlayer1()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        // �v���C���[2�̔Ԃɂ���
        public void ShowPlayer2()
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}