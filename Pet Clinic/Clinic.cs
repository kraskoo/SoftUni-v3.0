namespace P08PetClinic
{
    using System;
    using System.Linq;
    using System.Text;

    public class Clinic
    {
        private readonly Room[] rooms;
        private int roomsCount;

        public Clinic(int roomsCount)
        {
            this.RoomsCount = roomsCount;
            this.rooms = new Room[this.roomsCount];
        }

        public int RoomsCount
        {
            get
            {
                return this.roomsCount;
            }

            private set
            {
                if (value % 2 == 0)
                {
                    throw new ArgumentException("Invalid Operation!");
                }

                this.roomsCount = value;
            }
        }

        public bool ReleaseRoom()
        {
            int middle = this.RoomsCount / 2;
            for (int i = middle; i < this.rooms.Length; i++)
            {
                if (this.rooms[i] != null && this.rooms[i].PetInRoom != null)
                {
                    this.rooms[i].PetInRoom = null;
                    return true;
                }
            }

            for (int i = 0; i < middle; i++)
            {
                if (this.rooms[i] != null && this.rooms[i].PetInRoom != null)
                {
                    this.rooms[i].PetInRoom = null;
                    return true;
                }
            }

            return false;
        }

        public bool HasEmptyRoom()
        {
            for (int i = 0; i < this.rooms.Length; i++)
            {
                if (this.rooms[i] == null || this.rooms[i].PetInRoom == null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AddNewPetToRoom(Pet pet)
        {
            int middle = this.RoomsCount / 2;
            if (this.rooms[middle] == null || this.rooms[middle].PetInRoom == null)
            {
                this.SetPetToRoom(pet, middle);
                return true;
            }

            for (int i = middle - 1, j = middle + 1; j < this.RoomsCount; i -= 2, j += 2)
            {
                if (this.rooms[i] == null || this.rooms[i].PetInRoom == null)
                {
                    this.SetPetToRoom(pet, i);
                    return true;
                }

                if (this.rooms[j] == null || this.rooms[j].PetInRoom == null)
                {
                    this.SetPetToRoom(pet, j);
                    return true;
                }
            }

            if (this.rooms.Length > 3)
            {
                for (int i = middle - 2, j = middle + 2; j < this.rooms.Length; i -= 2, j += 2)
                {
                    if (this.rooms[i] == null || this.rooms[i].PetInRoom == null)
                    {
                        this.SetPetToRoom(pet, i);
                        return true;
                    }

                    if (this.rooms[j] == null || this.rooms[j].PetInRoom == null)
                    {
                        this.SetPetToRoom(pet, j);
                        return true;
                    }
                }
            }

            return false;
        }

        public string Print()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < this.rooms.Length; i++)
            {
                if (this.rooms[i] == null || this.rooms[i].PetInRoom == null)
                {
                    output.AppendLine("Room empty");
                }
                else
                {
                    output.AppendLine(this.rooms[i].PetInRoom.ToString());
                }
            }

            return output.ToString().Trim();
        }

        public string Print(int index)
        {
            Room room = this.rooms
                .Where(r => r?.PetInRoom != null)
                .FirstOrDefault(r => r.RoomNumber.Equals(index));
            if (room == null)
            {
                return "Room empty";
            }

            return room.PetInRoom.ToString();
        }

        private void SetPetToRoom(Pet pet, int index)
        {
            if (this.rooms[index] == null)
            {
                this.rooms[index] = new Room(index + 1);
            }

            this.rooms[index].PetInRoom = pet;
        }
    }
}