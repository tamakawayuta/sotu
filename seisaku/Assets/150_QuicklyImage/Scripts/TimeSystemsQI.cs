using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������Ԃ̊Ǘ�
/// </summary>

namespace QuicklyImage
{
    public class TimeSystemsQI : MonoBehaviour
    {
        private bool isTimeUp = true;

        private void Awake()
        {
            // ������
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        // �J�E���g�_�E���̏���
        public async void StartCountdown(int delayTime)
        {
            this.gameObject.GetComponent<Image>().fillAmount = 1f;
            this.isTimeUp = true;

            while (this.gameObject.GetComponent<Image>().fillAmount > 0f)
            {
                // �|�[�Y���Ȃ�ҋ@
                while (Time.timeScale == 0)
                {
                    await Task.Delay(1000);
                }

                this.gameObject.GetComponent<Image>().fillAmount -= 0.05f;

                await Task.Delay(delayTime);
            }

            // �܂�A�������ԂȂ��ɑI������I�΂Ȃ������Ƃ��Q�[���I�����̃E�B���h�E��\��
            if (this.isTimeUp)
            {
                GameObject.Find("GameDirector").GetComponent<CardSystemsQI>().AppearClearUI();
            }
        }

        public void SetIsTimeUp()
        {
            this.isTimeUp = false;
        }
    }
}