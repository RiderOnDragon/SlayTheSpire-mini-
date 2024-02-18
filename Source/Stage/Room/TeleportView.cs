using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class TeleportView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Space(20)]

    [SerializeField] private AnimatorController _battleRoomTeleport;
    [SerializeField] private AnimatorController _campRoomTeleport;

    public void Init(RoomData.Type roomType)
    {
        switch (roomType)
        {
            case RoomData.Type.SIMPLE_ENEMY:
                _animator.runtimeAnimatorController = _battleRoomTeleport;
                break;
            case RoomData.Type.CAMP:
                _animator.runtimeAnimatorController = _campRoomTeleport;
                break;
        }
    }
}
