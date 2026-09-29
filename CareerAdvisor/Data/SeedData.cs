using CareerAdvisor.Models;

namespace CareerAdvisor.Data;

public static class SeedData
{
    public static void Run(AppDbContext db)
    {
        if (db.Skills.Any()) return;

        var skills = new (string Name, string Cat)[]
        {
            ("C#", "Programming"), ("Python", "Programming"), ("JavaScript", "Programming"),
            ("SQL", "Data"), ("Statistics", "Data"), ("Machine Learning", "Data"),
            ("HTML/CSS", "Web"), ("ASP.NET", "Web"), ("Cloud (Azure/AWS)", "Infrastructure"),
            ("Networking", "Infrastructure"), ("Security", "Infrastructure"),
            ("UI/UX Design", "Design"), ("Communication", "Soft Skills"),
            ("Problem Solving", "Soft Skills"), ("Project Management", "Soft Skills"),
        }.Select(s => new Skill { Name = s.Name, Category = s.Cat }).ToList();
        db.Skills.AddRange(skills);
        db.SaveChanges();

        int Id(string n) => skills.First(s => s.Name == n).Id;

        Career Make(string name, string desc, params (string Skill, int W, int Min)[] reqs) => new()
        {
            Name = name, Description = desc,
            Requirements = reqs.Select(r => new CareerSkill { SkillId = Id(r.Skill), Weight = r.W, MinLevel = r.Min }).ToList()
        };

        db.Careers.AddRange(
            Make("Backend Developer", "Builds APIs, services and databases.",
                ("C#", 5, 4), ("ASP.NET", 5, 4), ("SQL", 4, 3), ("Problem Solving", 3, 3), ("Cloud (Azure/AWS)", 2, 2)),
            Make("Frontend Developer", "Builds interactive user interfaces.",
                ("JavaScript", 5, 4), ("HTML/CSS", 5, 4), ("UI/UX Design", 3, 3), ("Problem Solving", 3, 3), ("Communication", 2, 3)),
            Make("Data Scientist", "Extracts insight and builds predictive models.",
                ("Python", 5, 4), ("Statistics", 5, 4), ("Machine Learning", 5, 3), ("SQL", 3, 3), ("Communication", 2, 3)),
            Make("Data Analyst", "Turns data into reports and decisions.",
                ("SQL", 5, 4), ("Statistics", 4, 3), ("Python", 3, 3), ("Communication", 4, 3)),
            Make("Cloud Engineer", "Designs and runs cloud infrastructure.",
                ("Cloud (Azure/AWS)", 5, 4), ("Networking", 4, 3), ("Security", 3, 3), ("Python", 2, 2)),
            Make("Cybersecurity Analyst", "Protects systems and responds to threats.",
                ("Security", 5, 4), ("Networking", 5, 4), ("Problem Solving", 3, 3), ("Python", 2, 2)),
            Make("UI/UX Designer", "Designs usable, attractive products.",
                ("UI/UX Design", 5, 4), ("HTML/CSS", 3, 3), ("Communication", 4, 4)),
            Make("Project Manager", "Leads teams and delivers projects.",
                ("Project Management", 5, 4), ("Communication", 5, 4), ("Problem Solving", 3, 3))
        );
        db.SaveChanges();
    }
}
