using System;

namespace Ajj.Core.Entities.MarketoAPI
{
    public class MarketoResponse
    {
        public string requestId { get; set; }
        public Result[] result { get; set; }
        public bool success { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string IdField { get; set; }
        public string[] DedupeFields { get; set; }
        public string[][] SearchableFields { get; set; }
        public Field[] Fields { get; set; }
    }

    public class Field
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public bool Updateable { get; set; }
        public int Length { get; set; }
    }
}