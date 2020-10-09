using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // private void OnClick(){
    //     //UnityEditor.EditorApplication.isPlaying = false;
    //     UnityEditor.EditorApplication.isPlaying = false;
    //     Application.Quit();
    // }

    private void OnClick() {
         /*将状态设置false才能退出游戏*/
        //  UnityEditor.EditorApplication.isPlaying = false;
        //  Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
     }
}
