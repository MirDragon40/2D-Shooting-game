using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    private void Awake()
    {
        // 인스턴스화 된 직후에 호출된다.
        // 주로 게임의 상태나 변수를 초기화할 때 사용
        // 오브젝트들이 렌덤순으로 호출됨
        //Debug.Log("Awake");
    }

    private void OnEnable()
    {
        // 사용 가능할때마다 호출된다. (비활성화 됐다가 활성화 될 때)
        // 사용자가 만든 이벤트를 연결할 때 많이 쓰인다.
        //Debug.Log("OnEnable");
    }

    private void Start()
    {
        // 시작할 때 호출된다.
        // 다른 스크립트의 모든 Awake 모두 실행된 이후에 호출된다.
        //Debug.Log("Start");
    }

    // Input 업데이트

    private void Update()
    {
        // 매 프레임마다 호출된다.
    }

    // 코루틴 업데이트

    private void LateUpdate()
    {
        // 모든 활성화된 스크립트의 Update 함수가 호출되고 나서 한 번씩 호출된다.
        // 카메라가 캐릭터를 따라다니는 상황에서 주로 쓰임: 캐릭터 먼저 움직이고, 카메라 움직임
    }
    private void OnDisable()
    {
        // 사용 불가능할때마다 호출된다. 
        // 이벤트 연결을 종료할 때 사용된다.
        //Debug.Log("OnDisable");
    }
}
