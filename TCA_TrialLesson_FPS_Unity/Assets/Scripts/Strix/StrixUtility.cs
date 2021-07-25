using SoftGear.Strix.Unity.Runtime;

namespace Strix
{
    public static class StrixUtility
    {
        public static RoomProperties CreateDefaultRoomProperties(string roomName)
        {
            var roomProperties = new RoomProperties();
            roomProperties.name = roomName;
            roomProperties.capacity = StrixConfig.DEFAULT_ROOM_CAPACITY;
            return roomProperties;
        }

        public static RoomMemberProperties CreateDefaultRoomMemberProperties(string playerName)
        {
            var roomMemberProperties = new RoomMemberProperties();
            roomMemberProperties.name = playerName;
            return roomMemberProperties;
        }
    }
}
