using CareerAdvisor.Data;
using CareerAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerAdvisor.Services;

public class RecommendationService
{
    private readonly AppDbContext _db;
    public RecommendationService(AppDbContext db) => _db = db;

    /// levels: SkillId -> proficiency (0 = none, 5 = expert)
    public async Task<List<RecommendationResult>> RecommendAsync(Dictionary<int, int> levels, int top = 5)
    {
        var careers = await _db.Careers
            .Include(c => c.Requirements).ThenInclude(r => r.Skill)
            .ToListAsync();

        var results = new List<RecommendationResult>();
        foreach (var c in careers)
        {
            double earned = 0, total = 0;
            var res = new RecommendationResult { Career = c };

            foreach (var r in c.Requirements)
            {
                levels.TryGetValue(r.SkillId, out var have);
                // Full credit at/above required level, partial credit below it.
                double ratio = Math.Min(1.0, (double)have / r.MinLevel);
                earned += r.Weight * ratio;
                total += r.Weight;

                if (have >= r.MinLevel) res.Strengths.Add(r.Skill.Name);
                else res.Gaps.Add(new SkillGap { Skill = r.Skill.Name, Current = have, Required = r.MinLevel });
            }

            res.Score = total == 0 ? 0 : Math.Round(earned / total * 100, 1);
            res.Gaps = res.Gaps.OrderByDescending(g => g.Required - g.Current).ToList();
            results.Add(res);
        }

        return results.OrderByDescending(r => r.Score).Take(top).ToList();
    }
}
