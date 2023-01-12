using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �q�b�g�ƃu���[���L�^����
/// </summary>

namespace HitAndBlow
{
    public class RecordTextSystemsHB : MonoBehaviour
    {
        // �q�b�g�ƃu���[���L�^����I�u�W�F�N�g�̊Ǘ�
        private Queue<GameObject> texts = new Queue<GameObject>();

        // ���g���Ă���L�^�I�u�W�F�N�g
        private GameObject textNow;

        private void Awake()
        {
            // �L�^�I�u�W�F�N�g�̏������Ǝ擾
            for (var i = 0; i < 8; i++)
            {
                this.gameObject.transform.GetChild(i).GetComponent<Text>().text = "";
                texts.Enqueue(this.gameObject.transform.GetChild(i).gameObject);
            }

            // �g�p����L�^�I�u�W�F�N�g�����o��
            textNow = texts.Dequeue();
        }

        // �q�b�g�ƃu���[�̋L�^
        public void DrawText(int hit, int blow)
        {
            textNow.GetComponent<Text>().text = hit.ToString() + " " + blow.ToString();

            // ���̋L�^�I�u�W�F�N�g�����o��
            textNow = texts.Dequeue();
        }
    }
}