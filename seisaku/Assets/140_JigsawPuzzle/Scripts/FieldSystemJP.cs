using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �t�B�[���h�̊Ǘ�
/// </summary>

namespace JigsawPuzzle
{
    public class FieldSystemJP : MonoBehaviour
    {
        // �p�Y���s�[�X
        [SerializeField]
        private GameObject test;

        // �p�Y���s�[�X�̊Ǘ�
        private List<GameObject> fields = new List<GameObject>();

        // �t�B�[���h�̐���
        public void InstantiateFields(int amount)
        {
            var x = -369f;
            var y = 269f;

            for (var i = 0; i < amount; i++)
            {
                // �������Ďq���ɐݒ肷��
                var obj = Instantiate(this.test);
                obj.transform.SetParent(this.gameObject.transform);

                // �傫���ƈʒu��ݒ肷��
                obj.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 0f);
                obj.GetComponent<RectTransform>().localPosition = new Vector3(x,y,0f);

                // �C���f�b�N�X��ݒ肷��
                obj.GetComponent<FieldButtonEventsJP>().SetIndex(i);

                // ���X�g�ŊǗ�����
                fields.Add(obj);

                // ���W�̍X�V
                x += 161f;

                if (x > 275f)
                {
                    y -= 60f;
                    x = -369f;
                }
            }
        }

        // �s�[�X�ɉ摜���Z�b�g����
        public void DrawSprites(List<Sprite> sprites)
        {
            var index = 0;
            foreach (var field in fields)
            {
                field.GetComponent<Image>().sprite = sprites[index];
                index++;
            }
        }

        // �p�Y���s�[�X���Ǘ�����ϐ��̃Q�b�^�[
        public List<GameObject> GetFields()
        {
            return this.fields;
        }
    }
}