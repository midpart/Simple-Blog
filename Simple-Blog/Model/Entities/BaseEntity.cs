namespace Simple_Blog.Model.Entities
{
    public interface IBaseEntity
    {
        long Id { get; set; }
        int Status { get; set; }
        int VersionNumber { get; set; }
        DateTime CreationDate { get; set; }
        DateTime ModificationDate { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public int VersionNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public static class EntityStatus
        {
            public static int Active { get { return 1; } }
            public static int Inactive { get { return -1; } }
            public static int Delete { get { return -404; } }
        }
    }
}
