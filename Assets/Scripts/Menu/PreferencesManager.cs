using UnityEngine;
using UnityEngine.UI;
using RPGTALK.Localization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        // Adaptar para mais de um save
        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave" + 0 + "*.save"))
        {
            FindDeepChild(transform, "ContinueButton").gameObject.SetActive(false);
        }
        else
        {
            string[] files = MissionManager.GetFilesByPattern(Application.persistentDataPath, "gamesave" + 0 + "*.save");
            RectTransform rect = FindDeepChild(transform, "ContinueSavesContent").gameObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 30 * files.Length);
            foreach (string f in files)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(f, FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();

                string[] parts = f.Split('.');
                string part = parts[parts.Length - 2];
                string i = part.Substring(part.Length - 1);
                AddButton("SaveButton", i + ": " + save.time, i, FindDeepChild(transform, "ContinueSavesContent").gameObject);
            }
        }

        if (!MissionManager.FilePatternExists(Application.persistentDataPath, "gamesave*.save"))
        {
            FindDeepChild(transform, "LoadGameButton").gameObject.SetActive(false);
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

    GameObject AddButton(string name, string text, string numSave, GameObject parent)
    {
        // print("UI: " + name + " > " + text);
        GameObject instance =
            Instantiate(Resources.Load("Prefab/UI/" + name) as GameObject);

        instance.transform.SetParent(parent.transform);
        instance.GetComponent<RectTransform>().transform.localScale = new Vector3(1,1,1);
        if (text != "") instance.transform.Find("Text").GetComponent<Text>().text = text;

        if (name.Equals("SaveButton"))
        {
            instance.GetComponent<ContinueGame>().black = transform.Find("BlackSquare").GetComponent<Image>();
            instance.GetComponent<ContinueGame>().anim = transform.Find("BlackSquare").GetComponent<Animator>();
            instance.GetComponent<Button>().onClick.AddListener(delegate { instance.GetComponent<ContinueGame>().OnClick(int.Parse(numSave)); });
        }

        return instance;
    }
    
}
