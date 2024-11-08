using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    //로고
    public Animation LogoAnim;
    public TextMeshProUGUI LogoTxt;

    //타이틀
    public GameObject Title;
    public Slider LoadingSlider;
    public TextMeshProUGUI LoadingProgressTxt;

    //비동기 로딩을 로드하기 위한 선언
    AsyncOperation m_AsyncOperation;

    private void Awake()
    {
        //처음 시작할때 로고 애니메이션을 재생시켜주기위해 로고애니메이션을 활성화 시켜줌
        LogoAnim.gameObject.SetActive(true);
        //로고애니메이션 재생이 끝난 후 타이틀을 활성화 해줘야 하기 때문에 타이틀 씬이 시작되었을 땐 타이틀은 일단 꺼놔줌
        Title.SetActive(false);
    }
    void Start()
    {
        //유저 데이터 로드
        UserDataManager.Instance.LoadUserData();

        //저장된 유저 데이터가 없으면 기본값으로 세팅 후 저장
        if (!UserDataManager.Instance.ExistsSavedData)
        {
            UserDataManager.Instance.SetDefaultUserData();
            UserDataManager.Instance.SaveUserData();
        }

        StartCoroutine(LoadGameCo());
        //시작시 바로 실행 되어야함
        AudioManager.Instance.OnLoadUserData();
        //타이틀씬에서는 재화표시를 꺼줘야함 UIManager에서 작성해준 함수 호출
        UIManager.Instance.EnableGoodsUI(false);
    }

    IEnumerator LoadGameCo()
    {
        //이 코루틴 함수는 게임의 로딩을 처음 시작하는 중요한 함수이기 때문에
        //로그를 찍음.
        //GetType() : 클래스 명을 출력
        //타이틀 매니저에서 호출하는 로드게임 코루틴이라는 함수 확인
        Logger.Log($"{GetType()}::LoadGameCo");
        //로로 애니메이션 재생시켜주면서
        LogoAnim.Play();
        //애니메이션클립의 길만큼 대기 후에
        yield return new WaitForSeconds(LogoAnim.clip.length);
        //애니메이션 꺼주고
        LogoAnim.gameObject.SetActive(false);
        //타이틀 화면 꺼줬던걸 애니메이션 재생이 끝난 후 켜주기
        Title.SetActive(true);

        m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.InGame);

        if (m_AsyncOperation == null)
        {
            //AsyncOperation이 없다면 애러 매세지 출력
            Logger.Log("Lobby async Loading Error");
            //코루틴 종료
            yield break;
        }
        //일부러 몇 초 간 50%로 보여줌으로써 시각적으로 더 자연스럽게 연출
        //이상없이 잘 반환 되어져 왔다면
        m_AsyncOperation.allowSceneActivation = false;
        LoadingSlider.value = 0.5f;
        LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)} %";
        yield return new WaitForSeconds(0.5f);

        //로딩이 진행 중일 때
        while (!m_AsyncOperation.isDone)
        {
            //로딩 슬라이더 업데이트
            LoadingSlider.value = m_AsyncOperation.progress < 0.5f ? 0.5f : m_AsyncOperation.progress;
            LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)} %";

            //씬 로딩이 완료 되었다면 로비로 전환하고 코루틴 종료
            if (m_AsyncOperation.progress >= 0.9f)
            {
                m_AsyncOperation.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
    }
}
