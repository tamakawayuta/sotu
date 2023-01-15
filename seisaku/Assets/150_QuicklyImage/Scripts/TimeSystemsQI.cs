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
        private void Awake()
        {
            // ������
            this.gameObject.GetComponent<Image>().fillAmount = 0f;
        }

        // �J�E���g�_�E���̏���
        public async void StartCountdown(int delayTime)
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.GetComponent<Image>().fillAmount = 1f;

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

            // ���̃I�u�W�F�N�g�̉摜���A�N�e�B�u�Ȃ�Q�[���I�����̃E�B���h�E��\��
            // �܂�A�������ԂȂ��ɑI������I�΂Ȃ������Ƃ�
            if (this.gameObject.GetComponent<Image>().enabled)
            {
                GameObject.Find("GameDirector").GetComponent<CardSystemsQI>().AppearClearUI();
            }
        }
    }
}