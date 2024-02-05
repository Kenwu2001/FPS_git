using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public Image BlackCover;
    // public Image BloodBlur;
    public Text HPText;
    private string resumeButton = "Resume";
    // private string pauseButton = "PauseButton";
    GameObject resumeButtonObject;

    // Use this for initialization
    void Start()
    {
        BlackCover.color = Color.black;
        DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, new Color(0, 0, 0, 0), 1f);
        // GameObject resumeButtonObject = GameObject.Find(resumeButton);
        resumeButtonObject = GameObject.Find(resumeButton);
        // GameObject pauseButtonObject = GameObject.Find(pauseButton);

        if (resumeButtonObject != null)
        {
            resumeButtonObject.SetActive(false);
        }
    }

    Tweener tweenAnimation;
    
    public void Pause()
    {
        // GameObject resumeButtonObject = GameObject.Find(resumeButton);
        // Debug.Log("hghghghghghghghghghghgh");
        // resumeButtonObject.SetActive(true);
        if (resumeButtonObject != null)
        {
            Debug.Log("hghghghghghghghghghghgh");
            resumeButtonObject.SetActive(true);
        }
        // canvasMenu.enabled = true;
        Time.timeScale = 0;
    }
    
    public void Resume()
    {
        // GameObject resumeButtonObject = GameObject.Find(resumeButton);

        resumeButtonObject.SetActive(false);
        // canvasMenu.enabled = false;
        Time.timeScale = 1;
    }

    public void PlayHitAnimation()
    {
        if (tweenAnimation != null)
            tweenAnimation.Kill();

        // BloodBlur.color = Color.white;
        // tweenAnimation = DOTween.To(() => BloodBlur.color, (x) => BloodBlur.color = x, new Color(1, 1, 1, 0), 0.5f);

    }

    public void PlayerDiedAnimation()
    {
        // BloodBlur.color = Color.white;
        DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, new Color(0, 0, 0, 1), 1f).
        SetDelay(3).
        OnComplete(()=>
        {
            SceneManager.LoadScene("PrewScene");
            // DOTween.To(() => BloodBlur.color,
            //     (x) => BloodBlur.color = x,
            //     new Color(1, 1, 1, 0), 0.5f).SetDelay(3).OnComplete(() =>
            //     {
            //         SceneManager.LoadScene("PrewScene");
            //     });
        });
    }

    public void SetHP(int hp)
    {
        HPText.text = "HP:" + hp;
    }
}