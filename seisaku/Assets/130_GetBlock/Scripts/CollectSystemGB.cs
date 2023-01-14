using UnityEngine;
using UnityEngine.UI;

// �K�v
using PublicUI;

/// <summary>
/// �Q�[���󋵂������I�u�W�F�N�g�̒�`
/// </summary>

namespace GetBlock
{
    public class CollectSystemGB : MonoBehaviour
    {
        // �K�v�Ȃ��̂̎擾
        [SerializeField]
        private GameObject clearUI;
        [SerializeField]
        private GameObject text;

        // �󋵃I�u�W�F�N�g�̊Ǘ�
        private GameObject[] collectChildren;

        private void Awake()
        {
            // �󋵃I�u�W�F�N�g�̎擾
            collectChildren = new GameObject[this.gameObject.transform.childCount];

            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                collectChildren[i] = this.gameObject.transform.GetChild(i).gameObject;
            }
        }

        // �󋵃I�u�W�F�N�g�̍X�V
        public void UpdateCollectState(Sprite sprite,bool isFirstPlayer)
        {
            // �ǂ̃I�u�W�F�N�g���X�V���邩�Ǘ�
            var index = 0;

            // �󂯎�����X�v���C�g����X�V����I�u�W�F�N�g�����肷��
            if (sprite == collectChildren[0].GetComponent<Image>().sprite)
            {
                index = 0;
            }
            else if (sprite == collectChildren[1].GetComponent<Image>().sprite)
            {
                index = 1;
            }
            else if (sprite == collectChildren[2].GetComponent<Image>().sprite)
            {
                index = 2;
            }
            else if (sprite == collectChildren[3].GetComponent<Image>().sprite)
            {
                index = 3;
            }
            else if (sprite == collectChildren[4].GetComponent<Image>().sprite)
            {
                index = 4;
            }

            // �X�V����I�u�W�F�N�g�̈ʒu���擾
            var x = collectChildren[index].transform.localPosition.x;
            var y = collectChildren[index].transform.localPosition.y;

            // ���ƌ��őΉ����镪������
            if (isFirstPlayer)
            {
                y -= 40;
            }
            else
            {
                y += 40;
            }

            // �ʒu���X�V����
            collectChildren[index].GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0f);

            // �X�R�A���X�V����
            this.text.GetComponent<StateTextSystemGB>().UpdateText(collectChildren);

            // ���s�̔���
            if (collectChildren[index].GetComponent<RectTransform>().localPosition.y >= 160)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�v���C���[2�̂���");
            }
            else if (collectChildren[index].GetComponent<RectTransform>().localPosition.y <= -240)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�v���C���[1�̂���");
            }
        }

        // ���s����
        // ������Ȃ����Ƃ��ɌĂяo�����
        public void Judge()
        {
            // �X�R�A�̊Ǘ�
            var playerField = 0;
            var enemyField = 0;

            foreach (var field in collectChildren)
            {
                // �ʒu�ɂ��X�R�A�����Z
                if (field.transform.localPosition.y < 0)
                {
                    playerField++;
                }
                else if (field.transform.localPosition.y > 0)
                {
                    enemyField++;
                }
            }

            // �X�R�A���傫�����̏���
            if (playerField > enemyField)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�v���C���[1�̂���");
            }
            else if (playerField < enemyField)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�v���C���[2�̂���");
            }
            else
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�Ђ��킯");
            }
        }
    }
}