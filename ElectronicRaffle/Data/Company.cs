using Sorschia.Data;

namespace ElectronicRaffle.Data
{
    public class Company : DataBase<uint>
    {
        public Company(uint id) : base(id)
        {
        }

        public string Name { get; set; }
    }
}
