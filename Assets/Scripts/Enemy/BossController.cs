using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public AttackPattern[] attackPatterns;

    // ���� ���� ���� ������ �ε���
    private int currentPatternIndex = 0;

    // ������ ���� �������� �Ѿ �������� ��� �ð�
    public float timeBetweenPatterns = 5f;
    private float timeSinceLastPattern;

    void Start()
    {
        // �ʱ�ȭ �ڵ� ���� ���⿡ �߰��� �� �ֽ��ϴ�.
    }

    void Update()
    {
        // ���� �ð����� ���� �������� ��ȯ
        timeSinceLastPattern += Time.deltaTime;
        if (timeSinceLastPattern >= timeBetweenPatterns)
        {
            // ���� �������� ��ȯ
            SwitchToNextPattern();
        }
    }

    void SwitchToNextPattern()
    {
        // ���� ������ ��Ȱ��ȭ
        if (attackPatterns.Length > 0)
        {
            attackPatterns[currentPatternIndex].StopPattern();

            // ���� ���� �ε����� �̵�
            currentPatternIndex = (currentPatternIndex + 1) % attackPatterns.Length;

            // ���ο� ������ ����
            attackPatterns[currentPatternIndex].StartPattern();

            // ��� �ð� �ʱ�ȭ
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
