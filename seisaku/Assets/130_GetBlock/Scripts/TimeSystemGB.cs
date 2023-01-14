using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������Ԃ̊Ǘ�
/// </summary>

namespace GetBlock
{
    public class TimeSystemGB : MonoBehaviour
    {
        private void Awake()
        {
            // ������
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        // �J�E���g�_�E�����s��
        public async Task StartCountdown()
        {
            // �Q�[�W���t���ɂ���
            this.gameObject.GetComponent<Image>().fillAmount = 1f;

            // ��ɂȂ�܂ŏ��X�ɃQ�[�W�����炷
            while (this.gameObject.GetComponent<Image>().fillAmount > 0)
            {
                this.gameObject.GetComponent<Image>().fillAmount -= 0.05f;
                await Task.Delay(100);
            }
        }
    }
}