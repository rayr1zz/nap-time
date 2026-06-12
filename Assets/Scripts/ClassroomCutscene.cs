using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ClassroomCutscene : MonoBehaviour
{
    public TextMeshProUGUI cutsceneText;
    public Image fadePanel;
    public Image cutsceneImage;

    public Sprite classroomNormal;
    public Sprite classroomSleep;

    public string nextSceneName = "DungeonEntrance";

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        cutsceneText.text = "";
        fadePanel.color = new Color(0, 0, 0, 0);

        cutsceneImage.sprite = classroomNormal;

        yield return new WaitForSeconds(2f);

        cutsceneText.text = "Егор сидел за партой и устало смотрел на доску...";
        yield return new WaitForSeconds(3f);

        cutsceneText.text = "Глаза начали закрываться...";
        yield return new WaitForSeconds(2f);

        cutsceneImage.sprite = classroomSleep;

        cutsceneText.text = "Он положил голову на парту и провалился в сон...";
        yield return new WaitForSeconds(3f);

        cutsceneText.text = "";

        float alpha = 0f;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime;
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nextSceneName);
    }
}