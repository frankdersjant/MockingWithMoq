using System;

namespace Domain
{
    public class Dive : IEntityBase
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int Depth { get; set; }
        public int Visibility { get; set; }
      

        public Dive()
        {

        }

        public Dive(int id, string location, int depth, int visibility) : base()
        {
            Id = id;
            Location = location;
            Depth = depth;
            Visibility = visibility;
        }
    }
}
