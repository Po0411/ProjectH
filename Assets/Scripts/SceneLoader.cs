using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 이동할 씬의 이름 또는 인덱스를 입력
    public string sceneName;

    // 씬을 이동하는 메서드
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
