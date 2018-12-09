using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ajj.Core.Entities
{
    /// <summary>
    /// This has different Stream of Business
    /// </summary>
    [Table("businessstream")]
    public class BusinessStream
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_jp { get; set; }
        public string CategoryImageUrl { get; set; }
        public bool HasJob { get; set; }

        public List<VisaJobMap> visajobs = new List<VisaJobMap>();
    }
}