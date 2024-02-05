using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Animator animatorController;
    public Transform rotateYTransform;
    public Transform rotateXTransform;
    public float rotateSpeed;
    public float currentRotateX = 0;
    public float MoveSpeed;
    float currentSpeed = 0;
    public ShrinkToHalf shrinkCircle; // ShrinkToHalf 物件的參考
    public float damagePerSecond = 10f; // 每秒造成的傷害

    public Rigidbody rigidBody;

    public JumpSensor JumpSensor;
    public float JumpSpeed;
    public GunManager gunManager;
    public GameUIManager uiManager;
    public int hp = 100;
    private float lastHitTime;

    // Use this for initialization
    void Start()
    {
        animatorController = this.GetComponent<Animator>();
    }

    public void Hit(int value)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= value;
        uiManager.SetHP(hp);
        if (hp > 0)
        {
            uiManager.PlayHitAnimation();
        }
        else
        {
            uiManager.PlayerDiedAnimation();
            rigidBody.gameObject.GetComponent<Collider>().enabled = false;
            rigidBody.useGravity = false;
            rigidBody.velocity = Vector3.zero;
            this.enabled = false;
            rotateXTransform.transform.DOLocalRotate(new Vector3(-60, 0, 0), 0.5f);
            rotateYTransform.transform.DOLocalMoveY(-1.5f, 0.5f).SetRelative(true);
        }
    }

    bool IsOutsideShrinkCircle()
    {
        Vector3 playerPosition = transform.position;
        Vector3 circlePosition = shrinkCircle.transform.position;
        Vector3 playerPositionXZ = new Vector3(playerPosition.x, 0, playerPosition.z);
        Vector3 circlePositionXZ = new Vector3(circlePosition.x, 0, circlePosition.z);
        float distanceToCenter = Vector3.Distance(playerPositionXZ, circlePositionXZ);
        // 計算玩家與 ShrinkToHalf 物件中心的距離
        // float distanceToCenter = Vector3.Distance(transform.position, shrinkCircle.transform.position);
        Debug.Log("player distance: " + distanceToCenter);
        Debug.Log("circle distance: " + shrinkCircle.transform.localScale.x);

        // 如果距離大於 ShrinkToHalf 物件的半徑，則玩家在物件的外部
        return distanceToCenter/100 > shrinkCircle.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Renderer renderer = shrinkCircle.GetComponent<Renderer>();
        // Vector3 size = renderer.bounds.size;
        // Debug.Log("Length of shrinkCircle: " + size.x);

        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.K))
        {
            gunManager.TryToTriggerGun();
        }

        //決定鍵盤input的結果
        Vector3 movDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movDirection.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movDirection.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { movDirection.x += 1; }
        if (Input.GetKey(KeyCode.A)) { movDirection.x -= 1; }
        movDirection = movDirection.normalized;

        //決定要給Animator的動畫參數
        if (movDirection.magnitude == 0 || !JumpSensor.IsCanJump()) { currentSpeed = 0; }
        else
        {
            if (movDirection.z < 0) { currentSpeed = -MoveSpeed; }
            else { currentSpeed = MoveSpeed; }
        }
        animatorController.SetFloat("Speed", currentSpeed);

        //轉換成世界座標的方向
        Vector3 worldSpaceDirection = movDirection.z * rotateYTransform.transform.forward +
                                      movDirection.x * rotateYTransform.transform.right;
        Vector3 velocity = rigidBody.velocity;
        velocity.x = worldSpaceDirection.x * MoveSpeed;
        velocity.z = worldSpaceDirection.z * MoveSpeed;

        if (Input.GetKey(KeyCode.Space) && JumpSensor.IsCanJump())
        {
            velocity.y = JumpSpeed;
        }

        rigidBody.velocity = velocity;

        //計算滑鼠
        rotateYTransform.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotateSpeed;
        currentRotateX += Input.GetAxis("Vertical") * rotateSpeed;

        if (currentRotateX > 90)
        {
            currentRotateX = 90;
        }
        else if (currentRotateX < -90)
        {
            currentRotateX = -90;
        }
        rotateXTransform.transform.localEulerAngles = new Vector3(-currentRotateX, 0, 0);

        if (IsOutsideShrinkCircle() && Time.time - lastHitTime >= 1f)
        {
            // 如果玩家在物件的外部，則對玩家造成傷害
            Debug.Log("hihihihihihihihihihihihihihihih");
            // Hit((int)(damagePerSecond * Time.deltaTime));
            Hit((int)(2));
            lastHitTime = Time.time;
        }

    }
}