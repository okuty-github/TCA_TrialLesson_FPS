using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Client.Core;
using SoftGear.Strix.Client.Core.Model.Manager.Filter;
using SoftGear.Strix.Client.Core.Model.Manager.Filter.Builder;
using SoftGear.Strix.Client.Core.Request;
using SoftGear.Strix.Client.Core.Time;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Net.Logging;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Unity.Runtime.Event;

namespace Strix
{
    /// <summary>
    /// ���[��
    /// </summary>
    public class StrixRoom
    {
        private bool _isEnteredRoom;
        private CustomizableMatchRoom _currentRoom;
        private RoomMemberProperties _currentMember;

        private List<RoomInfo> _searchRoomInfoList;

        public bool IsEnteredRoom => _isEnteredRoom;
        public CustomizableMatchRoom CurrentRoom => _currentRoom;
        public RoomMemberProperties CurrentMember => _currentMember;

        public List<RoomInfo> SearchRoomInfoList => _searchRoomInfoList;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public StrixRoom()
        {
            _searchRoomInfoList = new List<RoomInfo>();
        }

        /// <summary>
        /// ���[���̍쐬
        /// </summary>
        /// <param name="roomProperties"></param>
        /// <param name="roomMemberProperties"></param>
        /// <param name="requestConfig">�^�C���A�E�g����</param>
        public void CreateRoom(RoomProperties roomProperties, RoomMemberProperties roomMemberProperties, Action onSuccess, Action onFailed, RequestConfig requestConfig = null)
        {
            _currentRoom = null;
            StrixNetwork.instance.CreateRoom(roomProperties, roomMemberProperties,
                args =>
                {
                    _isEnteredRoom = true;
                    _currentRoom = StrixNetwork.instance.room;
                    onSuccess?.Invoke();
                },
                args =>
                {
                    onFailed?.Invoke();
                },
                requestConfig);
        }

        /// <summary>
        /// �w�胋�[���ɎQ��
        /// </summary>
        /// <param name="roomInfo"></param>
        /// <param name="playerName"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void JoinRoom(RoomInfo roomInfo, string playerName, Action onSuccess, Action onFailed)
        {
            // �����o�[�v���p�e�B���쐬
            var memberProperties = StrixUtility.CreateDefaultRoomMemberProperties(playerName);
            memberProperties.properties = new Dictionary<string, object>()
            {
                { "state", 0 }
            };

            // ���[���Q�����쐬
            var roomJoinArgs = new RoomJoinArgs()
            {
                host = roomInfo.host,
                port = roomInfo.port,
                roomId = roomInfo.roomId,
                protocol = roomInfo.protocol,
                memberProperties = memberProperties,
            };

            // ���[���֎Q��
            StrixNetwork.instance.JoinRoom(roomJoinArgs,
                joinArgs =>
                {
                    onSuccess?.Invoke();
                },
                failureArgs =>
                {
                    onFailed?.Invoke();
                });
        }

        /// <summary>
        /// �����_���Ƀ��[���֓���
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="maxRank"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        private void JoinRandomRoom(string playerName, int maxRank, Action onSuccess, Action onFailed)
        {
            // �����o�[�v���p�e�B��ݒ�
            var properties = StrixUtility.CreateDefaultRoomMemberProperties(playerName);
            // �����_���ȃ����N��ݒ�
            properties.properties = new Dictionary<string, object>() { { "Rank", UnityEngine.Random.Range(1, maxRank) } };

            // �����_���ȃ��[���ɓ�������
            StrixNetwork.instance.JoinRandomRoom(properties,
                joinArgs =>
                {
                    _currentRoom = StrixNetwork.instance.room;
                    onSuccess?.Invoke();
                },
                failureArgs =>
                {
                    onFailed?.Invoke();
                });
        }

        /// <summary>
        /// ���[������ގ�
        /// </summary>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void LeaveRoom(Action onSuccess, Action onFailed)
        {
            if (_currentRoom != null)
            {
                StrixNetwork.instance.LeaveRoom(
                    args =>
                    {
                        // ���[���Z�b�V�������������ꍇ�́A���[���Z�b�V������ؒf
                        StrixNetwork.instance.roomSession?.Disconnect();
                        _currentRoom = null;
                        onSuccess?.Invoke();
                    },
                    args =>
                    {
                        onFailed?.Invoke();
                    });
            }
            else
            {
                // ���[���Z�b�V�������������ꍇ�́A���[���Z�b�V������ؒf
                StrixNetwork.instance.roomSession?.Disconnect();
                onFailed?.Invoke();
            }
        }

        /// <summary>
        /// ���[���̍폜�i�I�[�i�[�����j
        /// </summary>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void DeleteRoom(Action onSuccess, Action onFailed)
        {
            if (_currentRoom != null)
            {
                StrixNetwork.instance.DeleteRoom(_currentRoom.GetPrimaryKey(),
                    deleteArgs =>
                    {
                        onSuccess?.Invoke();
                    },
                    failureArgs =>
                    {
                        onFailed?.Invoke();
                    });
            }
            else
            {
                onFailed?.Invoke();
            }
        }

        /// <summary>
        /// ���[�������X�V�i�I�[�i�[�����j
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void UpdateRoomInfo(RoomProperties properties, Action onSuccess, Action onFailed)
        {
            if (StrixNetwork.instance.isRoomOwner)
            {
                StrixNetwork.instance.SetRoom(_currentRoom.GetPrimaryKey(), properties,
                    args =>
                    {
                        onSuccess?.Invoke();
                    },
                    args =>
                    {
                        onFailed?.Invoke();
                    });
            }
        }

        /// <summary>
        /// �w�胋�[���̃����o�[���擾
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void GetRoomMembers(long roomId, Action<ICollection<CustomizableMatchRoomMember>> onSuccess, Action onFailed)
        {
            StrixNetwork.instance.GetRoomMembers(_currentRoom.GetPrimaryKey(),
                args =>
                {
                    onSuccess?.Invoke(args.roomMemberCollection);
                },
                arg =>
                {
                    onFailed?.Invoke();
                });
        }

        /// <summary>
        /// ���[�������o�[�������i�I�[�i�[�����j
        /// </summary>
        /// <param name="targetId"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void RemoveRoomMember(UID targetId, Action onSuccess, Action onFailed)
        {
            if (StrixNetwork.instance.isRoomOwner)
            {
                StrixNetwork.instance.KickRoomMember(targetId,
                    kickArgs =>
                    {
                        onSuccess?.Invoke();
                    },
                    failureArgs =>
                    {
                        onFailed?.Invoke();
                    });
            }
            else
            {
                onFailed?.Invoke();
            }
        }

        /// <summary>
        /// ���[���̌���
        /// </summary>
        /// <param name="searchLimit"></param>
        /// <param name="searchOffset"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        public void SearchRoom(int searchLimit, int searchOffset, Action<ICollection<RoomInfo>> onSuccess, Action onFailed)
        {
            StrixNetwork.instance.SearchRoom(searchLimit, searchOffset,
                searchArgs =>
                {
                    onSuccess?.Invoke(searchArgs.roomInfoCollection);
                },
                failureArgs =>
                {
                    onFailed?.Invoke();
                });
        }

        /// <summary>
        /// ���[���̌���
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFailed"></param>
        /// <param name="searchLimit"></param>
        /// <param name="searchOffset"></param>
        public void SearchRoom(ICondition condition, Order order, Action onSuccess, Action onFailed, int searchLimit = StrixConfig.DEFAULT_SEARCH_ROOM_LIMIT, int searchOffset = StrixConfig.DEFAULT_SEARCH_ROOM_OFFSET)
        {
            _searchRoomInfoList.Clear();
            StrixNetwork.instance.SearchRoom(condition, order, searchLimit, searchOffset,
                searchArgs =>
                {
                    foreach (var info in searchArgs.roomInfoCollection)
                    {
                        _searchRoomInfoList.Add(info);
                    }
                    onSuccess?.Invoke();
                },
                failureArgs =>
                {
                    onFailed?.Invoke();
                });
        }
    }
}
