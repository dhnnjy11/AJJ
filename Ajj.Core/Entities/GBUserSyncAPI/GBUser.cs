﻿using System.ComponentModel.DataAnnotations;

namespace Ajj.Core.Entities
{
    public class GBUser
    {
        public GBUser() { }
        public GBUser(JobSeeker jobseeker)
        {
            first_name = jobseeker.FirstName;
            last_name = jobseeker.LastName;
            birth_year = jobseeker.BirthYear;
            birth_month = jobseeker.BirthMonth;
            birth_day = jobseeker.BirthDay;
            birth_age = jobseeker.Age;
            user_email = jobseeker.ApplicationUser.Email;
            user_pass = jobseeker.ApplicationUser.PasswordHash;
            zip = jobseeker.PostalAddrss;
            visa = jobseeker.Visa;
            addr1 = jobseeker.Address;
            birth_age = jobseeker.Age;
            thestate_other = jobseeker.OtherCountry;
            mobile = jobseeker.ApplicationUser.PhoneNumber;
            city = jobseeker.City + "" + jobseeker.Town;
            if (jobseeker.Gender == 'M')
            {
                radio_sex = "Male";
            }
            else if (jobseeker.Gender == 'F')
            {
                radio_sex = "Female";
            }
            if(jobseeker.VisaCategory != null)
            {
                visa = jobseeker.VisaCategory.Name;

                if (visa == "Designated Activities")
                {
                    vopen1 = jobseeker.VisaCategory.SubCategory;
                }
                else if (visa == "Skilled Labor")
                {
                    vopen5 = jobseeker.VisaCategory.SubCategory;
                }
                else if (visa == "Other Visa")
                {
                    vopen6 = jobseeker.VisaCategory.SubCategory;
                }
                else if (visa == "Student")
                {
                    vopen3 = (jobseeker.IsPermitToWork == false) ? "No" : "Yes";
                }
                else if (visa == "Dependent")
                {
                    vopen2 = (jobseeker.IsPermitToWork == false) ? "No" : "Yes";
                }

            }
            if(jobseeker.Country != null)
            {
                country = jobseeker.Country.Name;
            }

            if (jobseeker.Province != null)
            {
                thestate = jobseeker.Province.Name;
            }
    

                

           
        }
        public string user_id { get; set; }

        [Required(ErrorMessage ="User email is required")]
        public string user_email { get; set; }

        public string user_pass { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string radio_sex { get; set; }
        public string birth_year { get; set; }
        public string birth_month { get; set; }
        public string birth_day { get; set; }
        public string birth_age { get; set; }
        public string country { get; set; } //nationality
        public string visa { get; set; }
        public string zip { get; set; }
        public string thestate { get; set; } //province, fill with province id
        public string city { get; set; } // ajj => city + town
        public string addr1 { get; set; }
        public string thestate_other { get; set; }

        //this is not present in ajj
        public string nickname { get; set; }

        public string description { get; set; }
        public string radio_kiyaku { get; set; }
        public string radio_move { get; set; }
        public string vopen1 { get; set; }
        public string vopen2 { get; set; }
        public string vopen3 { get; set; }
        public string vopen4 { get; set; }
        public string vopen5 { get; set; }
        public string vopen6 { get; set; }
        public string visa_day { get; set; }
        public string visa_month { get; set; }
        public string visa_year { get; set; }
        public string visa_image { get; set; }
        public string movearea { get; set; }
        public string station { get; set; }
        public string tel { get; set; }
        public string mobile { get; set; }
        public string snsid { get; set; }
        public string snsid2 { get; set; }
        public string radio_contact { get; set; }
        public string radio_current { get; set; }
        public string current_other { get; set; }
        public string radio_edu1 { get; set; }
        public string edu1_other { get; set; }
        public string edu1_year { get; set; }
        public string radio_edu1_status { get; set; }
        public string edu1_school { get; set; }
        public string edu1_major { get; set; }
        public string radio_edu2 { get; set; }
        public string edu2_other { get; set; }
        public string edu2_year { get; set; }
        public string radio_edu2_status { get; set; }
        public string edu2_school { get; set; }
        public string edu2_major { get; set; }
        public string checkbox_job { get; set; }
        public string job_other { get; set; }
        public string period_sty { get; set; }
        public string period_stm { get; set; }
        public string period_eny { get; set; }
        public string period_enm { get; set; }
        public string we1_period_sty { get; set; }
        public string we1_period_stm { get; set; }
        public string we1_period_eny { get; set; }
        public string we1_period_enm { get; set; }
        public string we1_company { get; set; }
        public string we1_country { get; set; }
        public string we1_city { get; set; }
        public string we1_type { get; set; }
        public string we2_period_sty { get; set; }
        public string we2_period_stm { get; set; }
        public string we2_period_eny { get; set; }
        public string we2_period_enm { get; set; }
        public string we2_company { get; set; }
        public string we2_country { get; set; }
        public string we2_city { get; set; }
        public string we2_type { get; set; }
        public string skill1 { get; set; }
        public string skill1_year { get; set; }
        public string skill2 { get; set; }
        public string skill2_year { get; set; }
        public string skill3 { get; set; }
        public string skill3_year { get; set; }
        public string drivelicence { get; set; }
        public string radio_car { get; set; }
        public string radio_lang { get; set; }
        public string lang_other { get; set; }
        public string radio_lang_level { get; set; }
        public string radio_lang_jlpt { get; set; }
        public string radio_lang_eng { get; set; }
        public string other_lang1 { get; set; }
        public string other_lang1_level { get; set; }
        public string other_lang2 { get; set; }
        public string other_lang2_level { get; set; }
        public string textarea_pr { get; set; }
        public string select_question1 { get; set; }
        public string textarea_question2 { get; set; }
        public string textarea_question3 { get; set; }
    }
}