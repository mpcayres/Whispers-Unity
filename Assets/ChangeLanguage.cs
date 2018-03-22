using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK.Localization;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour {
    public int language;
    private Button button { get { return GetComponent<Button>(); } }
    Transform menuCanvas;

    private void Awake()
    {
        menuCanvas = GameObject.Find("MenuCanvas").gameObject.transform;
    }

    void Start()
    {
        button.onClick.AddListener(() => ChangeTextLanguage());
    }

    void ChangeTextLanguage()
    {
        switch (language) {
            case 1:
                LanguageSettings.actualLanguage = SupportedLanguages.EN_US;
                ChangeTextMenu("New Game", "Continue", "Load Game", "Choose the mission", "Controls", "Options", "Language", "Exit", "Back");
                break;
            case 2:
                LanguageSettings.actualLanguage = SupportedLanguages.PT_BR;
                ChangeTextMenu("Novo Jogo", "Continuar", "Carregar Jogo", "Escolha a missão", "Controles", "Opções", "Idioma", "Sair", "Voltar");
                break;
        }
    }

    void ChangeTextMenu(string newGame, string continueGame, string loadGame, string missionLabel, string controls, string options, string optionsLabel, string exit, string back)
    {
        FindDeepChild(menuCanvas.transform, "NewGameText").gameObject.GetComponent<Text>().text = newGame;
        FindDeepChild(menuCanvas.transform, "ContinueText").gameObject.GetComponent<Text>().text = continueGame;
        FindDeepChild(menuCanvas.transform, "LoadGameText").gameObject.GetComponent<Text>().text = loadGame;
        FindDeepChild(menuCanvas.transform, "MissionLabel").gameObject.GetComponent<Text>().text = missionLabel;
        FindDeepChild(menuCanvas.transform, "ControlsText").gameObject.GetComponent<Text>().text = controls;
        FindDeepChild(menuCanvas.transform, "OptionsText").gameObject.GetComponent<Text>().text = options;
        FindDeepChild(menuCanvas.transform, "OptionsLabel").gameObject.GetComponent<Text>().text = optionsLabel;
        FindDeepChild(menuCanvas.transform, "ExitText").gameObject.GetComponent<Text>().text = exit;
        FindDeepChild(menuCanvas.transform, "BackLoadGameText").gameObject.GetComponent<Text>().text = back;
        FindDeepChild(menuCanvas.transform, "BackControlsText").gameObject.GetComponent<Text>().text = back;
        FindDeepChild(menuCanvas.transform, "BackOptionsText").gameObject.GetComponent<Text>().text = back;
        // Mudar imagem dos controles
    }

    public static Transform FindDeepChild(Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null) return result;

        foreach (Transform child in aParent)
        {
            result = FindDeepChild(child, aName);
            if (result != null) return result;
        }
        return null;
    }

}
