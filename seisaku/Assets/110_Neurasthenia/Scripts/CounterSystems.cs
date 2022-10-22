using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    /// <summary>
    /// �X�R�A���L�^����e�L�X�g
    /// </summary>
    
    [DisallowMultipleComponent]
    public sealed class CounterSystems : MonoBehaviour
    {
        //�X�R�A���L�^����
        private int answerAmount = 0;

        private void Awake()
        {
            //������
            this.gameObject.GetComponent<Text>().text = 0.ToString();
        }

        //�X�R�A�����Z����UI�����Z����
        public void UpdateCount()
        {
            this.answerAmount++;
            this.gameObject.GetComponent<Text>().text = answerAmount.ToString();
        }

        //�S�ăJ�[�h���߂����Ă��邩�̔���
        public bool CheckCount()
        {
            return answerAmount % 5 == 0 && answerAmount != 0;
        }

        //�X�R�A���擾����
        public int GetAnswerAmount()
        {
            return this.answerAmount;
        }
    }
}