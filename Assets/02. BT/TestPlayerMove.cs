using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class TestPlayerMove : Action
    {
        public float moveSpeed = 5f; // 이동 속도
        private Vector2 randomPosition; // 랜덤 좌표를 저장할 변수

        public override void OnStart()
        {
            // 처음에 한 번 랜덤한 좌표를 생성
            float randomX = Random.Range(-10f, 10f); // X 좌표의 범위는 -10부터 10까지
            float randomY = Random.Range(-10f, 10f); // Y 좌표의 범위는 -10부터 10까지
            randomPosition = new Vector2(randomX, randomY);
        }

        public override TaskStatus OnUpdate()
        {
            // 현재 위치에서 랜덤 좌표까지 일정한 속도로 이동
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, randomPosition, moveSpeed * Time.deltaTime);

            // 이동한 위치로 설정
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

            // 목표 위치에 도달했다면 이동이 완료되었다고 간주
            if (currentPosition == randomPosition)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Running;
            }
        }
    }
}

