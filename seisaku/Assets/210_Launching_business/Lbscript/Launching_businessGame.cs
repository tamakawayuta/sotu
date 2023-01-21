using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launching_businessGame : MonoBehaviour
{
    int red;//赤旗の状況（上がっていれば１、下がっていれば０）
    int white;//白旗の状況（赤と同じ）
    int rand;//乱数を代入
    int x, y;//マウスの座標
    int check;
    /*↑どの旗が上下したか調べる変数
      赤があげられたら check=0
      赤が下げられたら check=1
      白があげられたら check=2
      白が下げられたら check=3
    */
    int redCheck;
    int whiteCheck;
    int num;//どの旗を上下するよう指示したか調べる変数
    int i;//drawを実行した数
    int orderCount;
    int point;//ポイント
    int scene;//シーンを代入。ゲーム画面は1,スタート・結果画面は0。1になるとゲームスタート！
    int count;//ボタンを押した回数(1回の指示で1回までボタンを押せる

    float limit;
    float timrLimit;
    float orderLimit;

    [SerializeField]
    string[] order = {"赤あげて","赤下げて","白あげて","白下げて",  //日本語
                  /*"赤下げない","赤あげない","白下げない","白あげない",*/
                  "Raise red","Lower white","Raise white","Lower white",  //英語
                  /*"Don't lower red","Don't raise red","Don't lower white","Don't raise red",*/
                  "Lever le drapeau rouge","Abaisser le drapeau rouge","Lever le drapeau blanc","Abaisser le drapeau blanc",  //フランス語
                  /*"Ne baissez pas le drapeau rouge","Ne levez pas le drapeau rouge","Ne baissez pas le drapeau blanc","Ne levez pas le drapeau rouge",*/
                  "舉起紅旗","降下紅旗","高舉白旗","降下白旗",  //中国語
                  /*"不要降低紅旗","不要舉起紅旗","不要降白旗","不要舉白旗",*/
                  "ارفعوا العلم الأحمر","اخفض العلم الأحمر","ارفع الراية البيضاء","اخفض العلم الأبيض",  //アラビア語
                  /*"لا تنزل العلم الأحمر","لا ترفع العلم الأحمر","لا تنزل العلم الأحمر","لا ترفع العلم الأحمر",*/
                  "Levanta la bandera roja","Baja la bandera roja","Levanta la bandera blanca","Baja la bandera blanca",  //スペイン語
                  /*"No bajes la bandera roja","No levantes la bandera roja","No bajes la bandera blanca","No levantes la bandera blanca",*/
                  "Hys die rooi vlag","Laat sak die rooi vlag","Hys die wit vlag","Laat sak die wit vlag",  //アフリカーンス語
                  /*"Moenie die rooi vlag laat sak nie","Moenie die rooi vlag hys nie","Moenie die wit vlag laat sak nie","Moenie die wit vlag opsteek nie",*/
                  "Inua bendera nyekundu","Usishushe bendera nyekundu","Inua bendera nyeupe","Punguza bendera nyeupe",  //スワヒリ語
                  /*"Usishushe bendera nyekundu","Usipandishe bendera nyekundu","Usishushe bendera nyeupe","Usipandishe bendera nyeupe",*/
                  "Levante a bandeira vermelha","Abaixe a bandeira vermelha","Levante a bandeira branca","Abaixe a bandeira branca",  //ポルトガル語
                  /*"Não abaixe a bandeira vermelha","Não levante a bandeira vermelha","Não abaixe a bandeira branca","Não levante a bandeira branca",*/
                  "붉은 깃발을 올려","붉은 깃발을 내리고","흰 깃발을 올려","흰 깃발을 내리고",  //韓国語
                  /*"붉은 깃발을 낮추지 마십시오","붉은 깃발을 올리지 마십시오","백기를 낮추지 마십시오","백기를 올리지 마십시오"*/ };

    [SerializeField]
    private GameObject strat;
    [SerializeField]
    private GameObject mainGame;
    [SerializeField]
    private GameObject[] flag;
    [SerializeField]
    private GameObject orderText;
    [SerializeField]
    private Text ordText;

    [SerializeField]
    public Text[] score;

    [SerializeField]
    private Text timeL;


    bool isActive = true;


    void draw()
    {
        if (scene == 0)
        {  //スタートボタンが押される前の処理
            strat.SetActive(true);//スタートボタンの配置
            point = 0;              //ポイントリセット
        }
        if (scene == 1)
        {  //スタートボタンが押されたあとの処理。ゲームスタート！
            limit -= Time.deltaTime;
            if(limit < 0)
            {
                seconds();
                limit = 2;
            }
            orderLimit -= Time.deltaTime;
            if(orderLimit < 0)
            {
                orderCount += 8;
                orderLimit = 15;
            }
            //Invoke("seconds",0);
            if (red == 1)
            {  //赤旗を上げるボタンが押されたら
                flag[2].SetActive(false);
                flag[0].SetActive(true);//赤旗を表示
            }
            if (red == 0)
            {  //赤旗を下げるボタンが押されたら
                flag[0].SetActive(false);//赤旗を消す
                flag[2].SetActive(true);
            }
            if (white == 1)
            {//白旗を上げるボタンが押されたら
                flag[3].SetActive(false);
                flag[1].SetActive(true);//白旗を表示
            }
            if (white == 0)
            {//白旗を下げるボタンが押されたら
                flag[1].SetActive(false);//白旗を消す
                flag[3].SetActive(true);
            }
            score[1].text = "" + point;

            mainGame.SetActive(true);
            timrLimit -= Time.deltaTime;
            //Debug.Log(timrLimit);
            if (timrLimit < 0)
            {    //ゲーム終了時を表す
                finish();       //finish()へ
            }
            timeL.text = (int)timrLimit + "秒";

        }
    }

    void seconds()
    {//2秒ごとに実行する
                count = 1;          //押せるボタンの数を1回に戻す

        point_count();
        order_text();       //order_text()へ
    }

    void order_text()
    {        //旗の上下の指示するテキストを表示させる
        rand = Random.Range(0, orderCount);     //randに乱数を代入
        ordText.text = "" + order[rand];
    }

    void Active()
    {
        isActive = !isActive;
    }

    void finish()
    {            //ゲーム終了時に実行
        scene = 0;              //変数startを0にしスタートボタンが押される状態の前に戻す
        red = 0;                //赤旗の状況リセット
        white = 0;              //白旗の状況リセット
        i = 0;                  //drawに回数リセット
        orderCount = 4;
        orderLimit = 15;
        timrLimit = 60;

        mainGame.SetActive(false);
        strat.SetActive(true);
        score[0].text = "" + point;  //「スコア」の下にpointを表示
    }

    public void sButton()
    {
        strat.SetActive(false);          //スタートボタンを消す
        scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
    }

    public void whiteUpButton()
    {   //白旗を上げるボタン  
            white = 0;
            check = 2;
            point_count();
    }

    public void whitDownButton()
    {   //白旗を下げるボタン
            white = 1;
            check = 3;
            point_count();
    }

    public void redUpButton()
    {   //赤旗を上げるボタン
            red = 0;
            check = 0;
            point_count();
    }

    public void redDownButton()
    {   //赤旗を下げるボタン
            red = 1;
            check = 1;
            point_count();
    }

    void point_count()
    {  //旗上下ボタン押した後の正誤判定
        if (count == 1)
        {
            num = rand % 4;            //randを4で割ったときの余りを代入(この数が配列orderと対応してい]
            Debug.Log(num);
            if(num <= 1)
            {
                //num -= 1;
                Debug.Log("red:" + red);
                if (num == red)
                {          //変数checkとnumが同じとき実行(指示と旗の状態が同じとき）
                    point += 10;             //ポイントを+10
                }  
            }
            else
            {
                num -= 2;
                Debug.Log("white:" + white) ;
                if (num == white)
                {
                    point += 10;
                }
            }
            count = 0;                //押せるボタンの数を1回に戻す
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        orderCount = 4;
        orderLimit = 15;
        timrLimit = 60;
        scene = 0;
        score[0].text = "" + point;
        point = 0;
        limit = 2;
        red = 0;

        GameObject target1 = GameObject.Find("Red_flag");
        GameObject target2 = GameObject.Find("White_flag");
    }

    // Update is called once per frame
    void Update()
    {
        draw();

        point_count();
    }
}
