using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

namespace Strix
{
    public class StrixLoginUI : MonoBehaviour
    {
        [SerializeField] private InputField _inputPlayerName = null;

        public void OnClick()
        {
            var playerName = _inputPlayerName.text;
            StrixManager.Instance.Server.Connect(SoftGear.Strix.Net.Logging.Level.INFO,
                StrixConfig.APPLICATION_ID, StrixConfig.HOST, StrixConfig.PORT, playerName,
                ()=>
                {
                    // ルームの検索
                    StrixManager.Instance.Room.SearchRoom(10, 0,
                        (rommInfoList) =>
                        {
                            if (rommInfoList.Count > 0)
                            {
                                // ルーム参加
                                RoomInfo info = null;
                                foreach (var roomInfo in rommInfoList)
                                {
                                    info = roomInfo;
                                    break;
                                }
                                StrixManager.Instance.Room.JoinRoom(info, playerName,
                                    () =>
                                    {
                                        gameObject.SetActive(false);
                                    },
                                    () =>
                                    {
                                    });
                            }
                            else
                            {
                                // ルーム作成
                                var roomProperties = StrixUtility.CreateDefaultRoomProperties("Room-0");
                                var roomMemberProperties = StrixUtility.CreateDefaultRoomMemberProperties(playerName);
                                StrixManager.Instance.Room.CreateRoom(roomProperties, roomMemberProperties,
                                    () =>
                                    {
                                        gameObject.SetActive(false);
                                    },
                                    () =>
                                    {
                                    });
                            }
                        },
                        () =>
                        {
                        });
                },
                ()=>
                {
                });
        }
    }
}
