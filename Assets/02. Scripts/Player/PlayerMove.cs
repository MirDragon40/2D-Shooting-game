using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /**
        ��ǥ: �÷��̾ �̵��ϰ� �ʹ�.
        �ʿ� �Ӽ�: 
         - �̵� �ӵ�
        ����:
        1. Ű���� �Է��� �޴´�.
        2. Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
        3. �̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
     **/

    public float Speed = 3f;    // �̵� �ӵ�: �ʴ� 3��ŭ �̵��ϰڴ�.
    public float SlowSpeed = 2f;

    public const float MinX = -3f;
    public const float MaxX = 3f;
    public const float MinY = -6f;
    public const float MaxY = 0f;

    public Animator MyAnimator;

    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move();

        SpeedUpDown();

    }

    private void Move()
    {
        // transform.Translate(Vector2.up * Speed * Time.deltaTime);
        // (0, 1) * 3 -> (0, 3) * Time.deltaTime
        // deltaTime�� ���ϰ� �Ǹ�, ���������δ� �ʴ� �̵��Ÿ��� �������� �ȴ�.
        // deltaTime�� ������ �� �ð� ������ ��ȯ�Ѵ�.
        // 30fps: d -> 0.03��
        // 60fps: d -> 0.016��

        //  1.Ű���� �Է��� �޴´�.
        //float h = Input.GetAxis("Horizontal");    // ���� �Է°�: -1.0f ~ 0f ~ +1.0f  �¿�Ű�� �Է¿� ���� ���� �ٲ��.
        //float v = Input.GetAxis("Vertical");      // ���� �Է°�: -1.0f ~ 0f ~ +1.0f (Input Manager ����)  ����Ű�� �Է¿� ���� ���� �ٲ��. 
        float h = Input.GetAxisRaw("Horizontal");   // ���� �Է°�: -1.0f, 0f, +1.0f  �¿�Ű�� �Է¿� ���� ���� �ٲ��.
        float v = Input.GetAxisRaw("Vertical");     // ���� �Է°�: -1.0f, 0f, +1.0f (Input Manager ����) ����Ű�� �Է¿� ���� ���� �ٲ��.
                                                    //Debug.Log($"h:{h}, v:{v}");
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isEPressed = Input.GetKeyDown(KeyCode.E);
        bool isQPressed = Input.GetKeyDown(KeyCode.Q);

        // �ִϸ����Ϳ��� �Ķ���� ���� �Ѱ��ش�.
        MyAnimator.SetInteger("h", (int)h);

        // 2.Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
        // Vector2 dir = Vector2.right * h + Vector2.up * v;
        // (1, 0) * h + (0, 1) * v = (h, v)

        // ������ �� �������� ����
        Vector2 dir = new Vector2(h, v);
        //Debug.Log($"����ȭ ��: {dir.magnitude}");
        // �̵� ������ ����ȭ (������ ������ ���̸� 1�� �������)
        dir = dir.normalized;
        //Debug.Log($"����ȭ ��: {dir.magnitude}");

        // 3.�̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
        // Debug.Log(Time.deltaTime);
        // transform.Translate(dir * Speed * Time.deltaTime);
        // ������ �̿��� �̵�
        // ���ο� ��ġ = ���� ��ġ + �ӵ� * �ð�
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;
        // Vector3���� �����µ� dir�� Vector2�� �����س��ұ� ������ ������ ���Ƿ�, ����ȯ�� ���ش�.

        if (isShiftPressed)
        {
            newPosition = transform.position + (Vector3)(dir * SlowSpeed) * Time.deltaTime;
        }

        /**
        if (isEPressed == true)
        {
            Speed += 1f;
            newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;
        }

        if (isQPressed == true)
        {
            Speed -= 1f;
            newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;
            if (Speed < 1)
            {
                Speed = 1f;
            }
        }
        **/

        // ������ �Ѿ�� ��, �ݴ������� �Ѿ����
        if (transform.position.x > MaxX)
        {
            newPosition.x = MinX;
        }
        else if (transform.position.x < MinX)
        {
            newPosition.x = MaxX;
        }

        // ��� 1.
        // newPosition.y = Mathf.Max(MinY, newPosition.y);
        // newPosition.y = Mathf.Min(newPosition.y, MaxY);

        // ��� 2.
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        // ��� 3.
        /**
        if (transform.position.y > MaxY)
        {
            newPosition.y = MaxY;
        }
        else if (transform.position.y < MinY)
        {
            newPosition.y = MinY;
        }
        **/



        //Debug.Log($"x: {newPosition.x}, y: {newPosition.y}");
        transform.position = newPosition;


        // ���� ��ġ ���
       // Debug.Log(transform.position);
        // transform.position = new Vector3(0, 1);  -> �̷��� �ϸ� �� �����Ӹ��� �÷��̾ Ư�� ��ġ�� �����ǹǷ� �̵��� ���� ���� ��.
    }

    private void SpeedUpDown()
    {
        // ��ǥ: Q/E ��ư�� ������ �ӷ��� �ٲٰ� �ʹ�.
        // �Ӽ�
        // - �ӷ� (Speed)
        // ����:

        // 1. Q/E ��ư �Է��� �Ǵ��Ѵ�.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 2. Q ��ư�� ���ȴٸ� ���ǵ� 1�ٿ�
            Speed++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Speed--;
            if (Speed < 1)
            {
                Speed = 1f;
            }
        }

    }

}
