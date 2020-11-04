using System;
using System.Collections.Generic;
using System.Linq;
using FamilyLINQTraining.DataGeneration.StaticData;
using Models;

namespace FamilyLINQTraining.DataGeneration
{
    public class FamilyGenerator
    {
        private Random rand = new Random();
        private Dictionary<string, int> streetNumbersTaken;
        private int ChildId = 0;

        public FamilyGenerator()
        {
            streetNumbersTaken = new Dictionary<string, int>();
        }

        public IList<Family> GenerateFamilies(int numToGenerate)
        {
            IList<Family> families = new List<Family>();
            for (int i = 0; i < numToGenerate; i++)
            {
                Family fam = GenerateOneFamily();

                families.Add(fam);
            }

            return families;
        }

        public Child GenerateChild(Family family, int[] ageRange, string lastName)
        {
            Child c = new Child();
            c.Id = (++ChildId);

            // c.Id = ChildId++;
            c.Age = rand.Next(ageRange[1] - ageRange[0]) + ageRange[0];
            c.Sex = rand.Next(2) == 0 ? "F" : "M";

            // setting height
            if (c.Age == 0)
            {
                c.Height = 50 + rand.Next(11) - 5;
            }
            else if (c.Age == 1)
            {
                c.Height = 70 + rand.Next(15) - 7;
            }
            else if (c.Age < 17)
            {
                int tmp = c.Age * 6 + 77;
                c.Height = tmp + rand.Next((int) (tmp * 0.4)) - (int) (tmp * 0.2);
                if (c.Sex.Equals("F"))
                {
                    c.Height -= rand.Next(5);
                }
            }
            else if (c.Age >= 17)
            {
                string s = c.Sex;

                int result = GenerateAdultHeight(s);

                c.Height = result;
            }

            //float cWeight = c.Weight;
            int cHeight = c.Height;
            decimal cWeight = GenerateWeight(cHeight);

            if (c.Sex.Equals("F"))
            {
                cWeight -= cWeight * 0.15M;
            }

            c.Weight = cWeight;
            c.EyeColor = GenerateEyeColor(family.Adults).ToString();
            c.HairColor = GenerateHairColor().ToString();
            if (c.Sex.Equals("F"))
            {
                c.FirstName = FemaleName.list[rand.Next(FemaleName.list.Length)];
            }
            else
            {
                c.FirstName = MaleName.list[rand.Next(MaleName.list.Length)];
            }

            c.LastName = !"".Equals(lastName) && lastName != null
                ? lastName
                : LastName.list[rand.Next(LastName.list.Length)];

            foreach (Adult parent in family.Adults)
            {
                if (c.LastName.Equals(parent.LastName))
                {
                    c.LastName += " Jr.";
                }
            }

            c.Pets = GenerateChildPets();

            List<ChildInterest> childInterests = new List<ChildInterest>();
            double chance = 1.0;

            while (chance >= rand.NextDouble())
            {
                int index = rand.Next(InterestTypes.dic.Count);
                string interestType = InterestTypes.dic.Keys.ToArray()[index];
                Interest interest = new Interest
                {
                    Type = interestType,
                    Description = InterestTypes.dic[interestType]
                };
                ChildInterest ci = new ChildInterest
                {
                    Child = c,
                    Interest = interest,
                    ChildId = c.Id,
                    InterestId = interest.Type
                };
                if (!childInterests.Contains(ci))
                {
                    childInterests.Add(ci);
                }

                chance *= 0.8;
            }

            c.ChildInterests = childInterests;
            return c;
        }

        private decimal GenerateWeight(int height)
        {
            // TODO noget går galt, den kan blive negativ

            int weightDist = rand.Next(100) + 1;
            if (height < 100) weightDist -= 50;
            else if (height < 120) weightDist -= 35;
            else if (height < 150) weightDist -= 20;

            decimal cWeight = 0M;
            if (weightDist < 15)
            {
                decimal scale = 0.2M * height;
                cWeight = scale + rand.Next((int) (scale * 0.4M)) - (int) (scale * 0.1M);
            }
            else if (weightDist < 65)
            {
                decimal scale = 0.40M * height;
                cWeight = scale + rand.Next((int) (scale * 0.4M)) - (int) (scale * 0.2M);
            }
            else if (weightDist < 90)
            {
                decimal scale = 0.57M * height;
                cWeight = scale + rand.Next((int) (scale * 0.4M)) - (int) (scale * 0.2M);
            }
            else if (weightDist <= 100)
            {
                decimal scale = 0.8M * height;
                cWeight = scale + rand.Next((int) (scale * 0.4M)) - (int) (scale * 0.2M);
            }

            cWeight = Math.Round(cWeight, 1);
            return cWeight;
        }

        private Family GenerateOneFamily()
        {
            Family fam = new Family();
            fam.Adults = GenerateParents();
            fam.Children = GenerateChildren(fam);

            fam.Pets = GenerateFamilyPets();

            fam.StreetName = Street.list[rand.Next(Street.list.Length)];
            if (streetNumbersTaken.ContainsKey(fam.StreetName))
            {
                int famHouseNumber = streetNumbersTaken[fam.StreetName] + 1;
                fam.HouseNumber = famHouseNumber;
                streetNumbersTaken[fam.StreetName] = famHouseNumber;
            }
            else
            {
                fam.HouseNumber = 1;
                streetNumbersTaken.Add(fam.StreetName, 1);
            }

            return fam;
        }

        private List<Pet> GenerateFamilyPets()
        {
            List<Pet> pets = new List<Pet>();

            double chance = 0.3;
            while (chance > rand.NextDouble())
            {
                Pet p = new Pet();
                p.Species = rand.Next(2) == 0 ? "Cat" : "Dog";
                p.Name = PetNames.list[rand.Next(PetNames.list.Length)];
                p.Age = rand.Next(15);
                // p.Id = PetId++;
                pets.Add(p);
                chance *= 0.5;
            }

            return pets;
        }

        private List<Child> GenerateChildren(Family family)
        {
            string lastName;
            if (family.Adults.Count > 0)
            {
                lastName = family.Adults[rand.Next(family.Adults.Count)].LastName;
            }
            else
            {
                lastName = LastName.list[rand.Next(LastName.list.Length)];
            }

            int youngestParentAge = 100;
            foreach (Adult parent in family.Adults)
            {
                youngestParentAge = Math.Min(youngestParentAge, parent.Age);
            }

            int[] ageRange = {0, youngestParentAge - 17};

            List<Child> children = new List<Child>();

            int childRange = rand.Next(100) + 1;
            int[] spread = {5, 19, 38, 23, 9, 3, 2, 1}; // distribution of how many children a family has
            int idx = 0;
            int counter = spread[idx];

            while (counter < childRange)
            {
                Child c = GenerateChild(family, ageRange, lastName);
                c.LastName = lastName ?? c.LastName;
                children.Add(c);
                idx++;
                counter += spread[idx];
            }

            return children;
        }

        private List<Adult> GenerateParents()
        {
            List<Adult> parents = new List<Adult>();
            int chance = rand.Next(100) + 1;
            int numOfParents = 2;

            if (chance < 10)
            {
                numOfParents = 0;
            }

            if (chance < 30)
            {
                numOfParents = 1;
            }


            int[] ageRange = new int[2];
            ageRange[0] = 30;
            ageRange[1] = 60;

            for (int i = 0; i < numOfParents; i++)
            {
                Adult adult = GenerateAdult(ageRange);
                if (parents.Count > 0)
                {
                    if (rand.Next(2) == 0)
                    {
                        // couple is married, same last name
                        adult.LastName = parents[0].LastName;
                    }
                }

                parents.Add(adult);
            }

            return parents;
        }

        private List<Pet> GenerateChildPets()
        {
            List<Pet> pets = new List<Pet>();
            double chance = 0.20;
            while (chance > rand.NextDouble())
            {
                Pet p = new Pet();
                int petType = rand.Next(100) + 1;
                if (petType < 30)
                {
                    p.Species = PetSpecies.list[0];
                }
                else if (petType < 60)
                {
                    p.Species = PetSpecies.list[1];
                }
                else if (petType < 70)
                {
                    p.Species = PetSpecies.list[2];
                }
                else if (petType < 80)
                {
                    p.Species = PetSpecies.list[3];
                }
                else if (petType < 85)
                {
                    p.Species = PetSpecies.list[4];
                }
                else if (petType < 90)
                {
                    p.Species = PetSpecies.list[5];
                }
                else if (petType < 105)
                {
                    p.Species = PetSpecies.list[6];
                }

                // p.Id = PetId++;
                p.Age = rand.Next(5);
                p.Name = PetNames.list[rand.Next(PetNames.list.Length)];
                pets.Add(p);
                chance *= 0.4;
            }

            return pets;
        }

        private HairColor GenerateHairColor()
        {
            // Ignore parents hair color because you can dye it
            int r = rand.Next(100) + 1;
            if (r < 69)
            {
                // black
                return HairColor.Black;
            }

            if (r < 85)
            {
                return HairColor.Brown;
            }

            if (r < 87)
            {
                return HairColor.Red;
            }

            if (r < 89)
            {
                return HairColor.Leverpostej;
            }

            if (r < 93)
            {
                return HairColor.Blond;
            }

            if (r < 95)
            {
                return HairColor.White;
            }

            if (r < 98)
            {
                return HairColor.Grey;
            }

            if (r < 99)
            {
                return HairColor.Blue;
            }

            if (r < 100)
            {
                return HairColor.Green;
            }

            return HairColor.Black;
        }

        private EyeColor GenerateEyeColor(List<Adult> familyParents)
        {
            if (familyParents.Count == 2)
            {
                // determine eye color based on parents
                EyeColor p1 = Enum.Parse<EyeColor>(familyParents[0].EyeColor);
                EyeColor p2 = Enum.Parse<EyeColor>(familyParents[1].EyeColor);

                if (IsBrownEye(p1) && IsBrownEye(p2))
                {
                    int r = rand.Next(100) + 1;
                    if (r < 75)
                    {
                        // brown/amber/hazel
                        r = rand.Next(100) + 1;
                        if (r < 88) return EyeColor.Brown;
                        if (r < 94) return EyeColor.Hazel;
                        return EyeColor.Amber;
                    }

                    if (r < 94)
                    {
                        // blue/grey
                        return rand.Next(3) == 0 ? EyeColor.Grey : EyeColor.Blue;
                    }

                    // green
                    return EyeColor.Green;
                }

                if (IsBrownEye(p1) && IsBlueEye(p2) ||
                    IsBrownEye(p2) && IsBlueEye(p1))
                {
                    int r = rand.Next(100) + 1;
                    if (r <= 50)
                    {
                        // brown/amber/hazel
                        r = rand.Next(100) + 1;
                        if (r < 88) return EyeColor.Brown;
                        if (r < 94) return EyeColor.Hazel;
                        return EyeColor.Amber;
                    }

                    return rand.Next(3) == 0 ? EyeColor.Grey : EyeColor.Blue;
                }

                if ((IsBrownEye(p1) && p2 == EyeColor.Green) ||
                    (IsBrownEye(p2) && p1 == EyeColor.Green))
                {
                    int r = rand.Next(100) + 1;
                    if (r < 50)
                    {
                        // brown/amber/hazel
                        r = rand.Next(100) + 1;
                        if (r < 88) return EyeColor.Brown;
                        if (r < 94) return EyeColor.Hazel;
                        return EyeColor.Amber;
                    }

                    if (r < 12)
                    {
                        // blue/grey
                        return rand.Next(3) == 0 ? EyeColor.Grey : EyeColor.Blue;
                    }

                    // green
                    return EyeColor.Green;
                }

                if (IsBlueEye(p1) && IsBlueEye(p2))
                {
                    return rand.Next(100) == 0 ? EyeColor.Green : EyeColor.Blue;
                }

                if ((IsBlueEye(p1) && p2 == EyeColor.Green) ||
                    (IsBlueEye(p2) && p1 == EyeColor.Green))
                {
                    return rand.Next(100) < 50 ? EyeColor.Green : EyeColor.Blue;
                }

                if (p1 == EyeColor.Green && p2 == EyeColor.Green)
                {
                    return rand.Next(100) < 75 ? EyeColor.Green : EyeColor.Blue;
                }
            }
            else
            {
                // randomly determine eye color based on world distribution
                int r = rand.Next(100) + 1;
                switch (r)
                {
                    case int n when n < 75: return EyeColor.Brown;
                    case int n when n >= 75 && n < 84: return EyeColor.Blue;
                    case int n when n >= 84 && n < 86: return EyeColor.Green;
                    case int n when n >= 86 && n < 91: return EyeColor.Hazel;
                    case int n when n >= 91 && n < 96: return EyeColor.Amber;
                    case int n when n >= 96: return EyeColor.Grey;
                }
            }

            // default to brown
            return EyeColor.Brown;
        }

        private static bool IsBlueEye(EyeColor p2)
        {
            return (p2 == EyeColor.Blue || p2 == EyeColor.Grey);
        }

        private static bool IsBrownEye(EyeColor p1)
        {
            return (p1 == EyeColor.Brown || p1 == EyeColor.Amber || p1 == EyeColor.Hazel);
        }

        private int GenerateAdultHeight(string s)
        {
            int result = 0;
            double heightDist = rand.NextDouble() * 100f;
            if (heightDist < 2.35)
            {
                // 150 - 165
                result = rand.Next(15) + 150;
            }
            else if (heightDist < 15.85)
            {
                // 165 - 169
                result = rand.Next(5) + 165;
            }
            else if (heightDist < 45.85)
            {
                // 169 - 173
                result = rand.Next(5) + 169;
            }
            else if (heightDist < 79.85)
            {
                // 172 - 177
                result = rand.Next(6) + 172;
            }
            else if (heightDist < 97.35)
            {
                // 177 - 181
                result = rand.Next(5) + 177;
            }
            else if (heightDist < 99.35)
            {
                // 181 - 196
                result = rand.Next(15) + 181;
            }
            else if (heightDist <= 100)
            {
                // 196
                result = rand.Next(20) + 196;
            }

            if (s.Equals("F"))
            {
                result -= rand.Next((int) (result * 0.2f));
            }

            return result;
        }

        public Adult GenerateAdult(int[] ageRange)
        {
            Adult a = new Adult();
            // a.Id = AdultId;
            a.Age = rand.Next(ageRange[1] - ageRange[0]) + ageRange[0];
            a.Sex = rand.Next(2) == 0 ? "F" : "M";
            a.JobTitle = Job.list[rand.Next(Job.list.Length)];
            a.Height = GenerateAdultHeight(a.Sex);
            a.EyeColor = GenerateEyeColor(new List<Adult>()).ToString();
            a.Weight = GenerateWeight(a.Height);
            a.FirstName = a.Sex.Equals("M")
                ? MaleName.list[rand.Next(MaleName.list.Length)]
                : FemaleName.list[rand.Next(FemaleName.list.Length)];
            a.LastName = LastName.list[rand.Next(LastName.list.Length)];
            a.HairColor = GenerateHairColor().ToString();
            // AdultId++;
            return a;
        }
    }
}