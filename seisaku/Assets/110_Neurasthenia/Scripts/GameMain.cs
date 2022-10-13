using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class GameMain : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] cards;
        [SerializeField]
        private GameObject clearPanel;

        private Color[] colors = {
            Color.red, Color.red,
            Color.blue, Color.blue,
            Color.green, Color.green,
            Color.yellow, Color.yellow,
            Color.magenta, Color.magenta 
        };

        private List<GameObject> selectGameObjects = new List<GameObject>();

        private void Awake()
        {
            clearPanel.SetActive(false);

            ColorShuffle();
            DrawColors();
        }

        private void Update()
        {
            if (selectGameObjects.Count == 2)
            {
                CheckAnswers();
            }
        }

        private void ColorShuffle()
        {
            for (var i = colors.Length - 1; i > 0; --i)
            {
                var j = Random.Range(0, i + 1);
                var tmp = colors[i];
                colors[i] = colors[j];
                colors[j] = tmp;
            }
        }

        private void DrawColors()
        {
            int i = 0;

            foreach (var card in cards)
            {
                card.GetComponent<CardEvents>().SetCardColor(colors[i]);
                i++;
            }
        }

        public void SetSelectGameObject(GameObject obj)
        {
            selectGameObjects.Add(obj);
        }

        private void CheckAnswers()
        {
            clearPanel.SetActive(true);

            if (selectGameObjects[0].GetComponent<CardEvents>().GetCardColor() ==
                selectGameObjects[1].GetComponent<CardEvents>().GetCardColor())
            {
                Debug.Log("A");
                selectGameObjects.Clear();
            }
            else
            {
                Debug.Log("B");
                Invoke("RemoveCards", 1.0f);
            }

            clearPanel.SetActive(false);
        }

        private void RemoveCards()
        {
            foreach (var obj in selectGameObjects)
            {
                obj.GetComponent<Image>().color = Color.white;
                obj.GetComponent<Button>().enabled = true;
            }
            selectGameObjects.Clear();
        }
    }
}
