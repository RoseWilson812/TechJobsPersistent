using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet("/Add")]
        public IActionResult AddJob()
        {
            List<Employer> allEmployers = context.Employers.ToList();
            List<Skill> allSkills = context.Skills.ToList();
            AddJobViewModel addJobViewModel = new AddJobViewModel(allEmployers, allSkills);

            return View(addJobViewModel);
        }
        [HttpPost]
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills)
        {
            if (selectedSkills.Length == 0)
            {
                ViewBag.ErrorMessage = "Please select at least one Skill.";
                addJobViewModel.CreateDropdown();
                return View("AddJob", addJobViewModel);
            }
            if (ModelState.IsValid)
            {
                var holdJob = new Job //no Id yet; 
                
                {
                    Name = addJobViewModel.Name,
                    EmployerId = addJobViewModel.EmployerId

                };

                context.Jobs.Add(holdJob);
                
                
                foreach (var skill in selectedSkills)
                {
                    var holdJobSkill = new JobSkill 
                    {
                        Job = holdJob, 
                        SkillId = int.Parse(skill)
                    };
                    
                    context.JobSkills.Add(holdJobSkill);
                    
                }

                context.SaveChanges();
                return Redirect("/Add");
            }
            
            addJobViewModel.CreateDropdown();
            return View("AddJob", addJobViewModel);
        }

       

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
