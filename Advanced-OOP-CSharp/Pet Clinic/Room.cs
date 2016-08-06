namespace P08PetClinic
{
    using System;

    public class Room : IComparable<Room>
    {
        public Room(int roomNumber, Pet petInRoom)
        {
            this.RoomNumber = roomNumber;
            this.PetInRoom = petInRoom;
        }

        public Room(int roomNumber)
        {
            this.RoomNumber = roomNumber;
        }

        public int RoomNumber { get; }

        public Pet PetInRoom { get; set; }

        public int CompareTo(Room other)
        {
            return this.RoomNumber.CompareTo(other.RoomNumber);
        }
    }
}