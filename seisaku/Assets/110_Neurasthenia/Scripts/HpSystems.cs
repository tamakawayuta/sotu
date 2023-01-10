using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

// �K�v
using PublicUI;

/// <summary>
/// HP���Ǘ�����
/// </summary>

namespace Neurasthenia
{
    public class HpSystems : MonoBehaviour
    {
        // �_���[�W���󂯂��摜
        [SerializeField]
        private Sprite damagedHpImage;

        // �ʏ펞�̉摜
        [SerializeField]
        private Sprite normalHpImage;

        // �K�v�ȃI�u�W�F�N�g�̎擾
        [SerializeField]
        private GameObject gameoverUI;
        [SerializeField]
        private GameObject counter;

        // HP���Ǘ����邽�߂̃X�^�b�N
        private Stack<Transform> childHps = new Stack<Transform>();
        private Stack<Transform> damagedHps = new Stack<Transform>();

        // ���̃I�u�W�F�N�g�̎q����HP�I�u�W�F�N�g�̐����Ǘ�����
        private int childAmounts;

        private void Awake()
        {
            // �q�������擾����
            childAmounts = this.gameObject.transform.childCount;

            // �q���̃I�u�W�F�N�g��ʏ�̃X�^�b�N�ɒǉ�����
            for (var i = 0; i < childAmounts; i++)
            {
                childHps.Push(this.gameObject.transform.GetChild(i));
            }

            // �q����HP�I�u�W�F�N�g�����������邽�߁A�z��ɓn��
            Transform[] children = childHps.ToArray();

            foreach (var child in children)
            {
                // �ʏ펞�̉摜���g�p����
                child.gameObject.GetComponent<Image>().sprite = normalHpImage;

                // �F��΂ɂ���
                child.gameObject.GetComponent<Image>().color = Color.green;
            }
        }

        // �_���[�W���󂯂��Ƃ��̏���
        public void DamagedHp()
        {
            // �ʏ�̃X�^�b�N����HP�I�u�W�F�N�g���擾����
            var childHp = childHps.Pop();

            // �擾�����I�u�W�F�N�g���_���[�W���󂯂��摜�ɂ���
            childHp.gameObject.GetComponent<Image>().sprite = damagedHpImage;

            // �F����������
            childHp.gameObject.GetComponent<Image>().color = Color.black;

            //�_���[�W���󂯂��I�u�W�F�N�g���Ǘ�����X�^�b�N�ɒǉ�����
            damagedHps.Push(childHp);

            // HP��0�ɂȂ�����Q�[���I�[�o�[
            if (childHps.Count == 0)
            {
                gameoverUI.GetComponent<GameOverSystems>().AppearGameOverUI(counter.GetComponent<CounterSystems>().GetAnswerAmount());
                return;
            }

            // HP�̐F���X�V����
            UpdateHpColor();
        }

        // HP���񕜂��鏈��
        public void HealedHp()
        {
            // HP�����^���Ȃ珈�����Ȃ�
            // �܂�A�_���[�W�̃X�^�b�N��0�Ȃ�
            if (damagedHps.Count == 0)
            {
                return;
            }

            // �_���[�W�̃X�^�b�N����HP�I�u�W�F�N�g�����o��
            var childHp = damagedHps.Pop();

            // �擾�����I�u�W�F�N�g�̉摜��ʏ�̂��̂ɂ���
            childHp.gameObject.GetComponent<Image>().sprite = normalHpImage;

            // �傫�����g�債�A�񕜂����������o��
            childHp.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1.0f);

            // �傫����߂�
            MinimumHpImage(childHp.gameObject);

            // �ʏ�̃X�^�b�N�ɒǉ�����
            childHps.Push(childHp);

            // HP�̐F���X�V����
            UpdateHpColor();
        }

        // HP�I�u�W�F�N�g�̐F���X�V����
        private void UpdateHpColor()
        {
            // HP�ɉ����đS�̂̐F��ω�������
            // ���̂��߂ɒʏ��HP�I�u�W�F�N�g�����o��
            Transform[] children = childHps.ToArray();

            // �ω�������F���Ǘ�����
            var hpColor = Color.clear;

            // �ω�������F�����肷��
            switch (childHps.Count)
            {
                case 5: // 5�܂���4�Ȃ��
                case 4:
                    hpColor = Color.green;
                    break;
                case 3: // 3�܂���2�Ȃ物�F
                case 2:
                    hpColor = Color.yellow;
                    break;
                case 1: // 1�Ȃ��
                    hpColor = Color.red;
                    break;
                default:
                    Debug.LogError("Illegal Value");
                    break;
            }

            // �ʏ�I�u�W�F�N�g�̐F���X�V����
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }
        }

        // HP�����̑傫���ɖ߂��񓯊��֐�
        private async void MinimumHpImage(GameObject hp)
        {
            // �n���ꂽHP�I�u�W�F�N�g�̑傫�����擾����
            var scaleX = hp.GetComponent<Transform>().localScale.x;
            var scaleY = hp.GetComponent<Transform>().localScale.y;

            while (scaleX > 1.0f)
            {
                // �傫���������k������
                scaleX -= 0.1f;
                scaleY -= 0.1f;
                hp.GetComponent<Transform>().localScale = new Vector3(scaleX, scaleY, 1.0f);

                // �f�B���C�������ăA�j���[�V����
                await Task.Delay(30);
            }
        }
    }
}