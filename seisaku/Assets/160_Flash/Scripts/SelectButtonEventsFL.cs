using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �I�����ƂȂ�{�^����OnClick��`
/// </summary>

namespace Flash
{
    public class SelectButtonEventsFL : MonoBehaviour
    {

        // OnClick�̏���
        public void OnClickSelect()
        {
            // �����̔�����˗�����
            GameObject.Find("GameDirector").GetComponent<CardSystemsFL>().CheckAnswer(this.gameObject.GetComponent<Image>().sprite);

            // �đI���ł��Ȃ��悤�ɂ���
            this.gameObject.GetComponent<Button>().enabled = false;
        }
    }
}