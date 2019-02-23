using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveCharacterSheet
{
    class DataCache
    {
        private XMLManager XManager;
        public List<Alignment> Alignments;
        public List<CharacterSize> Sizes;
        public List<string> Genders;
        public HashSet<CharacterRace> Races;
        public HashSet<CharacterClass> Classes;
        public ObservableCollection<CharacterSkill> Skills;

        public DataCache()
        {
            XManager = new XMLManager();
            LoadAlignments();
            LoadSizes();
            LoadGenders();
            LoadSkills();
            LoadRaces();
            //LoadClasses();
        }

        private void LoadAlignments()
        {
            List<Alignment> alignments = new List<Alignment>();
            alignments.Add(new Alignment("LawfulGood", "Lawful Good"));
            alignments.Add(new Alignment("LawfulNeutral", "Lawful Neutral"));
            alignments.Add(new Alignment("LawfulEvil", "Lawful Evil"));
            alignments.Add(new Alignment("NeutralGood", "Neutral Good"));
            alignments.Add(new Alignment("Neutral", "Neutral"));
            alignments.Add(new Alignment("NeutralEvil", "Neutral Evil"));
            alignments.Add(new Alignment("ChaoticGood", "Chaotic Good"));
            alignments.Add(new Alignment("ChaoticNeutral", "Chaotic Neutral"));
            alignments.Add(new Alignment("ChaoticEvil", "Chaotic Evil"));
            Alignments = alignments;
        }

        private void LoadSizes()
        {
            List<CharacterSize> sizes = new List<CharacterSize>();
            sizes.Add(new CharacterSize("Fine", "Fine", 8, -8, 8, 16, 0.5, 0));
            sizes.Add(new CharacterSize("Diminutive", "Diminutive", 4, -4, 6, 12, 1, 0));
            sizes.Add(new CharacterSize("Tiny", "Tiny", 2, -2, 4, 8, 2.5, 0));
            sizes.Add(new CharacterSize("Small", "Small", 1, -1, 2, 4, 5, 5));
            sizes.Add(new CharacterSize("Medium", "Medium", 0, 0, 0, 0, 5, 5));
            sizes.Add(new CharacterSize("LargeTall", "Large (tall)", -1, 1, -2, -4, 10, 10));
            sizes.Add(new CharacterSize("LargeLong", "Large (long)", -1, 1, -2, -4, 10, 5));
            sizes.Add(new CharacterSize("HugeTall", "Huge (tall)", -2, 2, -4, -8, 15, 15));
            sizes.Add(new CharacterSize("HugeLong", "Huge (long)", -2, 2, -4, -8, 15, 10));
            sizes.Add(new CharacterSize("GargantuanTall", "Gargantuan (tall)", -4, 4, -6, -12, 20, 20));
            sizes.Add(new CharacterSize("GargantuanLong", "Gargantuan (long)", -4, 4, -6, -12, 20, 15));
            sizes.Add(new CharacterSize("ColossalTall", "Colossal (tall)", -8, 8, -8, -16, 30, 30));
            sizes.Add(new CharacterSize("ColossalLong", "Colossal (long)", -8, 8, -8, -16, 30, 20));
            Sizes = sizes;
        }

        private void LoadGenders()
        {
            List<string> genders = new List<string>();
            genders.Add("Female");
            genders.Add("Male");
            genders.Add("Other");
            Genders = genders;
        }

        private void LoadSkills()
        {
            Skills = XManager.LoadSkills();
        }

        private void LoadRaces()
        {
            Races = XManager.LoadRaces();
        }

        private void LoadClasses()
        {
            Classes = XManager.LoadClasses();
        }
    }
}
