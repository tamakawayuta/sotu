using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PublicUI;

namespace GetBlock
{
    public class CollectSystemGB : MonoBehaviour
    {
        [SerializeField]
        private GameObject clearUI;

        private GameObject[] collectChildren;

        private void Awake()
        {
            collectChildren = new GameObject[this.gameObject.transform.childCount];

            for (var i = 0; i < this.gameObject.transform.childCount; i++)
            {
                collectChildren[i] = this.gameObject.transform.GetChild(i).gameObject;
            }
        }

        public void UpdateCollectState(Sprite sprite,bool isFirstPlayer)
        {
            var index = 0;

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

            var x = collectChildren[index].transform.localPosition.x;
            var y = collectChildren[index].transform.localPosition.y;

            if (isFirstPlayer)
            {
                y -= 50;
            }
            else
            {
                y += 50;
            }

            collectChildren[index].GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0f);

            if (collectChildren[index].GetComponent<RectTransform>().localPosition.y >= 250)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�܂�");
            }
            else if (collectChildren[index].GetComponent<RectTransform>().localPosition.y <= -250)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("����");
            }
        }

        public void Judge()
        {
            var playerField = 0;
            var enemyField = 0;

            foreach (var field in collectChildren)
            {
                if (field.transform.localPosition.y < 0)
                {
                    playerField++;
                }
                else if (field.transform.localPosition.y > 0)
                {
                    enemyField++;
                }
            }

            if (playerField > enemyField)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("����");
            }
            else if (playerField < enemyField)
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�܂�");
            }
            else
            {
                clearUI.GetComponent<GameOverSystems>().AppearUIOnlyText("�Ђ��킯");
            }
        }
    }
}