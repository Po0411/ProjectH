using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �̵��� ���� �̸� �Ǵ� �ε����� �Է�
    public string sceneName;

    // ���� �̵��ϴ� �޼���
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
