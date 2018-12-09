using CsvHelper.Configuration.Attributes;

namespace MTIC.Service.Import
{
    internal class JobFile
    {
        //[Name("案件タイトル")]
        public string JobTitle { get; set; }

        //[Name("契約")]
        public string ContractType { get; set; }

        //[Name("仕事内容")]
        public string JobDesription { get; set; }

        //[Name("勤務時間/日")]
        public string Workinghour { get; set; }

        //[Name("勤務日数/週")]
        public string WorkingdaysPerweek { get; set; }

        //[Name("役割")]
        public string Role { get; set; }

        [Name("給与")]
        public string Salary { get; set; }

        [Name("必要な日本語レベル")]
        public string JapaneseLevel { get; set; }

        [Name("交通費")]
        public string Transporationfee { get; set; }

        [Name("担当者名")]
        public string ContactPerson { get; set; }

        [Name("連絡用Email")]
        public string ContactEmail { get; set; }

        [Name("必要なスタッフ数")]
        public string NeededStaff { get; set; }

        [Name("都道府県")]
        public string Prefrecture { get; set; }
    }
}