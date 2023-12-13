using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class TestPlayerMove : Action
    {
        public float moveSpeed = 5f; // 이동 속도
        public SharedGameObject player;
        private Dice playerDice;

        public override void OnStart()
        {
            if (DiceManager.Instance.TryGetDice(player.Value.GetComponent<IDiceUnit>().myPos, out Dice dice))
            {
                playerDice = dice;
            }
        }

        public override TaskStatus OnUpdate()
        {
            // 현재 위치에서 랜덤 좌표까지 일정한 속도로 이동
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = Vector3.MoveTowards(currentPosition, playerDice.transform.position, moveSpeed * Time.deltaTime);

            // 이동한 위치로 설정
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

            // 목표 위치에 도달했다면 이동이 완료되었다고 간주
            if (currentPosition == playerDice.transform.position)
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

