using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Flash
{
    public class PraiseSystemFL : MonoBehaviour
    {
        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        public async Task AppearText()
        {
            this.gameObject.SetActive(true);
            await Task.Delay(1500);
            this.gameObject.SetActive(false);
        }
    }
}