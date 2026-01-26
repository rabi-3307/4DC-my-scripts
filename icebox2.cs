using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class icebox2 : MonoBehaviour
{
    [SerializeField] Image gaugeImage;
    //[SerializeField] Image foodImage; // 追加: 食材のイラストを表示するImageコンポーネント
    //[SerializeField] Sprite[] foodSprites; // 追加: 各食材に対応するSpriteの配列

    // クールダウンが現在進行中かを示すフラグ
    bool isProcessing = false;

    // クールダウンの残り時間 (今回は経過時間として使用)
    private float coolDownTimer = 0f;

    // ゲージが満タンになるまでの時間
    [SerializeField] private float coolDownDuration = 5.0f; // 例として5秒に設定

    // 新しいフラグ: ゲージが満タンで、次のアクションの準備ができている場合にtrue
    public bool isReady { get; private set; } = true;

    // 冷蔵庫の中身のアイテム名
    public string foodname;

    void Start()
    {
        //SetRandomFood();

        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = 1; // ゲージの初期値を1に設定 (クールダウン開始時に0になるため)
        }
    }

    void Update()
    {
        if (isProcessing)
        {
            // クールダウンタイマーを増やす（経過時間として使用）
            coolDownTimer += Time.deltaTime;

            // ゲージの値を更新 (0から1へ増加する形式)
            gaugeImage.fillAmount = coolDownTimer / coolDownDuration;

            if (coolDownTimer >= coolDownDuration) // 経過時間がクールダウン時間を超えたら終了
            {
                // クールダウン終了
                coolDownTimer = coolDownDuration; // 最大値にクランプ
                isProcessing = false;
                isReady = true; // アイテムが再度拾えるようにする
                //SetRandomFood();  もしクールダウンが終わるたびに新しいアイテムがランダムに生成
            }
        }
    }

    // 新しいクールダウンメソッド
    public void CoolDown()
    {
        if (isReady && !isProcessing) // isReadyも確認して、拾える状態でのみクールダウンを開始
        {
            isProcessing = true;
            isReady = false; // クールダウン中はアイテムを拾えないようにする

            // クールダウンタイマーを初期化 (0から開始)
            coolDownTimer = 0f;

            // ゲージを表示し、初期値を0に設定
            gaugeImage.fillAmount = 0; // 0からカウントを開始
        }
    }

    // // ランダムなアイテムをセットするメソッド
    // void SetRandomFood()
    // {
    //     var foodindex = Random.Range(0, foodSprites.Length); // 柔軟性を高めるために変更　foodSprites.Length　-> 5　でもよい

    //     if (foodindex == 0)
    //     {
    //         this.foodname = "item0";
    //     }
    //     else if (foodindex == 1)
    //     {
    //         this.foodname = "item1";
    //     }
    //     else if (foodindex == 2)
    //     {
    //         this.foodname = "item2";
    //     }
    //     else if (foodindex == 3)
    //     {
    //         this.foodname = "item3";
    //     }
    //     else
    //     {
    //         this.foodname = "item4";
    //     }

    //     // 取得したfoodnameに対応するSpriteを設定
    //     if (foodImage != null && foodSprites != null && foodSprites.Length > foodindex)
    //     {
    //         foodImage.sprite = foodSprites[foodindex];
    //     }
    // }
}
