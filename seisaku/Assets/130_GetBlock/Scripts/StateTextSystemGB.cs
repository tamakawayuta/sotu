using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�R�A�̊Ǘ�
/// </summary>

namespace GetBlock
{
    public class StateTextSystemGB : MonoBehaviour
    {
        // �e�v���C���[�̃X�R�A�̊Ǘ�
        private int player1Point;
        private int player2Point;

        private void Awake()
        {
            // ������
            this.player1Point = 0;
            this.player2Point = 0;
        }

        // �X�R�A���X�V����
        public void UpdateText(GameObject[] collects)
        {
            // ��x0�ɏ���������
            this.player1Point = 0;
            this.player2Point = 0;

            // �󋵃I�u�W�F�N�g��y�ʒu����Ή�����ϐ��ɃJ�E���g
            foreach (var child in collects)
            {
                if (child.GetComponent<RectTransform>().localPosition.y >= 0)
                {
                    player2Point++;
                }
                else if (child.GetComponent<RectTransform>().localPosition.y <= -80)
                {
                    player1Point++;
                }
            }

            // �e�L�X�g�ɔ��f
            this.gameObject.transform.GetChild(1).GetComponent<Text>().text = this.player2Point.ToString();
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = this.player1Point.ToString();
        }
    }
}