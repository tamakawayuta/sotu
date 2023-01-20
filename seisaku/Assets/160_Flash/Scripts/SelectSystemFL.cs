using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �I�����ƂȂ�I�u�W�F�N�g�̊Ǘ�
/// </summary>

namespace Flash
{
    public class SelectSystemFL : MonoBehaviour
    {
        // �Ǘ�����ϐ�
        private GameObject[] buttons;

        private void Awake()
        {
            // �I�����I�u�W�F�N�g�̎擾
            this.buttons = new GameObject[this.gameObject.transform.childCount];

            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                this.buttons[i] = this.gameObject.transform.GetChild(i).gameObject;
            }
        }

        // �I�����̉摜��ݒ肷��
        public void SetButtonSprites(List<Sprite> sprites)
        {
            this.gameObject.SetActive(true);

            // �{�^����������悤�ɂ���
            foreach (var button in buttons)
            {
                button.GetComponent<Button>().enabled = true;
            }

            for (var i = 0; i < 8; i++)
            {
                this.buttons[i].GetComponent<Image>().sprite = sprites[i];
            }
        }
    }
}