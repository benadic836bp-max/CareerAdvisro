namespace CareerAdvisor.Models;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
}

public class Career
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<CareerSkill> Requirements { get; set; } = new();
}

// Links a career to a skill: importance (Weight 1-5) and needed proficiency (MinLevel 1-5).
public class CareerSkill
{
    public int Id { get; set; }
    public int CareerId { get; set; }
    public Career Career { get; set; } = null!;
    public int SkillId { get; set; }
    public Skill Skill { get; set; } = null!;
    public int Weight { get; set; }
    public int MinLevel { get; set; }
}

public class SkillGap
{
    public string Skill { get; set; } = "";
    public int Current { get; set; }
    public int Required { get; set; }
}

public class RecommendationResult
{
    public Career Career { get; set; } = null!;
    public double Score { get; set; }
    public List<string> Strengths { get; set; } = new();
    public List<SkillGap> Gaps { get; set; } = new();
}
