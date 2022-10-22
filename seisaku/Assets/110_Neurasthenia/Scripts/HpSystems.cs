using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicUI;
using System.Threading.Tasks;

namespace Neurasthenia
{
    /// <summary>
    /// �̗͂��Ǘ�����I�u�W�F�N�g
    /// </summary>

    [DisallowMultipleComponent]
    public sealed class HpSystems : MonoBehaviour
    {
        [SerializeField, TooltipAttribute("�_���[�W���󂯂�HP�摜������")]
        private Sprite damagedHpImage;
        [SerializeField, TooltipAttribute("�ʏ��Ԃ�HP�摜������")]
        private Sprite normalHpImage;
        [SerializeField, TooltipAttribute("�Q�[���I�����ɕ\������UI������")]
        private GameObject gameoverUI;
        [SerializeField, TooltipAttribute("�X�R�A���L�^����I�u�W�F�N�g������")]
        private GameObject counter;

        //�ʏ��Ԃ�HP�I�u�W�F�N�g���Ǘ�����
        private Stack<Transform> childHps = new Stack<Transform>();
        //�_���[�W���󂯂�HP�I�u�W�F�N�g���Ǘ�����
        private Stack<Transform> damagedHps = new Stack<Transform>();

        //HP��
        private int childAmounts;

        private void Awake()
        {
            //HP���̎擾
            childAmounts = this.gameObject.transform.childCount;

            //HP�I�u�W�F�N�g�̎擾
            for (var i = 0; i < childAmounts; i++)
            {
                childHps.Push(this.gameObject.transform.GetChild(i));
            }

            //HP�I�u�W�F�N�g�̏�����
            Transform[] children = childHps.ToArray();
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().sprite = normalHpImage;
                child.gameObject.GetComponent<Image>().color = Color.green;
            }
        }

        //�_���[�W����
        public void DamagedHp()
        {
            //HP�I�u�W�F�N�g��1���o��
            var childHp = childHps.Pop();
            //�_���[�W���󂯂��C���[�W�ɂ���
            childHp.gameObject.GetComponent<Image>().sprite = damagedHpImage;
            //�F�����ɂ���
            childHp.gameObject.GetComponent<Image>().color = Color.black;

            //HP��0�ɂȂ�����I���̂���UI��\������
            if (childHps.Count == 0)
            {
                gameoverUI.GetComponent<GameOverSystems>().AppearGameOverUI(counter.GetComponent<CounterSystems>().GetAnswerAmount());
                return;
            }

            //�c��HP�ɍ��킹�ĐF�����߂�
            Transform[] children = childHps.ToArray();
            var hpColor = Color.clear;
            //3�ȉ��͉��F�A1�ȉ��͐�
            switch (childHps.Count)
            {
                case 4:
                    hpColor = Color.green;
                    break;
                case 3:
                    hpColor = Color.yellow;
                    break;
                case 2:
                    hpColor = Color.yellow;
                    break;
                case 1:
                    hpColor = Color.red;
                    break;
                default:
                    break;
            }
            //�F�̍X�V
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }

            //�_���[�W���󂯂��I�u�W�F�N�g���i�[
            damagedHps.Push(childHp);
        }

        //�񕜏���
        public async void HealedHp()
        {
            //�̗͂����^���Ȃ疳��
            if (damagedHps.Count == 0)
            {
                return;
            }

            await Task.Delay(30);

            //�_���[�W���󂯂Ă���HP�I�u�W�F�N�g��1���o��
            var childHp = damagedHps.Pop();
            //�摜��ʏ��Ԃ̂��̂ɃZ�b�g
            childHp.gameObject.GetComponent<Image>().sprite = normalHpImage;
            //�摜���ꎞ�I�Ɋg�債�Č��ɖ߂�
            childHp.gameObject.GetComponent<Transform>().localScale = new Vector3(1.5f,1.5f,1.0f);
            MinimizeHpImage(childHp.gameObject);
            //�񕜂���HP�I�u�W�F�N�g���i�[����
            childHps.Push(childHp);

            //�c��HP�ɂ���ĐF�����߂�
            Transform[] children = childHps.ToArray();
            var hpColor = Color.clear;
            switch (childHps.Count)
            {
                case 5:
                    hpColor = Color.green;
                    break;
                case 4:
                    hpColor = Color.green;
                    break;
                case 3:
                    hpColor = Color.yellow;
                    break;
                case 2:
                    hpColor = Color.yellow;
                    break;
                default:
                    break;
            }
            //�F�̍X�V
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }
        }

        private async void MinimizeHpImage(GameObject hpGauge)
        {
            //�n���ꂽ�I�u�W�F�N�g�̃X�P�[�����擾
            var scaleX = hpGauge.GetComponent<Transform>().localScale.x;
            var scaleY = hpGauge.GetComponent<Transform>().localScale.y;

            //����HP�I�u�W�F�N�g�Ɠ����傫���ɂȂ�܂ŏk������
            while (scaleX > 1.0f)
            {
                hpGauge.GetComponent<Transform>().localScale = new Vector3(scaleX,scaleY,1.0f);
                scaleX -= 0.01f;
                scaleY -= 0.01f;
                //�������f�B���C���ďk�����x�𒲐�
                await Task.Delay(20);
            }
        }
    }
}