using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[�ɃQ�[���i�s������
/// </summary>

namespace QuicklyImage
{
    public class GuideSystemQI : MonoBehaviour
    {
        private void Awake()
        {
            // �Q�[���̊J�n��`����
            SetText("���[��..!");
        }

        // �󂯎�����e�L�X�g�����̂܂ܕ\������
        public void SetText(string text)
        {
            this.gameObject.GetComponentInChildren<Text>().text = text;
        }
    }
}