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
        public List<string> Alignments;
        public List<string> Genders;
        public ObservableCollection<CharacterSkill> Skills;

        public DataCache()
        {
            this.XManager = new XMLManager();
            LoadAlignments();
            LoadGenders();
            LoadSkills();
        }

        private void LoadAlignments()
        {
            List<string> alignments = new List<string>();
            alignments.Add("Lawful Good");
            alignments.Add("Lawful Neutral");
            alignments.Add("Lawful Evil");
            alignments.Add("Neutral Good");
            alignments.Add("True Neutral");
            alignments.Add("Neutral Evil");
            alignments.Add("Chaotic Good");
            alignments.Add("Chaotic Neutral");
            alignments.Add("Chaotic Evil");
            this.Alignments = alignments;
        }

        private void LoadGenders()
        {
            List<string> genders = new List<string>();
            genders.Add("Female");
            genders.Add("Male");
            genders.Add("Other");
            this.Genders = genders;
        }

        private void LoadSkills()
        {
            this.Skills = XManager.LoadSkills();
        }
    }
}
