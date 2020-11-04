using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using FamilyLINQTraining.DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;

namespace FamilyLINQTraining.DataGeneration
{
    public class DBSeeder
    {
        public static void Seed(IList<Family> families)
        {
            // CleanInterestObjects(families);

            Console.WriteLine("Inserting Interests...");
            AddInterests(families);
            Console.WriteLine("Done!");

            Console.WriteLine("Caching child interests..");
            List<ChildInterest> childInterests = CollectAllChildInterests(families);
            Console.WriteLine("Done!");

            Console.WriteLine("Inserting families..");
            AddFamilies(families);
            Console.WriteLine("Done!");

            Console.WriteLine("Setting child <-> interest relations..");
            SetupChildInterestRelations(childInterests);
            Console.WriteLine("Done!");
        }

        private static void SetupChildInterestRelations(List<ChildInterest> childInterests)
        {
            using (FamilyContext fctxt = new FamilyContext())
            {
                foreach (ChildInterest ci in childInterests)
                {
                    Child child = fctxt.Set<Child>().First(c => c.Id == ci.Child.Id);
                    Interest interest = fctxt.Set<Interest>().First(i => i.Type.Equals(ci.InterestId));
                    fctxt.Set<ChildInterest>().Add(new ChildInterest
                    {
                        Child = child,
                        Interest = interest
                    });
                    fctxt.SaveChanges();
                }
            }
        }

        private static void AddFamilies(IList<Family> families)
        {
            foreach (Family family in families)
            {
                using (FamilyContext fctxt = new FamilyContext())
                {
                    fctxt.Families.Add(family);

                    fctxt.Entry(family).State = EntityState.Added;
                    // fctxt.Entry(family).State = EntityState.Detached;
                    fctxt.SaveChanges();
                }
            }
        }

        private static List<ChildInterest> CollectAllChildInterests(IList<Family> families)
        {
            List<ChildInterest> childInterests =
                families.SelectMany(f => f.Children).SelectMany(child => child.ChildInterests).ToList();
            foreach (Family family in families)
            {
                foreach (Child child in family.Children)
                {
                    child.ChildInterests = null;
                }
            }

            return childInterests;
        }


        private static void FixChildInterests(IList<Family> families)
        {
            foreach (Family family in families)
            {
                foreach (Child child in family.Children)
                {
                    List<Interest> interests = new List<Interest>();
                    foreach (ChildInterest childInterest in child.ChildInterests)
                    {
                    }
                }
            }
        }

        private static void AddInterests(IEnumerable<Family> families)
        {
            foreach (Family family in families)
            {
                List<Interest> interests = family.Children.SelectMany(x => x.ChildInterests)
                    .Select(childInterest => childInterest.Interest).ToList();

                foreach (Interest entity in interests)
                {
                    using (FamilyContext familyContext = new FamilyContext())
                    {
                        try
                        {
                            Interest local = familyContext.Set<Interest>()
                                .Local
                                .FirstOrDefault(entry => entry.Type.Equals(entity.Type));
                            if (local != null)
                            {
                                // detach
                                familyContext.Entry(local).State = EntityState.Detached;
                            }

                            if (!familyContext.Set<Interest>().Any(e => e.Type.Equals(entity.Type)))
                            {
                                familyContext.Set<Interest>().Add(entity);
                                // Console.WriteLine($"Added interest: {interestType}");

                                // familyContext.Entry(entity).State = EntityState.Modified;
                            }
                        }
                        catch (Exception e)
                        {
                            // Console.WriteLine("Failed when adding " + interestType);
                            Console.WriteLine(e);
                            throw;
                        }

                        familyContext.SaveChanges();
                    }
                }
            }
        }

        public static IList<Family> ReadJsonFromFile(string path)
        {
            IList<Family> families;
            using (var jsonReader = File.OpenText(path))
            {
                families = JsonSerializer.Deserialize<List<Family>>(jsonReader.ReadToEnd());
            }

            return families;
        }
    }
}