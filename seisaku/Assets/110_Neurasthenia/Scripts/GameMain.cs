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
        [SerializeField]
        private GameObject hp;
        [SerializeField]
        private GameObject counter;

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

        private void LateUpdate()
        {
            if (counter.GetComponent<CounterMain>().GetAnswers() % 5 == 0 &&
                counter.GetComponent<CounterMain>().GetAnswers() != 0)
            {

                Invoke("ReloadCards", 0.5f);
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

        private void ReloadCards()
        {
            ColorShuffle();
            DrawColors();
            foreach (var card in cards)
            {
                card.GetComponent<Image>().color = Color.white;
                card.GetComponent<Button>().enabled = true;
            }
        }

        public void SetSelectGameObject(GameObject obj)
        {
            selectGameObjects.Add(obj);

            if (selectGameObjects.Count == 2)
            {
                CheckAnswers();
            }
        }

        private void CheckAnswers()
        {
            clearPanel.SetActive(true);

            if (selectGameObjects[0].GetComponent<CardEvents>().GetCardColor() ==
                selectGameObjects[1].GetComponent<CardEvents>().GetCardColor())
            {
                hp.GetComponent<HpImages>().HealedHp();
                counter.GetComponent<CounterMain>().AddAnswers();
                selectGameObjects.Clear();
            }
            else
            {
                Invoke("RemoveCards", 1.0f);
                hp.GetComponent<HpImages>().DamagedHp();
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
