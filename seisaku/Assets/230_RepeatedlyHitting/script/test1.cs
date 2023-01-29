using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SoundManager;

public class test1 : MonoBehaviour
{
    [SerializeField]
    private GameObject sound;
    public Slider hpSlider1;
    public Slider hpSlider2;
    public Text hpText1;
    public Text hpText2;
    public Text playerText1;
    public Text playerText2;

    private int divideHp;

    private float maxHp1;
    private float nowHp1;
    public int hp1;

    private float maxHp2;
    private float nowHp2;
    public int hp2;

    int scene;//シーンを代入。ゲーム画面は1,スタートは0。1になるとゲームスタート！

    float timrLimit;
    [SerializeField]
    private Text timeL;


    [SerializeField]
    private GameObject strat;
    [SerializeField]
    private GameObject mainGame;
    [SerializeField]
    private GameObject result;

    float waitTime;

    float orderLimit;
    float limit;
    float difficultyLimit;


    [SerializeField]
    private GameObject[] stageC;

    private int[] orderNum = {/*32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50
            ,51,52,53,54,55,56,57,58,59,60,61,62,63,64,91,92,93,94,95,96,*/97,98,99,100
            ,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120
            ,121,122/*,123,124,125*/};
    [SerializeField]
    private Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        

        strat.SetActive(false);
        mainGame.SetActive(false);
        result.SetActive(false);
        maxHp1 = 100f;
        nowHp1 = 50f;
        hp1 = 50;

        maxHp2 = 100f;
        nowHp2 = 50f;
        hp2 = 50;


        //スライダーの最大値の設定
        hpSlider1.maxValue = maxHp1;

        //スライダーの最大値の設定
        hpSlider2.maxValue = maxHp2;

        scene = 0;
        orderLimit = 2;
        timrLimit = 60;
        Difficulty(0);
    }

    // Update is called once per frame
    void Update()
    {
        //スライダーの現在値の設定
        hpSlider1.value = nowHp1;
        //スライダーの現在値の設定
        hpSlider2.value = nowHp2;

        hpText1.text = "" + hp1;
        hpText2.text = "" + hp2;

        game();

        KeyCode PlayerAttack1 = (KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[0]);
        KeyCode PlayerAttack2 = (KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[21]);

        KeyCode Playerrecovery1 = (KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[15]);
        KeyCode Playerrecovery2 = (KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[12]);

        playerText1.text = PlayerAttack1.ToString() + "で相手が-1\n" + Playerrecovery1.ToString() + "で自分が+1\n";
        playerText2.text = PlayerAttack2.ToString() + "で相手が-1\n" + Playerrecovery2.ToString() + "で自分が+1\n";
    }

    public void Shuffle(int[] order)
    {
        for(var i = order.Length -1; i > 0; --i)
        {
            var j = UnityEngine.Random.Range(0, i + 1);

            var tem = order[i];
            order[i] = order[j];
            order[j] = tem;
        }
    }

    void HpSlider1()
    {
        if (nowHp2 > 0)
        {
            if (Input.GetKeyDown((KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[0])))
            {
                nowHp2 -= 1f;
                hp2 -= 1;
            }
        }
        if (Input.GetKeyDown((KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[15])))
        {
            nowHp1 += 1f;
            hp1 += 1;
        }
    }

    void HpSlider2()
    {
        if (nowHp1 > 0)
        {
            if (Input.GetKeyDown((KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[21])))
            {
                nowHp1 -= 1f;
                hp1 -= 1;
            }
        }
        if (Input.GetKeyDown((KeyCode)Enum.ToObject(typeof(KeyCode), orderNum[12])))
        {
            nowHp2 += 1f;
            hp2 += 1;
        }
    }

    IEnumerator LevelDifficulty()
    {
        if (nowHp1 > 0)
        {
            nowHp1 -= 1f;
            hp1 -= 1;
        }
        nowHp2 += 1f;
        hp2 += 1;

        //Debug.Log(nowHp1 + ":" + nowHp2 + ":" + hp1 + ":" + hp2);
        yield return new WaitForSeconds(waitTime);
    }

    public void game()
    {
        if (scene == 0)
        {  //スタートボタンが押される前の処理
            strat.SetActive(true);//スタートボタンの配置
        }
        if (scene == 1)
        {  //スタートボタンが押されたあとの処理。ゲームスタート！

            orderLimit -= Time.deltaTime;
            if (orderLimit  < 0)
            {
                Shuffle(orderNum);
                int x = UnityEngine.Random.Range(2, 5);
                orderLimit = x;
            }

            HpSlider1();
            if (divideHp == 1)
            {
                HpSlider2();
            }

            limit -= Time.deltaTime;
            if (divideHp == 0)
            {
                if (limit < 0)
                {
                    waitTime = 10.0f;
                    StartCoroutine(LevelDifficulty());
                    limit = difficultyLimit;
                }
            }
            mainGame.SetActive(true);
            timrLimit -= Time.deltaTime;
            //Debug.Log(timrLimit);
            if (timrLimit < 0)
            {    //ゲーム終了時を表す\
                mainGame.SetActive(false);
                result.SetActive(true);
                finish();       //finish()へ
            }
            timeL.text = "残り" + (int)timrLimit + "秒";
        }
    }

    public void finish()
    {
        if(divideHp == 0)
        {
            if (nowHp1 > nowHp2)
            {
                resultText.text = "あなたの勝ち！";
                Debug.Log("あなたの勝ち！");
            }

            if (nowHp2 > nowHp1)
            {
                resultText.text = "CPUの勝ち！";
                Debug.Log("CPUの勝ち！");
            }

            if (nowHp2 == nowHp1)
            {
                resultText.text = "引き分け！";
            }
        }

        if (divideHp == 1)
        {
            if (nowHp1 > nowHp2)
            {
                resultText.text = "青の勝ち！";
                Debug.Log("青の勝ち！");
            }

            if (nowHp2 > nowHp1)
            {
                resultText.text = "赤の勝ち！";
                Debug.Log("赤の勝ち！");
            }

            if (nowHp2 == nowHp1)
            {
                resultText.text = "引き分け！";
            }
        }


    }
    public void Continue()
    {
        maxHp1 = 100f;
        nowHp1 = 50f;
        hp1 = 50;

        maxHp2 = 100f;
        nowHp2 = 50f;
        hp2 = 50;

        scene = 0;
        orderLimit = 2;
        timrLimit = 60;
        result.SetActive(false);
        strat.SetActive(true);
    }

    public void sButton()
    {
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
    }

    public void OnePersonStratButton()
    {
        sound.GetComponent<SoundSystems>().PlaySE(0);
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
        divideHp = 0;
    }
    public void TwoPersonStratButton()
    {
        sound.GetComponent<SoundSystems>().PlaySE(0);
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
        divideHp = 1;
    }

    public void Difficulty(int x)
    {

        sound.GetComponent<SoundSystems>().PlaySE(1);
        switch (x)
        {
            case 0:
                difficultyLimit = 0.8f;
                break;
            case 1:
                difficultyLimit = 0.4f;
                break;
            case 2:
                difficultyLimit = 0.1f;
                break;
        }
        //選択時のカラー変更
        stageC[0].GetComponent<Image>().color = Color.white;
        stageC[1].GetComponent<Image>().color = Color.white;
        stageC[2].GetComponent<Image>().color = Color.white;
        switch (x)
        {
            case 0:
                stageC[0].GetComponent<Image>().color = Color.red;
                break;
            case 1:
                stageC[1].GetComponent<Image>().color = Color.red;
                break;
            case 2:
                stageC[2].GetComponent<Image>().color = Color.red;
                break;
        }
    }
}
