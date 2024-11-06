using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{

    public void Init()
    {
        //로비에서는 켜줘야 하기때문에 UIManager에서 작성해준 재화 활성화 비활성화 함수 호출
        UIManager.Instance.EnableGoodsUI(true);
        //SetCurrChapter();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
