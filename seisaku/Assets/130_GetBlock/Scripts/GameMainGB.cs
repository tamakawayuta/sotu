using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using SoundManager;

/// <summary>
/// ゲームの定義
/// </summary>

namespace GetBlock
{
    public class GameMainGB : MonoBehaviour
    {
        // 必要なものの取得
        [SerializeField]
        private GameObject time;
        [SerializeField]
        private GameObject collect;
        [SerializeField]
        private GameObject fields;
        [SerializeField]
        private GameObject turn;
        [SerializeField]
        private GameObject showTurn;
        [SerializeField]
        private GameObject sound;

        // 何かのフィールドオブジェクトが選択されているかどうかの管理
        private bool didCallTime = false;

        // 手番の管理
        private bool isFirstPlayerTurn = true;

        // 選ばれたフィールドオブジェクトの管理
        private bool[] didSelect =
        {
            false,false,false,false,false,
            false,false,false,false,false,
            false,false,false,false,false,
            false,false,false,false,false,
            false,false,false,false,false,
        };

        // 選択された駒の数管理
        private static int finishCount;

        private void Awake()
        {
            // 初期化
            finishCount = 0;
        }

        // 選択可能なフィールドオブジェクトの更新
        public async void UpdateGameState(int index,Sprite sprite)
        {
            // 状況オブジェクトの更新
            CallCollectSystem(sprite);

            // 選択されたフィールドオブジェクトに対応するフラグを更新
            didSelect[index] = true;

            // カウントする
            finishCount++;

            // 全ての駒が選択されたら勝敗判定をする
            if (finishCount == 25)
            {
                collect.GetComponent<CollectSystemGB>().Judge();
            }

            // この関数が2度以上呼ばれている場合はここで処理を終える
            if (didCallTime)
            {
                return;
            }

            // 1度目ならフラグを立てる
            didCallTime = true;

            // 制限時間が過ぎるまで待機する
            await time.GetComponent<TimeSystemGB>().StartCountdown();

            while (true)
            {
                if (Time.timeScale == 1)
                {
                    break;
                }
                await Task.Delay(2000);
            }

            // 選択できる駒を更新する
            UpdateField();

            sound.GetComponent<SoundSystems>().PlaySE(1);
            
            // 次の手番にする
            if (isFirstPlayerTurn)
            {
                turn.GetComponent<TurnDetailGB>().ShowPlayer2();
            }
            else
            {
                turn.GetComponent<TurnDetailGB>().ShowPlayer1();
            }

            showTurn.GetComponent<ShowTurnUIEvents>().SetTurnSprite();

            // フラグを更新する
            isFirstPlayerTurn = !isFirstPlayerTurn;

            // 変数のリセット
            FieldButtonEventGB.selectedSprite = null;
            didCallTime = false;
        }

        // 状況オブジェクト更新のラッパー
        private void CallCollectSystem(Sprite sprite)
        {
            collect.GetComponent<CollectSystemGB>().UpdateCollectState(sprite,isFirstPlayerTurn);
        }

        // hack: 適切なアルゴリズムがあるはず
        // 選択できる駒を更新する
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