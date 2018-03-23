using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPGTALK.Localization;

public class PreferencesManager : MonoBehaviour {

	void Start () {
        if (PlayerPrefs.HasKey("Language"))
        {
            switch (PlayerPrefs.GetString("Language"))
            {
                case "EN_US":
                    LanguageSettings.actualLanguage = SupportedLanguages.EN_US;
                    ChangeTextMenu("New Game", "Continue", "Load Game", "Choose the mission", "Controls", "Options", "Language", "Exit", "Back");
                    break;
                case "PT_BR":
                    LanguageSettings.actualLanguage = SupportedLanguages.PT_BR;
                    ChangeTextMenu("Novo Jogo", "Continuar", "Carregar Jogo", "Escolha a missão", "Controles", "Opções", "Idioma", "Sair", "Voltar");
                    break;
            }
        }
	}

    public void ChangeTextMenu(string newGame, string continueGame, string loadGame, string missionLabel, string controls, string options, string optionsLabel, string exit, string back)
    {
        FindDeepChild(transform, "NewGameText").gameObject.GetComponent<Text>().text = newGame;
        FindDeepChild(transform, "ContinueText").gameObject.GetComponent<Text>().text = continueGame;
        FindDeepChild(transform, "LoadGameText").gameObject.GetComponent<Text>().text = loadGame;
        FindDeepChild(transform, "MissionLabel").gameObject.GetComponent<Text>().text = missionLabel;
        FindDeepChild(transform, "ControlsText").gameObject.GetComponent<Text>().text = controls;
        FindDeepChild(transform, "OptionsText").gameObject.GetComponent<Text>().text = options;
        FindDeepChild(transform, "OptionsLabel").gameObject.GetComponent<Text>().text = optionsLabel;
        FindDeepChild(transform, "ExitText").gameObject.GetComponent<Text>().text = exit;
        FindDeepChild(transform, "BackLoadGameText").gameObject.GetComponent<Text>().text = back;
        FindDeepChild(transform, "BackControlsText").gameObject.GetComponent<Text>().text = back;
        FindDeepChild(transform, "BackOptionsText").gameObject.GetComponent<Text>().text = back;
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
