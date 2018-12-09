using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirtableApiClient;

namespace Ajj.Infrastructure.Services
{
    class AirtableService
    {
        readonly string baseId = "https://airtable.com/tbltkAXlzPOUxkmsR/viwjUnvpirf4ssY56";
        readonly string appKey = "keypciB5eD21wOK27";
     
        public async Task<AirtableListRecordsResponse> ListRecords(
             string tableName,
             string offset = null,
             IEnumerable<string> fields = null,
             string filterByFormula = null,
             int? maxRecords = null,
             int? pageSize = null,
             IEnumerable<Sort> sort = null,
             string view = null)
        {   
            string errorMessage = null;
            var records = new List<AirtableRecord>();
            AirtableListRecordsResponse response = null;
            using (AirtableBase airtableBase = new AirtableBase(appKey, baseId))
            {
                //
                // Use 'offset' and 'pageSize' to specify the records that you want
                // to retrieve.
                // Only use a 'do while' loop if you want to get multiple pages
                // of records.
                //

                do
                {
                    Task<AirtableListRecordsResponse> task = airtableBase.ListRecords(
                           tableName,
                           offset,
                           fields,
                           filterByFormula,
                           maxRecords,
                           pageSize,
                           sort,
                           view);

                    response = await task;

                    if (response.Success)
                    {
                        records.AddRange(response.Records.ToList());
                        offset = response.Offset;
                    }
                    else if (response.AirtableApiError is AirtableApiException)
                    {
                        errorMessage = response.AirtableApiError.ErrorMessage;
                        break;
                    }
                    else
                    {
                        errorMessage = "Unknown error";
                        break;
                    }
                } while (offset != null);
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Error reporting
            }
            else
            {   
                // Do something with the retrieved 'records' and the 'offset'
                // for the next page of the record list.
            }
            return response;



        }


        public async Task CreateRecordsIntoAirtable()
        {
            using (AirtableBase airtableBase = new AirtableBase(appKey, baseId))
            {
                // Create Attachments list
                //var attachmentList = new List<AirtableAttachment>();
                //attachmentList.Add(new AirtableAttachment { Url = "https://upload.wikimedia.org/wikipedia/en/d/d1/Picasso_three_musicians_moma_2006.jpg" });

                var fields = new Fields();
                fields.AddField("User Email", "Pablo Picasso");
                fields.AddField("Notes", "Spanish expatriate Pablo Picasso was one of the greatest and most influential artists of the 20th century, as well as the co-creator of Cubism.");

                //fields.AddField("Attachments", attachmentList);
                //fields.AddField("On Display?", false);

                Task<AirtableCreateUpdateReplaceRecordResponse> task = airtableBase.CreateRecord("New Users", fields, true);
                var response = await task;

                if (!response.Success)
                {
                    string errorMessage = null;
                    if (response.AirtableApiError is AirtableApiException)
                    {
                        errorMessage = response.AirtableApiError.ErrorMessage;
                    }
                    else
                    {
                        errorMessage = "Unknown error";
                    }
                    // Report errorMessage
                }
                else
                {
                    var record = response.Record;
                    // Do something with your created record.
                }
            }
        }
    }
}
