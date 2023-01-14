using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �t�B�[���h�̊Ǘ�
/// </summary>

namespace GetBlock
{
    public class FieldSystemGB : MonoBehaviour
    {
        // �R�}�̉摜
        [SerializeField]
        private Sprite[] sprites;

        // �t�B�[���h�I�u�W�F�N�g�̊Ǘ�
        private GameObject[] fields;

        private void Awake()
        {
            // �t�B�[���h�I�u�W�F�N�g�̎擾
            fields = new GameObject[this.gameObject.transform.childCount];

            for (var i=0;i<this.gameObject.transform.childCount; i++)
            {
                fields[i] = this.gameObject.transform.GetChild(i).gameObject;
            }

            // �t�B�[���h�I�u�W�F�N�g�Ɠ����̉摜��z��Ɋi�[����
            Sprite[] tmpSprites = new Sprite[this.gameObject.transform.childCount];

            for (var i = 0; i < sprites.Length; i++)
            {
                tmpSprites[i] = this.sprites[0];
                tmpSprites[i+5] = this.sprites[1];
                tmpSprites[i+10] = this.sprites[2];
                tmpSprites[i+15] = this.sprites[3];
                tmpSprites[i+20] = this.sprites[4];
            }

            // �V���b�t������
            ShuffleImages(tmpSprites);

            // �t�B�[���h�I�u�W�F�N�g�ɑΉ�����摜���i�[����
            for (var i = 0; i < tmpSprites.Length; i++)
            {
                this.fields[i].GetComponent<Image>().sprite = tmpSprites[i];
                this.fields[i].GetComponent<Image>().color = Color.gray;

                // �{�^���͉����Ȃ��l�ɏ�����
                this.fields[i].GetComponent<Button>().enabled = false;
            }

            // �l���̃t�B�[���h�I�u�W�F�N�g�̂ݑI���ł���悤�ɂ���
            this.fields[0].GetComponent<Image>().color = Color.white;
            this.fields[4].GetComponent<Image>().color = Color.white;
            this.fields[20].GetComponent<Image>().color = Color.white;
            this.fields[24].GetComponent<Image>().color = Color.white;

            this.fields[0].GetComponent<Button>().enabled = true;
            this.fields[4].GetComponent<Button>().enabled = true;
            this.fields[20].GetComponent<Button>().enabled = true;
            this.fields[24].GetComponent<Button>().enabled = true;
        }

        // �t�B�b�V���\�C�F�[�c�̃V���b�t��
        private void ShuffleImages(Sprite[] images)
        {
            for (var i = images.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = images[i];
                images[i] = images[j];
                images[j] = tmp;
            }
        }

        // �t�B�[���h�I�u�W�F�N�g�̃Q�b�^�[
        public GameObject GetFields(int index)
        {
            return this.fields[index];
        }
    }
}