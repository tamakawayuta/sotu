using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���𐔂��J�E���g����
/// </summary> 

namespace Neurasthenia
{
    public class CounterSystems : MonoBehaviour
    {
        // ���𐔂��Ǘ�����
        private int answerAmount = 0;

        private void Awake()
        {
            // ���𐔂���0�ɏ�����
            this.gameObject.GetComponent<Text>().text = 0.ToString();
        }

        // ���𐔂��X�V����
        public void UpdateCount()
        {
            // ���𐔂�1���₷
            this.answerAmount++;

            // �e�L�X�g���X�V����
            this.gameObject.GetComponent<Text>().text = answerAmount.ToString();
        }

        // ���𐔂�5�̔{���ł��邩���肵�Č��ʂ�Ԃ�
        public bool CheckCount()
        {
            return answerAmount % 5 == 0 && answerAmount != 0;
        }

        // ���𐔂̃Q�b�^�[
        public int GetAnswerAmount()
        {
            return this.answerAmount;
        }
    }
}