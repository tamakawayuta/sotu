using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���𐔂̊Ǘ�
/// </summary>

namespace Flash
{
    public class CountSystemFL : MonoBehaviour
    {
        // ���𐔂��L�^����ϐ�
        private int count = 0;

        // ���𐔂��J�E���g����
        public void AddCount()
        {
            this.count++;
            UpdateCountText();
        }

        // ���𐔂̃Q�b�^�[
        public int GetCount()
        {
            return this.count;
        }

        // ���𐔂��e�L�X�g�ɔ��f����
        private void UpdateCountText()
        {
            this.gameObject.GetComponent<Text>().text = this.count.ToString();
        }
    }
}