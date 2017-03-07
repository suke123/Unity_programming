using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballScript : MonoBehaviour
{

    public GameObject ballPrefab;   //ボールのプレハブ
    public Sprite[] ballSprites;    //ボールの画像のリスト

    private GameObject firstBall;   //最初にドラッグしたボール
    List<GameObject> removableBallList = new List<GameObject>();
    List<GameObject> selectedBallList = new List<GameObject>();
    private GameObject lastBall;    //最後にドラッグしたボール
    private string currentName;     //名前判定用のstring変数

    public bool isFalledBall = false;

    //private bool isPlaying = false;

    public GameObject scoreGUI;
    //private Text scoreText;
    private int point = 100;

    //bool isSelected;
    int i = 0;

    // Use this for initialization
    void Start()
    {
        ballPrefab.SetActive(true);
        StartCoroutine(DropBall(60));   //ボールを指定した数上から降らせる
        isFalledBall = false;
    }

    // Update is called once per frame
    void Update()
    {
        //画面をクリックし、firstBallがnullの時実行
        if (Input.GetMouseButtonDown(0) && firstBall == null)
        {
            OnDragStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //クリックを終えたとき
            OnDragEnd();
        }
        //OnDragStartメソッド実行後
        else if (firstBall != null)
        {
            OnDragging();
        }
    }

    private void OnDragStart()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObj = hit.collider.gameObject;
            //オブジェクトの名前を前方一致で判定
            string ballName = hitObj.name;
            if (ballName.StartsWith("Gem"))
            {
                firstBall = hitObj;

                lastBall = hitObj;
                currentName = hitObj.name;
                //削除対象オブジェクトリストの初期化
                removableBallList = new List<GameObject>();
                selectedBallList = new List<GameObject>();
                //削除オブジェクトの格納
                PushToList(hitObj);
            }
        }
    }

    private void OnDragging()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitObj = hit.collider.gameObject;
            //同じ名前のブロックをクリック＆lastBallとは別オブジェクトである時
            for(int j = 0; j < i; j++)
            {
                if (hitObj.name == currentName && lastBall != hitObj && hitObj != selectedBallList[j])
                {
                    //2つのオブジェクトの距離を取得
                    float distance = Vector2.Distance(hitObj.transform.position, lastBall.transform.position);
                    if (distance <= 1.2f)
                    {
                        //削除対象のオブジェクトを格納
                        lastBall = hitObj;
                        PushToList(hitObj);
                    }
                }
            }
        }
    }

    private void OnDragEnd()
    {
        int remove_cnt = removableBallList.Count;
        if (remove_cnt >= 3)
        {
            for (int i = 0; i < remove_cnt; i++)
            {
                Destroy(removableBallList[i]);
            }

            scoreGUI.SendMessage("AddPoint", point * remove_cnt);

            //ボールを新たに生成
            StartCoroutine(DropBall(remove_cnt));
        }
        else
        {
            //色の透明度を100%に戻す
            for (int i = 0; i < remove_cnt; i++)
            {
                ChangeColor(removableBallList[i], 1.0f);
            }
        }
        firstBall = null;
        lastBall = null;
        removableBallList = null;
        selectedBallList = null;
        i = 0;
    }

    IEnumerator DropBall(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-2.0f, 2.0f), 3f);
            GameObject ball = Instantiate(ballPrefab, pos, Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward)) as GameObject;
            int spriteId = Random.Range(0, 5);
            ball.name = "Gem" + spriteId;
            SpriteRenderer spriteObj = ball.GetComponent<SpriteRenderer>();
            spriteObj.sprite = ballSprites[spriteId];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        isFalledBall = true;
    }

    void PushToList(GameObject obj)
    {
        removableBallList.Add(obj);
        selectedBallList.Add(obj);
        i++;
        //Debug.Log(selectedBallList);
        //色の透明度を50%に落とす
        ChangeColor(obj, 0.5f);
    }

    void ChangeColor(GameObject obj, float transparency)
    {
        //SpriteRendererコンポーネントを取得
        SpriteRenderer ballTexture = obj.GetComponent<SpriteRenderer>();
        //Colorプロパティのうち、透明度のみを変更する
        ballTexture.color = new Color(ballTexture.color.r, ballTexture.color.g, ballTexture.color.b, transparency);
    }
}