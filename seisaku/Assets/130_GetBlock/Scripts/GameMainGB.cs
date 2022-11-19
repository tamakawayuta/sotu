using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GetBlock
{
    public class GameMainGB : MonoBehaviour
    {
        [SerializeField]
        private GameObject time;
        [SerializeField]
        private GameObject collect;
        [SerializeField]
        private GameObject fields;
        [SerializeField]
        private GameObject turn;

        private bool didCallTime = false;
        private bool isFirstPlayerTurn = true;
        private bool[] didSelect =
        {
            false,false,false,false,false,
            false,false,false,false,false,
            false,false,false,false,false,
            false,false,false,false,false,
            false,false,false,false,false,
        };

        private static int finishCount;

        private void Awake()
        {
            finishCount = 0;
        }


        public async void UpdateGameState(int index,Sprite sprite)
        {
            CallCollectSystem(sprite);
            didSelect[index] = true;
            finishCount++;

            if (finishCount == 25)
            {
                collect.GetComponent<CollectSystemGB>().Judge();
            }

            if (didCallTime)
            {
                return;
            }

            didCallTime = true;
            await time.GetComponent<TimeSystemGB>().StartCountdown();


            UpdateField();
            
            if (isFirstPlayerTurn)
            {
                turn.GetComponent<TurnDetailGB>().ShowPlayer2();
            }
            else
            {
                turn.GetComponent<TurnDetailGB>().ShowPlayer1();
            }

            isFirstPlayerTurn = !isFirstPlayerTurn;
            FieldButtonEventGB.selectedSprite = null;
            didCallTime = false;
        }

        private void CallCollectSystem(Sprite sprite)
        {
            collect.GetComponent<CollectSystemGB>().UpdateCollectState(sprite,isFirstPlayerTurn);
        }

        private void UpdateField()
        {
            List<GameObject> shouldUpdateGameObject = new List<GameObject>();

            if (!didSelect[1] && (didSelect[0] || didSelect[2]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(1));
            }

            if (!didSelect[2] && (didSelect[1] || didSelect[3]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(2));
            }

            if (!didSelect[3] && (didSelect[2] || didSelect[4]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(3));
            }

            if (!didSelect[5] && (didSelect[0] || didSelect[10]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(5));
            }

            if (!didSelect[6] &&
                ((didSelect[1] && didSelect[5]) || (didSelect[5] && didSelect[11]) ||
                (didSelect[11] && didSelect[7]) || (didSelect[7] && didSelect[1])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(6));
            }

            if (!didSelect[7] && 
                ((didSelect[2] && didSelect[6]) || (didSelect[6] && didSelect[12]) || 
                (didSelect[12] && didSelect[8]) || (didSelect[8] && didSelect[2])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(7));
            }

            if (!didSelect[8] &&
                ((didSelect[3] && didSelect[7]) || (didSelect[7] && didSelect[13]) ||
                (didSelect[13] && didSelect[9]) || (didSelect[9] && didSelect[3])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(8));
            }

            if (!didSelect[9] && (didSelect[4] || didSelect[14]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(9));
            }

            if (!didSelect[10] && (didSelect[5] || didSelect[15]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(10));
            }

            if (!didSelect[11] &&
                ((didSelect[6] && didSelect[10]) || (didSelect[10] && didSelect[16]) ||
                (didSelect[16] && didSelect[12]) || (didSelect[12] && didSelect[6])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(11));
            }

            if (!didSelect[12] &&
                ((didSelect[7] && didSelect[11]) || (didSelect[11] && didSelect[17]) ||
                (didSelect[17] && didSelect[13]) || (didSelect[13] && didSelect[7])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(12));
            }

            if (!didSelect[13] &&
                ((didSelect[8] && didSelect[12]) || (didSelect[12] && didSelect[18]) ||
                (didSelect[18] && didSelect[14]) || (didSelect[14] && didSelect[8])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(13));
            }

            if (!didSelect[14] && (didSelect[9] || didSelect[19]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(14));
            }

            if (!didSelect[15] && (didSelect[10] || didSelect[20]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(15));
            }

            if (!didSelect[16] &&
                ((didSelect[11] && didSelect[15]) || (didSelect[15] && didSelect[21]) ||
                (didSelect[21] && didSelect[17]) || (didSelect[17] && didSelect[11])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(16));
            }

            if (!didSelect[17] &&
                ((didSelect[12] && didSelect[16]) || (didSelect[16] && didSelect[22]) ||
                (didSelect[22] && didSelect[18]) || (didSelect[18] && didSelect[12])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(17));
            }

            if (!didSelect[18] &&
                ((didSelect[13] && didSelect[17]) || (didSelect[17] && didSelect[23]) ||
                (didSelect[23] && didSelect[19]) || (didSelect[19] && didSelect[13])))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(18));
            }

            if (!didSelect[19] && (didSelect[14] || didSelect[24]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(19));
            }

            if (!didSelect[21] && (didSelect[20] || didSelect[22]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(21));
            }

            if (!didSelect[22] && (didSelect[21] || didSelect[23]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(22));
            }

            if (!didSelect[23] && (didSelect[22] || didSelect[24]))
            {
                shouldUpdateGameObject.Add(fields.GetComponent<FieldSystemGB>().GetFields(23));
            }

            foreach (var field in shouldUpdateGameObject)
            {
                field.GetComponent<Image>().color = Color.white;
                field.GetComponent<Button>().enabled = true;
            }
        }
    }
}