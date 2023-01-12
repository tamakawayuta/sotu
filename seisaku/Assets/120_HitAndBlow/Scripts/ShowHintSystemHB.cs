using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �q�b�g�ƃu���[�̐�����������
/// </summary>

namespace HitAndBlow
{
    public class ShowHintSystemHB : MonoBehaviour
    {
        private void Awake()
        {
            // ������
            this.gameObject.SetActive(false);
        }

        // �q�b�g�ƃu���[�̐�������
        public async void ShowText(string text)
        {
            this.gameObject.SetActive(true);
            this.gameObject.GetComponentInChildren<Text>().text = text;
            await Task.Delay(1500);
            this.gameObject.SetActive(false);
        }
    }
}