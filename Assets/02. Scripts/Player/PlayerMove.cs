using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /**
        목표: 플레이어를 이동하고 싶다.
        필요 속성: 
         - 이동 속도
        순서:
        1. 키보드 입력을 받는다.
        2. 키보드 입력에 따라 이동할 방향을 계산한다.
        3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
     **/

    public float Speed = 3f;    // 이동 속도: 초당 3만큼 이동하겠다.
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
        // deltaTime을 곱하게 되면, 최종적으로는 초당 이동거리가 같아지게 된다.
        // deltaTime은 프레임 간 시간 간격을 반환한다.
        // 30fps: d -> 0.03초
        // 60fps: d -> 0.016초

        //  1.키보드 입력을 받는다.
        //float h = Input.GetAxis("Horizontal");    // 수평 입력값: -1.0f ~ 0f ~ +1.0f  좌우키의 입력에 따라 값이 바뀐다.
        //float v = Input.GetAxis("Vertical");      // 수직 입력값: -1.0f ~ 0f ~ +1.0f (Input Manager 참고)  상하키의 입력에 따라 값이 바뀐다. 
        float h = Input.GetAxisRaw("Horizontal");   // 수평 입력값: -1.0f, 0f, +1.0f  좌우키의 입력에 따라 값이 바뀐다.
        float v = Input.GetAxisRaw("Vertical");     // 수직 입력값: -1.0f, 0f, +1.0f (Input Manager 참고) 상하키의 입력에 따라 값이 바뀐다.
                                                    //Debug.Log($"h:{h}, v:{v}");
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isEPressed = Input.GetKeyDown(KeyCode.E);
        bool isQPressed = Input.GetKeyDown(KeyCode.Q);

        // 애니메이터에게 파라미터 값을 넘겨준다.
        MyAnimator.SetInteger("h", (int)h);

        // 2.키보드 입력에 따라 이동할 방향을 계산한다.
        // Vector2 dir = Vector2.right * h + Vector2.up * v;
        // (1, 0) * h + (0, 1) * v = (h, v)

        // 방향을 각 성분으로 제작
        Vector2 dir = new Vector2(h, v);
        //Debug.Log($"정규화 전: {dir.magnitude}");
        // 이동 방향을 정규화 (방향은 같지만 길이를 1로 만들어줌)
        dir = dir.normalized;
        //Debug.Log($"정규화 후: {dir.magnitude}");

        // 3.이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
        // Debug.Log(Time.deltaTime);
        // transform.Translate(dir * Speed * Time.deltaTime);
        // 공식을 이용한 이동
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;
        // Vector3값이 나오는데 dir을 Vector2로 정의해놓았기 때문에 오류가 나므로, 형변환을 해준다.

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

        // 범위를 넘어섰을 때, 반대쪽으로 넘어가도록
        if (transform.position.x > MaxX)
        {
            newPosition.x = MinX;
        }
        else if (transform.position.x < MinX)
        {
            newPosition.x = MaxX;
        }

        // 방법 1.
        // newPosition.y = Mathf.Max(MinY, newPosition.y);
        // newPosition.y = Mathf.Min(newPosition.y, MaxY);

        // 방법 2.
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        // 방법 3.
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


        // 현재 위치 출력
       // Debug.Log(transform.position);
        // transform.position = new Vector3(0, 1);  -> 이렇게 하면 매 프레임마다 플레이어가 특정 위치로 고정되므로 이동할 수가 없게 됨.
    }

    private void SpeedUpDown()
    {
        // 목표: Q/E 버튼을 누르면 속력을 바꾸고 싶다.
        // 속성
        // - 속력 (Speed)
        // 순서:

        // 1. Q/E 버튼 입력을 판단한다.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 2. Q 버튼이 눌렸다면 스피드 1다운
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
