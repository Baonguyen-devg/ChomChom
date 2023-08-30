using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : AutoMonoBehaviour
{
    [SerializeField] private AudioSource clickAudio;
    private void LoadClickAudio() =>
        this.clickAudio = GameObject.Find("Button_Audio_Source")?.GetComponent<AudioSource>();

    protected override void LoadComponent() => this.LoadClickAudio();

    public virtual void PlayGame()
    {
        this.PlayClickAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public virtual void QuitGame()
    {
        this.PlayClickAudio();
        Application.Quit();
    }

    public virtual void PlayClickAudio() => this.clickAudio.Play();
}
