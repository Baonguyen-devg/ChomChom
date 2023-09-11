using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : AutoMonoBehaviour
{
    /*Begin predicatedload of components*/
    [SerializeField] private List<System.Action> loadComponentActions;

    [SerializeField] private AudioSource clickAudio;
    /*End predicatedload of components*/

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.loadComponentActions = new List<System.Action>
        {
            () => this.clickAudio = GameObject.Find("Button_Audio_Source")?.GetComponent<AudioSource>(),
        };
        foreach (var action in this.loadComponentActions) 
            action?.Invoke();
    }

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
