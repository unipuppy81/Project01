using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public AttackPattern[] attackPatterns;

    // 현재 실행 중인 패턴의 인덱스
    private int currentPatternIndex = 0;

    // 보스가 다음 패턴으로 넘어갈 때까지의 대기 시간
    public float timeBetweenPatterns = 5f;
    private float timeSinceLastPattern;

    void Start()
    {
        // 초기화 코드 등을 여기에 추가할 수 있습니다.
    }

    void Update()
    {
        // 일정 시간마다 다음 패턴으로 전환
        timeSinceLastPattern += Time.deltaTime;
        if (timeSinceLastPattern >= timeBetweenPatterns)
        {
            // 다음 패턴으로 전환
            SwitchToNextPattern();
        }
    }

    void SwitchToNextPattern()
    {
        // 현재 패턴을 비활성화
        if (attackPatterns.Length > 0)
        {
            attackPatterns[currentPatternIndex].StopPattern();

            // 다음 패턴 인덱스로 이동
            currentPatternIndex = (currentPatternIndex + 1) % attackPatterns.Length;

            // 새로운 패턴을 시작
            attackPatterns[currentPatternIndex].StartPattern();

            // 대기 시간 초기화
            timeSinceLastPattern = 0f;
        }
    }
}


[System.Serializable]
public class AttackPattern
{
    public string patternName;

    public void StartPattern()
    {

    }

    public void StopPattern()
    {

    }
}
