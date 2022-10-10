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

        private Color[] colors = {
            Color.red, Color.red,
            Color.blue, Color.blue,
            Color.green, Color.green,
            Color.yellow, Color.yellow,
            Color.magenta, Color.magenta };

        void Start()
        {
            ColorShuffle();
            DrawColors();
        }

        void Update()
        {

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
                card.GetComponent<CardEvents>().setCardColor(colors[i]);
                i++;
            }
        }
    }
}
