using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Neurasthenia
{
    public class HpImages : MonoBehaviour
    {
        private int hp = 3;

        private void Awake()
        {
            this.gameObject.GetComponent<Image>().color = Color.green;
        }

        public void DamagedHp()
        {
            Color newCol = Color.clear;

            switch (hp)
            {
                case 3:
                    newCol = Color.yellow;
                    hp--;
                    break;
                case 2:
                    newCol = Color.red;
                    hp--;
                    break;
                default:
                    Debug.Log("GameOver");
                    break;
            }

            this.gameObject.GetComponent<Image>().color = newCol;
        }

        public void HealedHp()
        {
            Color newCol = Color.clear;

            switch (hp)
            {
                case 1:
                    newCol = Color.yellow;
                    hp++;
                    break;
                case 2:
                    newCol = Color.green;
                    hp++;
                    break;
                default:
                    newCol = Color.green;
                    Debug.Log("OverHeal");
                    break;
            }

            this.gameObject.GetComponent<Image>().color = newCol;
        }
    }
}
