using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Windows.Documents;
using System;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InteractiveCharacterSheet
{
    /// <summary>
    /// Interaction logic for CharacterSheet.xaml
    /// </summary>
    public partial class CharacterSheet : MetroWindow
    {
        public CharacterSheet()
        {
            InitializeComponent();

            NewCharacterSheet(null, null);
        }

        internal CharacterSheetPresenter CharSheetPresenter;
        internal bool _isloading = true;

        #region "Menu Options"

        private void NewCharacterSheet(object sender, RoutedEventArgs e)
        {
            CharSheetPresenter = new CharacterSheetPresenter();
            CharSheetPresenter.LoadEmptyCharacterSheet();
            InitializeControls();
        }

        private void SaveCharacterSheet(object sender, RoutedEventArgs e)
        {
            string inputUrl = null;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog1.Title = "Save a Character Sheet";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                inputUrl = saveFileDialog1.FileName;
                SaveBiography();
                CharSheetPresenter.SaveCharacterSheet(inputUrl);
                if (CharSheetPresenter.CharSheetData.Error != null)
                {
                    if (CharSheetPresenter.CharSheetData.Error.ErrorMessage != string.Empty)
                    {
                        MessageBox.Show(CharSheetPresenter.CharSheetData.Error.ErrorMessage, "Error", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void OpenCharacterSheet(object sender, RoutedEventArgs e)
        {
            string inputUrl = null;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog1.Title = "Open a Character Sheet";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == true)
            {
                inputUrl = openFileDialog1.FileName;
            }
            if (inputUrl != null)
            {
                CharSheetPresenter.LoadCharacterSheet(inputUrl);
                if (CharSheetPresenter.CharSheetData.Error != null && CharSheetPresenter.CharSheetData.Error.ErrorMessage != string.Empty)
                {
                    MessageBox.Show(CharSheetPresenter.CharSheetData.Error.ErrorMessage, "Error", MessageBoxButton.OK);
                }
                else
                {
                    InitializeControls();
                    LoadCharacterControls();
                    _isloading = false;
                }
            }
            else
            {
                MessageBox.Show("File not found", "Error", MessageBoxButton.OK);
            }
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SaveBiography()
        {
            CharSheetPresenter.CharSheetData.Biography.Clear();

            foreach (Paragraph p in BiographyText.Document.Blocks)
            {
                CharSheetPresenter.CharSheetData.Biography.Add(p);
            }
        }

        #endregion

        #region "Initialization of Controls"

        private void InitializeControls()
        {

            InitializeCharacterControls();
            LoadSkillsDataGrid();
        }

        private void InitializeCharacterControls()
        {
            AlignmentComboBox.ItemsSource = CharSheetPresenter.DCache.Alignments;
            GenderComboBox.ItemsSource = CharSheetPresenter.DCache.Genders;
            SizeComboBox.ItemsSource = CharSheetPresenter.DCache.Sizes;
            RaceDescriptionText.IsReadOnly = true;
            LoadCharacterControls();
        }

        #endregion

        #region "Character Tab Interface Functions"

        private void LoadCharacterControls()
        {
            CharLevelTextBox.Text = CharSheetPresenter.CharSheetData.Level.ToString();
            CharacterNameTextBox.Text = CharSheetPresenter.CharSheetData.CharacterName;
            PlayerNameTextBox.Text = CharSheetPresenter.CharSheetData.PlayerName;
            RaceTextBox.Text = CharSheetPresenter.CharSheetData.Race;
            SizeComboBox.SelectedItem = SetSelectedSize();
            GenderComboBox.SelectedItem = CharSheetPresenter.CharSheetData.Gender;
            HeightTextBox.Text = CharSheetPresenter.CharSheetData.Height.ToString();
            WeightTextBox.Text = CharSheetPresenter.CharSheetData.Weight.ToString();
            AgeTextBox.Text = CharSheetPresenter.CharSheetData.Age.ToString();
            AlignmentComboBox.SelectedItem = SetSelectedAlignment();
            DeityTextBox.Text = CharSheetPresenter.CharSheetData.Deity;
            OccupationTextBox.Text = CharSheetPresenter.CharSheetData.Occupation;
            LanguagesTextBox.Text = CharSheetPresenter.CharSheetData.Languages;
            BiographyText.Document.Blocks.Clear();
            BiographyText.Document.Blocks.AddRange(CharSheetPresenter.CharSheetData.Biography);
        }

        private void LoadCombatControls()
        {
            StrengthBaseTextBox.Text = CharSheetPresenter.CharSheetData.Strength.BaseAbilityScoreValue.ToString();
            DexterityBaseTextBox.Text = CharSheetPresenter.CharSheetData.Dexterity.BaseAbilityScoreValue.ToString();
            ConstitutionBaseTextBox.Text = CharSheetPresenter.CharSheetData.Constitution.BaseAbilityScoreValue.ToString();
            IntelligenceBaseTextBox.Text = CharSheetPresenter.CharSheetData.Intelligence.BaseAbilityScoreValue.ToString();
            WisdomBaseTextBox.Text = CharSheetPresenter.CharSheetData.Wisdom.BaseAbilityScoreValue.ToString();
            CharismaBaseTextBox.Text = CharSheetPresenter.CharSheetData.Charisma.BaseAbilityScoreValue.ToString();
        }

        private void LoadSkillsDataGrid()
        {
            SkillsDataGrid.ItemsSource = CharSheetPresenter.CharSheetData.Skills;
        }

        private void LoadTraitsDataGrid()
        {
            TraitsDataGrid.ItemsSource = CharSheetPresenter.CharSheetData.Traits;
        }

        private void LoadFeatsDataGrid()
        {
            FeatsDataGrid.ItemsSource = CharSheetPresenter.CharSheetData.Feats;
        }

        private void CharacterNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.CharacterName = CharacterNameTextBox.Text;
            }
        }

        private void PlayerNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.PlayerName = PlayerNameTextBox.Text;
            }
        }

        private void CharLevelTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Level = int.Parse(CharLevelTextBox.Text);
            }
        }

        private void RaceTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Race = RaceTextBox.Text;
            }
        }

        private void GenderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Gender = (string)GenderComboBox.SelectedItem;
            }
        }

        private void SizeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Size = (string)SizeComboBox.SelectedItem;
            }
        }

        private void HeightTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Height = int.Parse(HeightTextBox.Text);
            }
        }

        private void WeightTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Weight = int.Parse(WeightTextBox.Text);
            }
        }

        private void AgeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Age = int.Parse(AgeTextBox.Text);
            }
        }

        private void AlignmentComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Alignment = (string)(AlignmentComboBox.SelectedItem);
            }
        }

        private void DeityTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Deity = DeityTextBox.Text;
            }
        }

        private void OccupationTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Occupation = OccupationTextBox.Text;
            }
        }

        private void LanguagesTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Languages = LanguagesTextBox.Text;
            }
        }
        
        private void SkillsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CharacterSkill cs = (CharacterSkill)SkillsDataGrid.SelectedItem;
            DescriptionTextBox.Document.Blocks.Clear();
            DescriptionTextBox.Document.Blocks.AddRange(cs.Description);
        }

        private void TraitsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RacialTrait rt = (RacialTrait)TraitsDataGrid.SelectedItem;
            DescriptionTextBox.Document.Blocks.Clear();
            DescriptionTextBox.Document.Blocks.AddRange(rt.Description);
        }

        private void FeatsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CharacterFeat cf = (CharacterFeat)FeatsDataGrid.SelectedItem;
            DescriptionTextBox.Document.Blocks.Clear();
            DescriptionTextBox.Document.Blocks.AddRange(cf.Description);
        }

        private void InventoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InventoryRow ir = (InventoryRow)InventoryDataGrid.SelectedItem;
            DescriptionTextBox.Document.Blocks.Clear();
            DescriptionTextBox.Document.Blocks.AddRange(ir.RowItem.Description);
        }

        private CharacterSize SetSelectedSize()
        {
            return CharSheetPresenter.DCache.Sizes.Find(x => x.Name == CharSheetPresenter.CharSheetData.Size);
        }

        private Alignment SetSelectedAlignment()
        {
            return CharSheetPresenter.DCache.Alignments.Find(x => x.Name == CharSheetPresenter.CharSheetData.Alignment);
        }

        #endregion
    }
}