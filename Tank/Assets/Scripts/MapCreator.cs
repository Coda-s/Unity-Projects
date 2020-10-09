using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    /**
     * 地图元素
     * 0.家
     * 1.墙
     * 2.障碍
     * 3.出生效果
     * 4.水
     * 5.草
     * 6.空气墙
     */
    public GameObject[] mapElements;
    public GameObject[] tools;

    private List<Vector3> itemPositionList = new List<Vector3>();
    private List<GameObject> homeBlockList = new List<GameObject>();
    public float barrierTimeVal = 3.0f;
    public bool isMakeBarrier = false;
    
    
    // 单例模式
    private static MapCreator instance;

    public static MapCreator Instance{
        get{
            return instance;
        }
        set{
            instance = value;
        }
    }


    private void Awake(){
        Instance = this;
        itemPositionList.Add(new Vector3( 0, -7, 0));
        itemPositionList.Add(new Vector3( 1, -7, 0));
        itemPositionList.Add(new Vector3(-1, -7, 0));
        itemPositionList.Add(new Vector3( 1, -8, 0));
        itemPositionList.Add(new Vector3(-1, -8, 0));
        InitMap();
    }

    private void Update() {
        if(isMakeBarrier){
            if(barrierTimeVal >= 3.0){
                ProtectHome(1);
                isMakeBarrier = false;
            } else {
                ProtectHome(2);
                barrierTimeVal += Time.deltaTime;
            }
        }
    }

    // 初始化地图
    private void InitMap(){
        // 实例化老家
        CreateItem(mapElements[0], new Vector3(0, -8, 0), Quaternion.identity);
        // 用墙围住老家
        ProtectHome(1);
        // 空气墙
        for(int i=-11; i<=11; i++){
            CreateItem(mapElements[6], new Vector3(i, -9, 0), Quaternion.identity);
            CreateItem(mapElements[6], new Vector3(i,  9, 0), Quaternion.identity);
        }
        for(int i=-9; i<=9; i++){
            CreateItem(mapElements[6], new Vector3( 11, i, 0), Quaternion.identity);
            CreateItem(mapElements[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        // 初始化玩家
        GameObject bornPlayer = Instantiate(mapElements[3], new Vector3(-2, -8, 0), Quaternion.identity);
        bornPlayer.GetComponent<Born>().createPlayer = true;

        // 产生敌人
        CreateItem(mapElements[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(mapElements[3], new Vector3( 10, 8, 0), Quaternion.identity);
        CreateItem(mapElements[3], new Vector3(  0, 8, 0), Quaternion.identity);

        InvokeRepeating("generateEnemy", 4, 2);
        // 4s后每2s执行一次

        // 实例化地图
        for(int i=0; i<20; i++){
            CreateItem(mapElements[1], RandomVector3(), Quaternion.identity);
            CreateItem(mapElements[2], RandomVector3(), Quaternion.identity);
            CreateItem(mapElements[4], RandomVector3(), Quaternion.identity);
            CreateItem(mapElements[5], RandomVector3(), Quaternion.identity);
        }
    }

    // 创建物体
    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation){
        GameObject item = Instantiate(createGameObject, createPosition, createRotation);
        item.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

    // 产生随机位置
    private Vector3 RandomVector3(){
        while(true){
            int x = Random.Range(-9,10);
            int y = Random.Range(-7,8);
            Vector3 position = new Vector3(x, y, 0);
            if(!itemPositionList.Contains(position)) return position;
        }
    }

    // 产生敌人的方法
    private void generateEnemy(){
        int num = Random.Range(0, 3);
        Vector3 position = new Vector3();
        if(num == 0){
            position = new Vector3( 10, 8, 0);
        } else if(num == 1){
            position = new Vector3(-10, 8, 0);
        } else if(num == 2){
            position = new Vector3(  0, 8, 0);
        }
        CreateItem(mapElements[3], position, Quaternion.identity);
    }

    // 制造屏障
    private void MakeBarrier(){
        isMakeBarrier = true;
        barrierTimeVal = 0.0f;
    }

    // 保护老家
    private void ProtectHome(int itemId){
        GameObject item1 = Instantiate(mapElements[itemId], new Vector3( 0, -7, 0), Quaternion.identity);
        GameObject item2 = Instantiate(mapElements[itemId], new Vector3( 1, -8, 0), Quaternion.identity);
        GameObject item3 = Instantiate(mapElements[itemId], new Vector3(-1, -8, 0), Quaternion.identity);
        GameObject item4 = Instantiate(mapElements[itemId], new Vector3( 1, -7, 0), Quaternion.identity);
        GameObject item5 = Instantiate(mapElements[itemId], new Vector3(-1, -7, 0), Quaternion.identity);
        item1.transform.SetParent(gameObject.transform);
        item2.transform.SetParent(gameObject.transform);
        item3.transform.SetParent(gameObject.transform);
        item4.transform.SetParent(gameObject.transform);
        item5.transform.SetParent(gameObject.transform);
        for(int i=0; i<homeBlockList.Count; i++){
            if(homeBlockList[i] != null){
                Destroy(homeBlockList[i]);
            }
        }
        homeBlockList.Clear();
        homeBlockList.Add(item1);
        homeBlockList.Add(item2);
        homeBlockList.Add(item3);
        homeBlockList.Add(item4);
        homeBlockList.Add(item5);
    }

    // 生成道具
    public void GenerateTool(){
        Vector3 position = RandomVector3();
        int num = Random.Range(0, 6);
        CreateItem(tools[num], position, Quaternion.identity);
    }
}
