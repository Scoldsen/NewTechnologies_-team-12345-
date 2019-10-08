using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; //プレイヤーの速さ
    public Sprite[] walk; //プレイヤーの歩くスプライト配列
    private int animIndex; //歩くアニメーションのインデックス
    private bool walkCheck; //歩いているかのチェック
    private Rigidbody2D player;

    // Use this for initialization
    private void Start()
    {
        animIndex = 0;
        walkCheck = true;
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //歩いていたら歩くアニメーションの再生
        if (walkCheck)
        {
            animIndex++;
            if (animIndex >= walk.Length)
            {
                animIndex = 0;
            }
            GetComponent<SpriteRenderer>().sprite = walk[animIndex];
        }

        ////マウスをクリックしたら歩き出す
        //if (Input.GetButton("Fire1"))
        //{
        //    walkCheck = true;
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        //}
        ////マウスのクリックを離すと止まる
        //else if (Input.GetButtonUp("Fire1") && walkCheck)
        //{
        //    walkCheck = false;
        //    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //}

        //player.velocity = new Vector2(speed, player.velocity.y);
        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnGUI()
    {
        //// 説明テキスト
        //GUI.TextField(new Rect(5, 5, 400, 40), "ゲーム画面上でマウスの左ボタンを押し続けてる間は歩く。ボタンを離すと止まる。");
        //// リセットボタン
        //if (GUI.Button(new Rect(5, 50, 110, 30), "リセットボタン"))
        //{
        //    Application.LoadLevel(Application.loadedLevelName);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Field"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
