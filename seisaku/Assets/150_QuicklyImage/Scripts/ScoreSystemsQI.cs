using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�R�A���Ǘ�����
/// </summary>

namespace QuicklyImage
{
    public class ScoreSystemsQI : MonoBehaviour
    {
        // �X�R�A���L�^����
        private int score = 0;

        private void Awake()
        {
            // ������
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = "0";
        }

        // �X�R�A���L�^���e�L�X�g�Ɏ���
        public void AddScore(int addValue)
        {
            this.score += addValue;
            this.gameObject.transform.GetChild(2).GetComponent<Text>().text = this.score.ToString();
        }

        // �X�R�A�̃Q�b�^�[
        public int GetScore()
        {
            return this.score;
        }
    }
}