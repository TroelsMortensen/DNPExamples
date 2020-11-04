using System;
using System.Collections.Generic;
using System.Linq;
using FamilyLINQTraining.DataAccess;
using Microsoft.EntityFrameworkCore;
using Models;

namespace FamilyLINQTraining.SolutionExample
{
    public class Solutions
    {
        public void RunSolutions()
        {
            using (FamilyContext ctx = new FamilyContext())
            {
                // HowManyFamiliesLivesAt("Abby Park Street", ctx);
                // HowManyFamiliesLivesInNumber(5, ctx);
                // HowManyFamiliesHaveOneParent(ctx);
                // HowManyFamiliesLiveInNumberThreeOrFive(ctx);
                // HowManyFamiliesHaveADog(ctx);
                // HowManyFamiliesHaveCatAndDog(ctx);
                // HowManyFamiliesHaveXChildren(3, ctx);
                // HowManyFamiliesHaveGayParents(ctx);
                // HowManyFamiliesHaveAnAdultWithRedHair(ctx);
                // HowManyFamiliesHaveXPets(2, ctx);
                // HowManyFamiliesHaveAChildPlaying("Soccer", ctx);
                // HowManyFamiliesHaveAdultAndChildWithBlackHair(ctx);
                // HowManyFamiliesHaveAChildWithBlackHairPlayingUltimate(ctx);
                // todo HowManyFamiliesHaveTwoAdultsWithSameHairColor(ctx);
                // TODO HowManyUniqueHairColorsDoesAFamilyHave(ctx);
                // TODO HowManyChildrenAreOfHeightBetween(95, 112, ctx);
                // TODO HowManyChildrenAreObese(ctx); // BMI above 30 
            }
        }

        /**
         * 14
         */
        private void HowManyFamiliesHaveCatAndDog(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => 
                    family.Pets.Any(pet => pet.Species.Equals("Dog") &&
                    family.Pets.Any(pet => pet.Species.Equals("Cat"))                                           
                    )).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 53
         */
        private void HowManyFamiliesHaveAChildWithBlackHairPlayingUltimate(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => 
                    family.Children.Any(
                    child => child.HairColor.Equals("Black")
                             && 
                             child.ChildInterests.Any(
                                 childInterest => childInterest.InterestId.Equals("Ultimate")))).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 99 dogs
         */
        private void HowManyFamiliesHaveADog(FamilyContext ctx)
        {
            var result = ctx.Families.Where(family => family.Pets.Any(pet => pet.Species.Equals("Dog"))).ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 94
         */
        private void HowManyFamiliesLiveInNumberThreeOrFive(FamilyContext ctx)
        {
            var result = ctx.Families.Where(family => family.HouseNumber == 3 || family.HouseNumber == 5).ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 134
         */
        private void HowManyFamiliesHaveOneParent(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => family.Adults.Count == 1).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 387
         */
        private void HowManyFamiliesHaveAdultAndChildWithBlackHair(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => 
                    family.Adults.Any(adult => 
                        adult.HairColor.Equals("Black")) && 
                    family.Children.Any(child => 
                        child.HairColor.Equals("Black"))).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 79
         */
        private void HowManyFamiliesHaveAChildPlaying(string sport, FamilyContext ctx)
        {
            var result = ctx.Families.Where(family => family.Children.Any(
                child => child.ChildInterests.Any(
                    childInterest => childInterest.InterestId.Equals(sport)))).ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 26
         */
        private void HowManyFamiliesHaveXPets(int i, FamilyContext ctx)
        {
            var result = ctx.Families.Where(family => family.Pets.Count == 2).ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * 19
         * Hint: Use .Any -> it will return if any entity in a collection fulfills a condition.
         * I include Adults, so they are loaded into the end result. This is not actually necessary.
         * It was mainly for verifycation afterwards.
         *
         * I filter based on Where a Family's collection of Adults contains _Any_ Adult which satisfies the condition
         */
        private void HowManyFamiliesHaveAnAdultWithRedHair(FamilyContext ctx)
        {
            var result = ctx.Families.
                Include(family => family.Adults).
                Where(family => family.Adults.Any(adult => adult.HairColor.Equals("Red"))).
                ToList();

            Console.WriteLine(result.Count);
        }

        /**
         * 191
         * Cannot create an appropriate Where statement. Must convert to List, and filter this one.
         * I could move the where predicate to the FindAll. But optimize but first shrinking the list with Where.
         */
        private void HowManyFamiliesHaveGayParents(FamilyContext ctx)
        {
            List<Family> families = ctx.Families.Include(f => f.Adults).Where(family => family.Adults.Count == 2)
                .ToList().FindAll(family => family.Adults[0].Sex.Equals(family.Adults[1].Sex));
            Console.WriteLine(families.Count); // 191
        }

        private void HowManyFamiliesHaveXChildren(int i, FamilyContext ctx)
        {
            List<Family> families = ctx.Families.Where(family => family.Children.Count == i).ToList();
            Console.WriteLine(families.Count); //122
        }

        private void HowManyFamiliesLivesInNumber(int i, FamilyContext ctx)
        {
            List<Family> families = ctx.Families.Where(f => f.HouseNumber == i).ToList();
            Console.WriteLine(families.Count); // 47
        }

        private void HowManyFamiliesLivesAt(string street, FamilyContext ctx)
        {
            List<Family> families = ctx.Families.Where(f => f.StreetName.Equals(street)).ToList();
            Console.WriteLine(families.Count); // 15
        }
    }
}