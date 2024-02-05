using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public Canvas canvas; // 要控制的 Canvas 物件
    public bool isGamePaused = false; // 遊戲是否已暫停

    // 這個方法將被按鈕的點擊事件調用
    public void ToggleCanvasAndPauseGame()
    {
        // 切換 Canvas 的顯示狀態
        canvas.enabled = !canvas.enabled;

        // 切換遊戲的暫停狀態
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0; // 暫停遊戲
        }
        else
        {
            Time.timeScale = 1; // 恢復遊戲
        }
    }
}