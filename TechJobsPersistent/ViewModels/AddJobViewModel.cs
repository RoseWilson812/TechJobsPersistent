using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechJobsPersistent.Models;

namespace TechJobsPersistent.ViewModels
{
    public class AddJobViewModel
    {
        [Required(ErrorMessage = "Job name is required.")]
        public string Name { get; set; }
        //public Employer Employer { get; set; }
        [Required(ErrorMessage = "Employer is required.")]
        public int EmployerId { get; set; }
                
        public List<SelectListItem> ListEmployers { get; set; }
        public static List<Employer> SaveEmployers { get; set; }
        public static List<Skill> SaveSkills { get; set; }
        
        public static string[] SelectedSkills {get; set;}
        
        public List<SelectListItem> ListSkills { get; set; }


        public AddJobViewModel(List<Employer> possibleEmployers, List<Skill> possibleSkills)
        {
            
            SaveEmployers = new List<Employer>();
            SaveEmployers = possibleEmployers.GetRange(0, possibleEmployers.Count);

            ListEmployers = new List<SelectListItem>();
            foreach (var emp in possibleEmployers)
            {                
                ListEmployers.Add(new SelectListItem
                {
                    Value = emp.Id.ToString(),
                    Text = emp.Name.ToString()
                });
            }
            SaveSkills = new List<Skill>();
            SaveSkills = possibleSkills.GetRange(0, possibleSkills.Count);

            ListSkills = new List<SelectListItem>();
            foreach (var skill in possibleSkills)
            {                
                ListSkills.Add(new SelectListItem
                {
                    Value = skill.Id.ToString(),
                    Text = skill.Name.ToString()
                });
            }

        }

        public AddJobViewModel()
        {
            
        }
        public  void CreateDropdown()
        {
            ListEmployers = new List<SelectListItem>();
            foreach (var emp in SaveEmployers)
            {
                
                ListEmployers.Add(new SelectListItem
                {
                    Value = emp.Id.ToString(),
                    Text = emp.Name.ToString()
                });
            }
            ListSkills = new List<SelectListItem>();
            foreach (var skill in SaveSkills)
            {
                
                ListSkills.Add(new SelectListItem
                {
                    Value = skill.Id.ToString(),
                    Text = skill.Name.ToString()
                });
            }

        }
    }
}
