using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{

    public void Init()
    {
        //�κ񿡼��� ����� �ϱ⶧���� UIManager���� �ۼ����� ��ȭ Ȱ��ȭ ��Ȱ��ȭ �Լ� ȣ��
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
