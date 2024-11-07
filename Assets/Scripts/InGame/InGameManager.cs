using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    public InGameUIController InGameUIController { get; private set; }

    private bool m_IsLoadingInDunGeon;

    protected override void Init()
    {
        //�ΰ��� �Ŵ����� �ٸ� ������ ��ȯ �� �� ����
        m_IsDestroyOnLoad = true;
        m_IsLoadingInDunGeon = false;
        base.Init();
    }

    private void Start()
    {
        //FindObjectofType�� ���� �����ϴ� Ÿ���� ã�� ���� ���� ã���ν��Ͻ��� �Ѱ���
        //�κ���������Ʈ�ѷ��� �κ������ �ϳ��� �����Ұ���
        InGameUIController = FindFirstObjectByType<InGameUIController>();

        if (!InGameUIController)
        {
            Logger.LogError("LobbyUIController does not exist");
            return;
        }

        InGameUIController.Init();
        AudioManager.Instance.PlayBGM(BGM.lobby);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
