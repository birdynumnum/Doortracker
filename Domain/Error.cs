using System;

namespace Domain
{
    public class Error : IEntityBase
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; }
        public string StackTrace { get; set; }

        public Error()
        {

        }
    }
}
