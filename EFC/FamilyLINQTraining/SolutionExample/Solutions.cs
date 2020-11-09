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
                // HowManyFamiliesHaveTwoAdultsWithSameHairColor(ctx);
                // HowManyFamiliesHaveAChildWithAHamster(ctx);
                // HowManyChildrenAreInterestedInBothSoccerAndBarbies(ctx);
                
                // TODO HowManyUniqueHairColorsDoesAFamilyHave(ctx);
                // TODO HowManyChildrenAreOfHeightBetween(95, 112, ctx);
                // TODO HowManyChildrenAreObese(ctx); // BMI above 30
                // TODO ListFamilyMemberByHeight(ctx);
            }
        }

        /**
         * P
         * 5
         */
        private void HowManyChildrenAreInterestedInBothSoccerAndBarbies(FamilyContext ctx)
        {
            var result = ctx.Families.
                SelectMany(family => family.Children).
                Where(child => 
                    child.ChildInterests.Any(interest => interest.InterestId.Equals("Soccer")) && 
                    child.ChildInterests.Any(interest => interest.InterestId.Equals("Barbie"))).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * O
         * 67
         */
        private void HowManyFamiliesHaveAChildWithAHamster(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family =>
                family.Children.Any(child => 
                    child.Pets.Any(pet => pet.Species.Equals("Hamster")))).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * N
         * 192
         * It looks a bit iffy, to keep from calling ToList() before the end.
         * Hint: Count() counts how many elements matches a criteria
         */
        private void HowManyFamiliesHaveTwoAdultsWithSameHairColor(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => family.Adults.Count == 2 &&
                                family.Adults.Count(adult => 
                                    adult.HairColor.Equals(family.Adults.First().HairColor)) == 2).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * F
         * 10
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
         * M
         * 54
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
         * E
         * 83 dogs
         */
        private void HowManyFamiliesHaveADog(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => family.Pets.Any(pet => pet.Species.Equals("Dog"))).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * D
         * 92
         */
        private void HowManyFamiliesLiveInNumberThreeOrFive(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => family.HouseNumber == 3 || family.HouseNumber == 5).ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * C
         * 154
         */
        private void HowManyFamiliesHaveOneParent(FamilyContext ctx)
        {
            var result = ctx.Families.
                Where(family => family.Adults.Count == 1).
                ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * L
         * 350
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
         * K
         * 67
         */
        private void HowManyFamiliesHaveAChildPlaying(string sport, FamilyContext ctx)
        {
            var result = ctx.Families.Where(family => family.Children.Any(
                child => child.ChildInterests.Any(
                    childInterest => childInterest.InterestId.Equals(sport)))).ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * J
         * 20
         */
        private void HowManyFamiliesHaveXPets(int i, FamilyContext ctx)
        {
            var result = ctx.Families.Where(family => family.Pets.Count == i).ToList();
            Console.WriteLine(result.Count);
        }

        /**
         * I
         * 12
         * Hint: Use .Any -> it will return if any entity in a collection fulfills a condition.
         * I include Adults, so they are loaded into the end result. This is not actually necessary.
         * It was mainly for verification afterwards.
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
         * H
         * 187
         * Cannot create an appropriate Where statement. Must convert to List, and filter this one.
         * I could move the where predicate to the FindAll. But optimize but first shrinking the list with Where.
         */
        private void HowManyFamiliesHaveGayParents(FamilyContext ctx)
        {
            /* Less efficient alternative. I call ToList() in the middle */
             List<Family> families1 = ctx.Families.Include(f => f.Adults).Where(family => family.Adults.Count == 2)
                .ToList().FindAll(family => family.Adults[0].Sex.Equals(family.Adults[1].Sex));
                

            var families = ctx.Families.
                Where(family => family.Adults.Count == 2).
                Where(family => 
                    family.Adults.Count(adult => 
                        adult.Sex.Equals(family.Adults.First().Sex)) == 2).
                ToList();
            Console.WriteLine(families.Count); 
        }

        /**
         * G
         * 112 (3 children families)
         */
        private void HowManyFamiliesHaveXChildren(int i, FamilyContext ctx)
        {
            List<Family> families = ctx.Families.Where(family => family.Children.Count == i).ToList();
            Console.WriteLine(families.Count); 
        }

        /**
         * B
         * 46
         */
        private void HowManyFamiliesLivesInNumber(int i, FamilyContext ctx)
        {
            List<Family> families = ctx.Families.Where(f => f.HouseNumber == i).ToList();
            Console.WriteLine(families.Count); 
        }

        /**
         * A
         * 5
         */
        private void HowManyFamiliesLivesAt(string street, FamilyContext ctx)
        {
            List<Family> families = ctx.Families.Where(f => f.StreetName.Equals(street)).ToList();
            Console.WriteLine(families.Count); 
        }

        private void HowManyFamiliesLiveInNumberOne(FamilyContext ctx)
        {
            var result = ctx.Families.Where(family => family.HouseNumber == 1).ToList();
            Console.WriteLine(result.Count);
        }
    }
}