using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    public InGameUIController InGameUIController { get; private set; }

    private bool m_IsLoadingInDunGeon;

    protected override void Init()
    {
        //인게임 매니저는 다른 씬으로 전환 할 때 삭제
        m_IsDestroyOnLoad = true;
        m_IsLoadingInDunGeon = false;
        base.Init();
    }

    private void Start()
    {
        //FindObjectofType은 씬에 존재하는 타입을 찾아 가장 먼저 찾은인스턴스를 넘겨줌
        //로비유아이컨트롤러는 로비씬에서 하나만 존재할것임
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
