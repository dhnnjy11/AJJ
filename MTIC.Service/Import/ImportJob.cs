using Ajj.Core.Entities;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MTIC.Service.Import
{
    public class ImportJob
    {
        public bool ImportCSV(string csvfile)
        {
            Encoding unicode = Encoding.Unicode;
            TextReader textreader = new StreamReader(csvfile);

            var csv = new CsvReader(textreader);
            csv.Configuration.HeaderValidated = null;

            while (csv.Read())
            {
                //   var str = csv.GetField<string>(0);
                //csv.ValidateHeader<JobFile>();
                var str = csv.GetField<string>(0);

                var record = csv.GetRecord<JobFile>();
            }
            return true;
        }
    }

    public class ImportGBUsers
    {
        public IEnumerable<GBUser> ImportCSV(string csvfile)
        {
            Encoding unicode = Encoding.Unicode;

            TextReader textreader = new StreamReader(csvfile);

            var csv = new CsvReader(textreader);
            csv.Configuration.HeaderValidated = null;
            csv.Configuration.MissingFieldFound = null;
            IEnumerable<GBUser> records = csv.GetRecords<GBUser>();
            return records;
        }
    }
}