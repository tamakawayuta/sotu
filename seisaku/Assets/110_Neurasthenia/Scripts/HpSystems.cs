using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neurasthenia
{
    public class HpSystems : MonoBehaviour
    {
        [SerializeField]
        private Sprite damagedHpImage;
        [SerializeField]
        private Sprite normalHpImage;

        private Stack<Transform> childHps = new Stack<Transform>();
        private Stack<Transform> damagedHps = new Stack<Transform>();

        private int childAmounts;

        private void Awake()
        {
            childAmounts = this.gameObject.transform.childCount;

            for (var i = 0; i < childAmounts; i++)
            {
                childHps.Push(this.gameObject.transform.GetChild(i));
            }

            Transform[] children = childHps.ToArray();
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().sprite = normalHpImage;
                child.gameObject.GetComponent<Image>().color = Color.green;
            }
        }

        public void DamagedHp()
        {
            if (childHps.Count == 0)
            {
                Debug.Log("GameOver");
                return;
            }

            var childHp = childHps.Pop();
            childHp.gameObject.GetComponent<Image>().sprite = damagedHpImage;
            childHp.gameObject.GetComponent<Image>().color = Color.black;

            Transform[] children = childHps.ToArray();
            var hpColor = Color.clear;
            switch (childHps.Count)
            {
                case 2:
                    hpColor = Color.yellow;
                    break;
                case 1:
                    hpColor = Color.red;
                    break;
            }
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }
 
            damagedHps.Push(childHp);
        }

        public void HealedHp()
        {
            if (damagedHps.Count == 0)
            {
                return;
            }

            var childHp = damagedHps.Pop();
            childHp.gameObject.GetComponent<Image>().sprite = normalHpImage;
            childHps.Push(childHp);

            Transform[] children = childHps.ToArray();
            var hpColor = Color.clear;
            switch (childHps.Count)
            {
                case 3:
                    hpColor = Color.green;
                    break;
                case 2:
                    hpColor = Color.yellow;
                    break;
            }
            foreach (var child in children)
            {
                child.gameObject.GetComponent<Image>().color = hpColor;
            }
        }

    }
}