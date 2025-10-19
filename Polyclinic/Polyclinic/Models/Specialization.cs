namespace Polyclinic.Models
{
    public class Specialization
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Specialization name
        /// </summary>
        public required string Name { get; set; }
    }
 }
