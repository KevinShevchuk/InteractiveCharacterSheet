using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;

namespace InteractiveCharacterSheet
{
    class XMLManager
    {
        private string _directoryRoot;

        public XMLManager()
        {
            //Throws two backslashes on the end that we don't want.
            _directoryRoot = AppDomain.CurrentDomain.BaseDirectory;
        }

        public CharacterSheetData SaveCharacterSheet(CharacterSheetData csd, string inputUrl)
        {
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(inputUrl))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Character");
                    xmlWriter.WriteElementString("Level", csd.Level.ToString());
                    xmlWriter.WriteElementString("CharacterName", csd.CharacterName);
                    xmlWriter.WriteElementString("PlayerName", csd.PlayerName);
                    xmlWriter.WriteElementString("Race", csd.Race);
                    xmlWriter.WriteElementString("Size", csd.Size);
                    xmlWriter.WriteElementString("Gender", csd.Gender);
                    xmlWriter.WriteElementString("Age", csd.Age.ToString());
                    xmlWriter.WriteElementString("Height", csd.Height.ToString());
                    xmlWriter.WriteElementString("Weight", csd.Weight.ToString());
                    xmlWriter.WriteElementString("Alignment", csd.Alignment);
                    xmlWriter.WriteElementString("Deity", csd.Height.ToString());
                    xmlWriter.WriteElementString("Occupation", csd.Occupation);
                    xmlWriter.WriteStartElement("Languages");
                    foreach (string lang in csd.LanguagesList)
                    {
                        xmlWriter.WriteElementString("Languages", lang);
                    }
                    xmlWriter.WriteEndElement(); //Languages
                    xmlWriter.WriteStartElement("Biography");
                    foreach (Paragraph paragraph in csd.Biography)
                    {
                        foreach (Run r in paragraph.Inlines)
                        {
                            xmlWriter.WriteElementString("p", r.Text);
                        }
                    }
                    xmlWriter.WriteEndElement(); //Biography
                    xmlWriter.WriteStartElement("BaseAbilityScores");
                    xmlWriter.WriteElementString("Strength", csd.Strength.BaseAbilityScoreValue.ToString());
                    xmlWriter.WriteElementString("Dexterity", csd.Dexterity.BaseAbilityScoreValue.ToString());
                    xmlWriter.WriteElementString("Constitution", csd.Constitution.BaseAbilityScoreValue.ToString());
                    xmlWriter.WriteElementString("Intelligence", csd.Intelligence.BaseAbilityScoreValue.ToString());
                    xmlWriter.WriteElementString("Wisdom", csd.Wisdom.BaseAbilityScoreValue.ToString());
                    xmlWriter.WriteElementString("Charisma", csd.Charisma.BaseAbilityScoreValue.ToString());
                    xmlWriter.WriteEndElement(); //BaseAbilityScores
                    xmlWriter.WriteEndElement(); //Character
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Close();
                }
            }
            catch (Exception ex)
            {
                csd.Error = new Error(ex.Message);
            }

            return csd;
        }

        public CharacterSheetData LoadCharacterSheet(string inputUrl)
        {
            CharacterSheetData csd = new CharacterSheetData();
            try
            {
                using (XmlReader reader = XmlReader.Create(inputUrl))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            XElement xe = XNode.ReadFrom(reader) as XElement;
                            switch (xe.Name.LocalName)
                            {
                                case "Level":
                                    csd.Level = (int)xe;
                                    break;
                                case "CharacterName":
                                    csd.CharacterName = (string)xe;
                                    break;
                                case "PlayerName":
                                    csd.PlayerName = (string)xe;
                                    break;
                                case "Race":
                                    csd.Race = (string)xe;
                                    break;
                                case "Size":
                                    csd.Size = (string)xe;
                                    break;
                                case "Gender":
                                    csd.Gender = (string)xe;
                                    break;
                                case "Age":
                                    csd.Age = (int)xe;
                                    break;
                                case "Height":
                                    csd.Height = (int)xe;
                                    break;
                                case "Weight":
                                    csd.Weight = (int)xe;
                                    break;
                                case "Alignment":
                                    csd.Alignment = (string)xe;
                                    break;
                                case "Deity":
                                    csd.Deity = (string)xe;
                                    break;
                                case "Occupation":
                                    csd.Occupation = (string)xe;
                                    break;
                                case "Languages":
                                    string lang = "";
                                    csd.LanguagesList = LoadLanguages(xe, ref lang);
                                    csd.Languages = lang;
                                    break;
                                case "Biography":
                                    csd.Biography = TextBlocktoParagraphs(xe);
                                    break;
                            }
                        }
                    }
                }
            }

            //        reader.ReadStartElement("CharacterSheet");
            //        //Character
            //        while (reader.Name == "Character")
            //        {
            //            XElement el = (XElement)XNode.ReadFrom(reader);
            //            csd.Level = int.Parse(el.Attribute("level").Value);
            //            csd.CharacterName = el.Attribute("charactername").Value;
            //            csd.PlayerName = el.Attribute("playername").Value;
            //            csd.Race = el.Attribute("race").Value;
            //            csd.Size = el.Attribute("size").Value;
            //            csd.Gender = el.Attribute("gender").Value;
            //            csd.Age = int.Parse(el.Attribute("age").Value);
            //            csd.Height = int.Parse(el.Attribute("height").Value);
            //            csd.Weight = int.Parse(el.Attribute("weight").Value);
            //            csd.Alignment = el.Attribute("alignment").Value;
            //            csd.Deity = el.Attribute("deity").Value;
            //            csd.Occupation = el.Attribute("occupation").Value;
            //            csd.Languages = el.Attribute("languages").Value;
            //            csd.Biography = TextToParagraphs(el.Attribute("biography").Value);

            //            reader.ReadEndElement();
            //        }
            //    }
            //}
            catch (Exception ex)
            {
                csd.Error = new Error(ex.Message);
            }

            return csd;
        }

        #region Skills

        public ObservableCollection<CharacterSkill> LoadSkills()
        {
            ObservableCollection<CharacterSkill> skills = new ObservableCollection<CharacterSkill>();
            try
            {
                using (XmlReader reader = XmlReader.Create(_directoryRoot + "XMLDataSheets\\Skills\\Skills.xml"))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "Skill")
                            {
                                XElement el = XNode.ReadFrom(reader) as XElement;
                                if (el != null)
                                {
                                    skills.Add(LoadSkill(el));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error(ex.Message);
            }

            return skills;
        }

        private CharacterSkill LoadSkill(XElement el)
        {
            CharacterSkill skill = new CharacterSkill();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Name":
                        skill.SkillName = (string)xe;
                        break;
                    case "DisplayName":
                        skill.SkillDisplayName = (string)xe;
                        break;
                    case "GoverningAbilityScore":
                        skill.GoverningAbilityScore = (string)xe;
                        break;
                    case "AppliesArmorCheckPenalty":
                        skill.AppliesArmorCheckPenalty = (bool)xe;
                        break;
                    case "TrainedOnly":
                        skill.TrainedOnly = (bool)xe;
                        break;
                    case "IsCraftSkill":
                        skill.IsCraftSkill = (bool)xe;
                        break;
                    case "IsProfession":
                        skill.IsProfession = (bool)xe;
                        break;
                    case "Description":
                        skill.Description = TextBlocktoParagraphs(xe);
                        break;
                }
            }
            return skill;
        }

        #endregion

        #region Races

        public HashSet<CharacterRace> LoadRaces()
        {
            HashSet<CharacterRace> races = new HashSet<CharacterRace>();
            try
            {
                string[] filenames = Directory.GetFiles(_directoryRoot + "XMLDataSheets\\Races\\");
                for (int i = 0; i < filenames.Length; i++)
                {
                    using (XmlReader reader = XmlReader.Create(filenames[i]))
                    {
                        reader.MoveToContent();
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == "Race")
                                {
                                    XElement el = XNode.ReadFrom(reader) as XElement;
                                    if (el != null)
                                    {
                                        races.Add(LoadRace(el));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error(ex.Message);
            }

            return races;
        }

        private CharacterRace LoadRace(XElement el)
        {
            CharacterRace race = new CharacterRace();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Name":
                        race.RaceName = (string)xe;
                        break;
                    case "DisplayName":
                        race.RaceDisplayName = (string)xe;
                        break;
                    case "Size":
                        race.Size = (string)xe;
                        break;
                    case "BaseSpeed":
                        race.BaseSpeed = (int)xe;
                        break;
                    case "BaseFlySpeed":
                        race.BaseFlySpeed = (int)xe;
                        break;
                    case "BaseSwimSpeed":
                        race.BaseSwimSpeed = (int)xe;
                        break;
                    case "Description":
                        race.Description = TextBlocktoParagraphs(xe);
                        break;
                    case "RacialTraits":
                        race.TraitList = LoadRacialTraits(xe);
                        break;
                }
            }
            return race;
        }

        private List<RacialTrait> LoadRacialTraits(XElement el)
        {
            List<RacialTrait> traits = new List<RacialTrait>();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                if (xe.NodeType == XmlNodeType.Element && xe.Name == "Trait")
                    traits.Add(LoadRacialTrait(xe));
            }
            return traits;
        }

        private RacialTrait LoadRacialTrait(XElement el)
        {
            RacialTrait trait = new RacialTrait();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Name":
                        trait.Name = (string)xe;
                        break;
                    case "DisplayName":
                        trait.DisplayName = (string)xe;
                        break;
                    case "Description":
                        trait.Description = TextBlocktoParagraphs(xe);
                        break;
                    case "Modifications":
                        trait.Modifications = LoadModifications(xe);
                        foreach (CharacterModification mod in trait.Modifications)
                        {
                            mod.ModificationSource = trait.Name;
                        }
                        break;
                }
            }
            return trait;
        }

        #endregion

        #region Classes

        public HashSet<CharacterClass> LoadClasses()
        {
            HashSet<CharacterClass> classes = new HashSet<CharacterClass>();
            try
            {
                string[] filenames = Directory.GetFiles(_directoryRoot + "XMLDataSheets\\Races\\");
                for (int i = 0; i < filenames.Length; i++)
                {
                    using (XmlReader reader = XmlReader.Create(filenames[i]))
                    {
                        reader.MoveToContent();
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == "Race")
                                {
                                    XElement el = XNode.ReadFrom(reader) as XElement;
                                    if (el != null)
                                    {
                                        classes.Add(LoadClass(el));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new Error(ex.Message);
            }

            return classes;
        }

        private CharacterClass LoadClass(XElement el)
        {
            return null; //To be finished.
        }

        #endregion

        #region Feats

        public HashSet<CharacterFeat> LoadFeats()
        {
            return null; //To be finished.
        }

        #endregion

        private List<string> LoadLanguages(XElement el, ref string lang)
        {
            List<string> languages = new List<string>();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Language":
                        languages.Add((string)xe);
                        lang += (string)xe + ", ";
                        break;
                }
            }
            lang.Substring(0, lang.Length - 2);
            return languages;
        }

        #region Support Functions

        private List<CharacterModification> LoadModifications(XElement el)
        {
            List<CharacterModification> mods = new List<CharacterModification>();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                if (xe.NodeType == XmlNodeType.Element && xe.Name == "Modification")
                    mods.Add(LoadModification(xe));
            }
            return mods;
        }

        private CharacterModification LoadModification(XElement el)
        {
            CharacterModification mod = new CharacterModification();

            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                switch (xe.Name.LocalName)
                {
                    case "Type":
                        if (Enum.TryParse((string)xe, out ModType mt))
                            mod.Type = mt;
                        break;
                    case "Mode":
                        if (Enum.TryParse((string)xe, out ModMode mm))
                            mod.Mode = mm;
                        break;
                    case "Property":
                        mod.Property = (string)xe;
                        break;
                    case "Options":
                        mod.OptionsList = new List<string>();
                        IEnumerable<XElement> optionsNodes = xe.Descendants();
                        foreach (XElement xeo in optionsNodes)
                        {
                            mod.OptionsList.Add((string)xeo);
                        }
                        break;
                    case "LevelTable":
                        mod.LevelTable = LoadLevelTable(xe);
                        break;
                    case "Value":
                        mod.Value = (double)xe;
                        break;
                    case "DieSize":
                        if (Enum.TryParse((string)xe, out DieSize ds))
                            mod.DieSize = ds;
                        break;
                }
            }
            return mod;
        }

        private LevelTable LoadLevelTable(XElement el)
        {
            LevelTable lt = new LevelTable();
            IEnumerable<XElement> nodes = el.Descendants();
            foreach (XElement xe in nodes)
            {
                if (xe.Name == "t")
                {
                    LevelTableRow ltr = new LevelTableRow();
                    if (Int32.TryParse(xe.Attribute("Level")?.Value, out int level))
                    {
                        ltr.Property = xe.Attribute("Property")?.Value;
                        if (Enum.TryParse(xe.Attribute("DieSize")?.Value, out DieSize ds))
                            ltr.DieSize = ds;
                        if (Enum.TryParse(xe.Attribute("Action")?.Value, out ModificationAction ma))
                            ltr.ModAction = ma;
                        if (double.TryParse(xe.Attribute("Value")?.Value, out double val))
                            ltr.Value = val;
                    }
                    lt.Table.Add(level, ltr);
                }
            }
            return lt;
        }

        private List<Paragraph> TextBlocktoParagraphs(XmlReader inner)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();

            inner.ReadToFollowing("p");
            while ((inner.NodeType == XmlNodeType.Element) && (inner.Name == "p"))
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run() { Text = inner.ReadElementContentAsString() });
                paragraphs.Add(paragraph);
                inner.Read();
            }
            return paragraphs;
        }

        private List<Paragraph> TextBlocktoParagraphs(XElement textBlock)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();
            IEnumerable<XElement> nodes = textBlock.Descendants();
            foreach (XElement xe in nodes)
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run() { Text = xe.Value });
                paragraphs.Add(paragraph);
            }
            return paragraphs;
        }

        private List<Paragraph> TextToParagraphs(string text)
        {
            List<Paragraph> paragraphs = new List<Paragraph>();
            
            string[] lines = text.Split(new string[] { "\\r", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run() { Text = lines[i] });
                //if (i < lines.Length - 1) paragraph.Inlines.Add(new LineBreak()); //puts extra line in on loads.
                paragraphs.Add(paragraph);
            }

            return paragraphs;
        }

        private string ParagraphsToText(List<Paragraph> paragraphs)
        {
            string text = string.Empty;
            for (int i = 0; i < paragraphs.Count; i++)
            {
                Paragraph p = paragraphs.ElementAt(i);
                text += string.Join(string.Empty, p.Inlines.Select(line => line.ContentStart.GetTextInRun(LogicalDirection.Forward)));
                if (i < paragraphs.Count - 1)
                {
                    text += "\r";
                }
            }

            return text;
        }

        #endregion
    }
}
