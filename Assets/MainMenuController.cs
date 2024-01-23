using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private int gameLevelIndex = 0;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonStart = root.Q<Button>("StartButton");
        Button buttonQuit = root.Q<Button>("QuitButton");
        
        buttonStart.RegisterCallback<ClickEvent>(ev => { StartGame(); });
        buttonQuit.RegisterCallback<ClickEvent>(ev => { QuitGame(); });
    }
    
    private void StartGame()
    {
        SceneManager.LoadScene(gameLevelIndex);
    }
    
    private void QuitGame()
    { 
        Application.Quit(); 
    }
}
