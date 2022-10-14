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
    int num;//どの旗を上下するよう指示したか調べる変数
    int i;//drawを実行した数
    int point;//ポイント
    int scene;//シーンを代入。ゲーム画面は1,スタート・結果画面は0。1になるとゲームスタート！
    int count;//ボタンを押した回数(1回の指示で1回までボタンを押せる
    [SerializeField]
    string[] order = {"赤あげて","赤下げて","白あげて","白下げて",  //日本語
                  "赤下げない","赤あげない","白下げない","白あげない",
                  "Raise red","Lower white","Raise white","Lower white",  //英語
                  "Don't lower red","Don't raise red","Don't lower white","Don't raise red",
                  "Lever le drapeau rouge","Abaisser le drapeau rouge","Lever le drapeau blanc","Abaisser le drapeau blanc",  //フランス語
                  "Ne baissez pas le drapeau rouge","Ne levez pas le drapeau rouge","Ne baissez pas le drapeau blanc","Ne levez pas le drapeau rouge",
                  "舉起紅旗","降下紅旗","高舉白旗","降下白旗",  //中国語
                  "不要降低紅旗","不要舉起紅旗","不要降白旗","不要舉白旗",
                  "ارفعوا العلم الأحمر","اخفض العلم الأحمر","ارفع الراية البيضاء","اخفض العلم الأبيض",  //アラビア語
                  "لا تنزل العلم الأحمر","لا ترفع العلم الأحمر","لا تنزل العلم الأحمر","لا ترفع العلم الأحمر",
                  "Levanta la bandera roja","Baja la bandera roja","Levanta la bandera blanca","Baja la bandera blanca",  //スペイン語
                  "No bajes la bandera roja","No levantes la bandera roja","No bajes la bandera blanca","No levantes la bandera blanca",
                  "Hys die rooi vlag","Laat sak die rooi vlag","Hys die wit vlag","Laat sak die wit vlag",  //アフリカーンス語
                  "Moenie die rooi vlag laat sak nie","Moenie die rooi vlag hys nie","Moenie die wit vlag laat sak nie","Moenie die wit vlag opsteek nie",
                  "Inua bendera nyekundu","Usishushe bendera nyekundu","Inua bendera nyeupe","Punguza bendera nyeupe",  //スワヒリ語
                  "Usishushe bendera nyekundu","Usipandishe bendera nyekundu","Usishushe bendera nyeupe","Usipandishe bendera nyeupe",
                  "Levante a bandeira vermelha","Abaixe a bandeira vermelha","Levante a bandeira branca","Abaixe a bandeira branca",  //ポルトガル語
                  "Não abaixe a bandeira vermelha","Não levante a bandeira vermelha","Não abaixe a bandeira branca","Não levante a bandeira branca",
                  "붉은 깃발을 올려","붉은 깃발을 내리고","흰 깃발을 올려","흰 깃발을 내리고",  //韓国語
                  "붉은 깃발을 낮추지 마십시오","붉은 깃발을 올리지 마십시오","백기를 낮추지 마십시오","백기를 올리지 마십시오" };

    [SerializeField]
    private GameObject strat;
    [SerializeField]
    private GameObject mainGame;
    [SerializeField]
    private Button stratButton;
    [SerializeField]
    private Button whiteupButton;
    [SerializeField]
    private Button redupButton;
    [SerializeField]
    private Button whitdownButton;
    [SerializeField]
    private Button reddownButton;

    [SerializeField]
    public Text[] score;


    bool isActive = true;


    /*  void setup()
      {
          image_setup();//void image_setupへ
          text_setup();//void text_setupへ
          point = 0;
      }

      void image_setup()
      {  //画像を読み込むプログラムをまとめてある
          frag_red = loadImage("Frag_red.jpg");        //赤旗
          frag_white = loadImage("Frag_white.jpg");    //白旗
          red_up = loadImage("red_up.jpg");            //赤旗を上げるボタン
          red_down = loadImage("red_down.jpg");        //赤旗を下げるボタン
          white_up = loadImage("white_up.jpg");        //白旗を上げるボタン  
          white_down = loadImage("white_down.jpg");    //白旗を下げるボタン
          start_button = loadImage("start_button.jpg");//スタートボタン
      }

      void text_setup()
      {  //旗揚げの命令テキストの設定用プログラムをまとめてある
          font = createFont("MS Gothic", 60);                //ゴシック体
          font_chinese = createFont("Microsoft YaHei", 60);  //中国語のフォント
          font_arabia = createFont("Arial", 60);             //アラビア語のフォント
          font_korea = createFont("Malgun Gothic", 60);      //韓国語のフォント
          textFont(font);        //基本のフォントを変数fontに
          textAlign(CENTER);     //テキストの位置中央に寄せる
          textSize(60);          //フォントサイズを60に
      }*/

    void draw()
    {
        if (scene == 0)
        {  //スタートボタンが押される前の処理
            strat.SetActive(true);//スタートボタンの配置
            point = 0;              //ポイントリセット
        }
        if (scene == 1)
        {  //スタートボタンが押されたあとの処理。ゲームスタート！
            if (red == 1)
            {  //赤旗を上げるボタンが押されたら
                //赤旗を表示
            }
            if (red == 0)
            {  //赤旗を下げるボタンが押されたら
                //赤旗を消す
            }
            if (white == 1)
            {//白旗を上げるボタンが押されたら
                //白旗を表示
            }
            if (white == 0)
            {//白旗を下げるボタンが押されたら
                //白旗を消す
            }
            if (i % 120 == 59)
            {  //2秒ごとに実行する（余りが59なのはスタートボタンを押して1秒後にスタートさせるため
                count = 1;          //押せるボタンの数を1回に戻す
                order_text();       //order_text()へ
            }
            //rect(0, 0, 300, 100);    //前回のポイント表示を消す
            mainGame.SetActive(isActive);
            //text("白旗");  //ボタンの間に「白旗」を表示
            //text("赤旗");  //ボタンの間に「赤旗を」表示
            i++;  //draw関数が呼ばれるごとに+1
            //put_button();          //put_button()へ\
            if (i == 3659)
            {    //iが3659になったら実行(3659は旗の上下の指示30回分)。ゲーム終了時を表す
                finish();       //finish()へ
            }
        }
    }

    void order_text()
    {        //旗の上下の指示するテキストを表示させる
        //rect(0, 0, 1100, 220);   //前回の指示テキストを消す
        rand = Random.Range(0, 80);     //randに乱数を代入
        //textFont(font);     //font表示
        //text(order[rand], 550, 200);
    }

    /*void put_button()
    {
        image(white_up, 320, 300, 200, 200);  //白旗を上げるボタンの配置
        image(white_down, 320, 580, 200, 200);//白旗を下げるボタンの配置
        image(red_up, 580, 300, 200, 200);    //赤旗を上げるボタンの配置
        image(red_down, 580, 580, 200, 200);  //赤旗を下げるボタンの配置
    }*/

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

        strat.SetActive(isActive);
        score[0].text = " " + point;  //「スコア」の下にpointを表示
    }

    void mousePressed()
    {          
        if (stratButton)
        {//スタートボタンをクリックしたとき
            strat.SetActive(false);          //スタートボタンを消す
            scene = 1;                          //変数startに1を代入。ゲームをスタートさせる
        }
        //旗の上げ下げボタンを押したとき
        if (whiteupButton)
        {       //白旗を上げるボタン  
            white = 1;
            check = 2;
            point_count();
        }
        else if (whitdownButton)
        {  //白旗を下げるボタン
            white = 0;
            check = 3;
            point_count();
        }
        else if (redupButton)
        {  //赤旗を上げるボタン
            red = 1;
            check = 0;
            point_count();
        }
        else if (reddownButton)
        {  //赤旗を下げるボタン
            red = 0;
            check = 1;
            point_count();
        }
    }

    void point_count()
    {  //旗上下ボタン押した後の正誤判定
        if (count == 1)
        {
            num = rand % 4;            //randを4で割ったときの余りを代入(この数が配列orderと対応してい
            if (num == check)
            {          //変数checkとnumが同じとき実行(指示と旗の状態が同じとき）
                point += 10;             //ポイントを+10
            }
            count = 0;                //押せるボタンの数を1回に戻す
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score[1].text = " " + point;
        point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        draw();
    }
}
