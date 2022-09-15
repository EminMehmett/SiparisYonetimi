using System;

namespace SiparisYonetimi.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
