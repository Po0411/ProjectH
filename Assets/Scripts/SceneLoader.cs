using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // ���� �̵��ϴ� �޼���
    public void LoadScene(string a)
    {
        SceneManager.LoadScene(a);
    }
}
