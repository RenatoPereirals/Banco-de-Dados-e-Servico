// using Bsd.Domain.Entities;
// using Bsd.Domain.Enums;
// using Bsd.Infrastructure.Context;
// using Microsoft.EntityFrameworkCore;

// namespace Bsd.Infrastructure.Data
// {
//     public class RubricSeeder
//     {
//         public void Seed(BsdDbContext context, Action<DbSet<Rubric>, List<Rubric>> addEntitiesIfNotExists)
//         {
//             var rubrics = GetRubrics();
//             addEntitiesIfNotExists(context.Rubrics, rubrics);
//         }

//         private List<Rubric> GetRubrics()
//         {
//             return new List<Rubric>
//             {
//                 new()
//                 {
//                     Code = "1902",
//                     Description = "3h/dia de 50% (hora extra)",
//                     HoursPerDay = 3.0M,
//                     DayType = DayType.Workday,
//                     ServiceType = ServiceType.P140
//                 },
//                 new()
//                 {
//                     Code = "1335",
//                     Description = "3h/dia de 50% (hora extra)",
//                     HoursPerDay = 3.0M,
//                     DayType = DayType.Workday,
//                     ServiceType = ServiceType.P110
//                 },
//             };
//         }
//     }
// }