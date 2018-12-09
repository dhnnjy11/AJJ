namespace Ajj.Core.Entities
{
    public class VisaJobMap
    {
        public int BusinessStreamId { get; set; }
        public BusinessStream BusinessStream { get; set; }

        public int VisaCategoryId { get; set; }
        public VisaCategory VisaCategory { get; set; }
    }
}