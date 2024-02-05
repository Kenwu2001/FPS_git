using UnityEngine;

public class ShrinkToHalf : MonoBehaviour
{
    
    public float shrinkSpeed = 0.1f; // 縮小速度
    private Vector3 originalScale; // 原始大小

    void Start()
    {

        originalScale = transform.localScale; // 在開始時記錄原始大小
    }

    void Update()
    {
        float time = Time.time;

        if (time >= 5 && time < 10 && transform.localScale.x > originalScale.x * 0.75)
        {
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
        }
        else if (time >= 10 && time < 15 && transform.localScale.x > originalScale.x * 0.5)
        {
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
        }
        else if (time >= 15 && time < 20 && transform.localScale.x > originalScale.x * 25)
        {
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
        }
        else if (time >= 20 && transform.localScale.x > originalScale.x * 0.15)
        {
            transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
        }


        // Debug.Log("Current length of the object: " + transform.localScale.x);
        // if (transform.localScale.x > originalScale.x / 4)
        // {
        //     // 如果物件的大小大於原始大小的一半，則縮小物件
        //     transform.localScale -= new Vector3(shrinkSpeed, shrinkSpeed, shrinkSpeed) * Time.deltaTime;
        // }
    }
}
