using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // 属性
    public int heart = 5; // 生命值
    public int score = 0; // 分数
    public bool isDead = false; // 是否死亡
    public bool isDefeated = false; // 是否结束
    public float stopTimeVal = 10.0f;

    // 引用
    public GameObject born;
    public Text PlayerScore;
    public Text PlayerHeart;
    public GameObject defeatUI;

    // 单例模式
    private static PlayerManager instance;

    public static PlayerManager Instance{
        get{
            return instance;
        }
        set{
            instance = value;
        }
    }

    private void Awake(){
        Instance = this;
        defeatUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(isDefeated == true){
            defeatUI.SetActive(true);
            Invoke("ReturnToMenu", 3);
            return; 
        }

        if(isDead == true){
            Recover();
        }
        PlayerScore.text = score.ToString();
        PlayerHeart.text = heart.ToString();
    }

    private void Recover(){
        heart--;
        if(heart <= 0){
            // 游戏结束
            isDefeated = true;
        } else {
            GameObject player = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            player.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

    private void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }

    
}
