using System;
using System.Collections.Generic;

namespace SampleEFApp.Model
{
    public partial class Skill
    {
        public Skill()
        {
            EmployeeSkills = new HashSet<EmployeeSkill>();
        }

        public int SkillId { get; set; }
        public string? Skill1 { get; set; }
        public string? SkillDescription { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
