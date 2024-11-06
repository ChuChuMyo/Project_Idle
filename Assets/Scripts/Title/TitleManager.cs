using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    //�ΰ�
    public Animation LogoAnim;
    public TextMeshProUGUI LogoTxt;

    //Ÿ��Ʋ
    public GameObject Title;
    public Slider LoadingSlider;
    public TextMeshProUGUI LoadingProgressTxt;

    //�񵿱� �ε��� �ε��ϱ� ���� ����
    AsyncOperation m_AsyncOperation;

    private void Awake()
    {
        //ó�� �����Ҷ� �ΰ� �ִϸ��̼��� ��������ֱ����� �ΰ��ִϸ��̼��� Ȱ��ȭ ������
        LogoAnim.gameObject.SetActive(true);
        //�ΰ��ִϸ��̼� ����� ���� �� Ÿ��Ʋ�� Ȱ��ȭ ����� �ϱ� ������ Ÿ��Ʋ ���� ���۵Ǿ��� �� Ÿ��Ʋ�� �ϴ� ������
        Title.SetActive(false);
    }
    void Start()
    {
        //���� ������ �ε�
        UserDataManager.Instance.LoadUserData();

        //����� ���� �����Ͱ� ������ �⺻������ ���� �� ����
        if (!UserDataManager.Instance.ExistsSavedData)
        {
            UserDataManager.Instance.SetDefaultUserData();
            UserDataManager.Instance.SaveUserData();
        }

        StartCoroutine(LoadGameCo());
        //���۽� �ٷ� ���� �Ǿ����
        AudioManager.Instance.OnLoadUserData();
        //Ÿ��Ʋ�������� ��ȭǥ�ø� ������� UIManager���� �ۼ����� �Լ� ȣ��
        UIManager.Instance.EnableGoodsUI(false);
    }

    IEnumerator LoadGameCo()
    {
        //�� �ڷ�ƾ �Լ��� ������ �ε��� ó�� �����ϴ� �߿��� �Լ��̱� ������
        //�α׸� ����.
        //GetType() : Ŭ���� ���� ���
        //Ÿ��Ʋ �Ŵ������� ȣ���ϴ� �ε���� �ڷ�ƾ�̶�� �Լ� Ȯ��
        Logger.Log($"{GetType()}::LoadGameCo");
        //�η� �ִϸ��̼� ��������ָ鼭
        LogoAnim.Play();
        //�ִϸ��̼�Ŭ���� �游ŭ ��� �Ŀ�
        yield return new WaitForSeconds(LogoAnim.clip.length);
        //�ִϸ��̼� ���ְ�
        LogoAnim.gameObject.SetActive(false);
        //Ÿ��Ʋ ȭ�� ������� �ִϸ��̼� ����� ���� �� ���ֱ�
        Title.SetActive(true);

        m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.InGame);

        if (m_AsyncOperation == null)
        {
            //AsyncOperation�� ���ٸ� �ַ� �ż��� ���
            Logger.Log("Lobby async Loading Error");
            //�ڷ�ƾ ����
            yield break;
        }
        //�Ϻη� �� �� �� 50%�� ���������ν� �ð������� �� �ڿ������� ����
        //�̻���� �� ��ȯ �Ǿ��� �Դٸ�
        m_AsyncOperation.allowSceneActivation = false;
        LoadingSlider.value = 0.5f;
        LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)} %";
        yield return new WaitForSeconds(0.5f);

        //�ε��� ���� ���� ��
        while (!m_AsyncOperation.isDone)
        {
            //�ε� �����̴� ������Ʈ
            LoadingSlider.value = m_AsyncOperation.progress < 0.5f ? 0.5f : m_AsyncOperation.progress;
            LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)} %";

            //�� �ε��� �Ϸ� �Ǿ��ٸ� �κ�� ��ȯ�ϰ� �ڷ�ƾ ����
            if (m_AsyncOperation.progress >= 0.9f)
            {
                m_AsyncOperation.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
    }
}