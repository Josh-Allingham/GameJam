using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Base Setup")]
    public float walkingSpeed = 4f;
    public float runningSpeed = 8f;
    public float turnSpeed = 10f;

    CharacterController charController;
    Vector3 moveDir = Vector3.zero;

    bool canMove = true;

    private Vector2 mInput;
    private Quaternion freeRotation;
    private Vector3 targetDir;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charController = GetComponent<CharacterController>();

        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = false;
        isRunning = Input.GetKey(KeyCode.LeftShift);

        mInput.x = Input.GetAxis("Horizontal");
        mInput.y = Input.GetAxis("Vertical");

        float currentSpeed = canMove ? (isRunning ? runningSpeed : walkingSpeed) : 0;
        float moveDirY = moveDir.y;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(vertical<0)
        {
            vertical *= -1; ;
        }
        if (horizontal < 0)
        {
            horizontal *= -1; ;
        }

        float move = vertical + horizontal;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        moveDir = forward * move;
        moveDir.Normalize();
        moveDir *= currentSpeed;

        updateTargetDir();

        if(mInput != Vector2.zero && targetDir.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDir.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
            var difRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;

            if(difRotation < 0 || difRotation > 0)
            {
                eulerY = freeRotation.eulerAngles.y;
            }
            var euler = new Vector3(0, eulerY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * Time.deltaTime);
        }

        if(mInput != Vector2.zero)
        {
            charController.Move(moveDir * Time.deltaTime);
        }

    }

    public void updateTargetDir()
    {
        var forward = Camera.main.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        var right = Camera.main.transform.TransformDirection(Vector3.right);

        targetDir = mInput.x * right + mInput.y * forward;
    }

}
