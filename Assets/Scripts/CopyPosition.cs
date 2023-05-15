using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{  //[미니맵] 타겟을 따라 다니는 카메라 스크립트
    [SerializeField] private bool x, y, z; // 이 값이 true면 target의 좌표, false면 현재 좌표 그대로 사용
    [SerializeField] private Transform target; //쫒아가야할 대상 Transform
   

    // Update is called once per frame
    void Update()
    {
        //쫒아가야할 대상이 없으면 종료
        if (!target) return;

        transform.position = new Vector3((x ? target.position.x : transform.position.x), (y ? target.position.y : transform.position.y), (z ? target.position.z : transform.position.z));
    }
}
