using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 씬을 이동하는 메서드
    public void LoadScene(string a)
    {
        SceneManager.LoadScene(a);
    }
}
