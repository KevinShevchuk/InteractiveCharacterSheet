using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using System.IO;
using System.Windows.Documents;
using System;
using System.Timers;
using System.Threading.Tasks;

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

            CharSheetPresenter = new CharacterSheetPresenter();
            InitializeControls();
        }

        internal CharacterSheetPresenter CharSheetPresenter;
        internal bool _isloading = true;

        #region "Menu Options"

        private void SaveCharacterSheetButton_Click(object sender, RoutedEventArgs e)
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
                if (CharSheetPresenter.CharSheetData.ErrorMessage != string.Empty)
                {
                    MessageBox.Show(CharSheetPresenter.CharSheetData.ErrorMessage, "Error", MessageBoxButton.OK);
                }
                else
                {

                }
            }
        }

        private void LoadCharacterSheetButton_Click(object sender, RoutedEventArgs e)
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
                if (CharSheetPresenter.CharSheetData.ErrorMessage != string.Empty)
                {
                    MessageBox.Show(CharSheetPresenter.CharSheetData.ErrorMessage, "Error", MessageBoxButton.OK);
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
        }

        private void InitializeCharacterControls()
        {
            AlignmentComboBox.ItemsSource = CharSheetPresenter.DCache.Alignments;
            GenderComboBox.ItemsSource = CharSheetPresenter.DCache.Genders;
            RaceDescriptionText.IsReadOnly = true;
        }

        #endregion

        #region "Character Tab Interface Functions"

        private void LoadCharacterControls()
        {
            CharLevelTextBox.Text = CharSheetPresenter.CharSheetData.Level.ToString();
            CharacterNameTextBox.Text = CharSheetPresenter.CharSheetData.CharacterName;
            PlayerNameTextBox.Text = CharSheetPresenter.CharSheetData.PlayerName;
            RaceTextBox.Text = CharSheetPresenter.CharSheetData.Race;
            SizeTextBox.Text = CharSheetPresenter.CharSheetData.Size;
            GenderComboBox.SelectedItem = CharSheetPresenter.CharSheetData.Gender;
            HeightTextBox.Text = CharSheetPresenter.CharSheetData.Height.ToString();
            WeightTextBox.Text = CharSheetPresenter.CharSheetData.Weight.ToString();
            AgeTextBox.Text = CharSheetPresenter.CharSheetData.Age.ToString();
            AlignmentComboBox.SelectedItem = CharSheetPresenter.CharSheetData.Alignment;
            DeityTextBox.Text = CharSheetPresenter.CharSheetData.Deity;
            OccupationTextBox.Text = CharSheetPresenter.CharSheetData.Occupation;
            LanguagesTextBox.Text = CharSheetPresenter.CharSheetData.Languages;
            BiographyText.Document.Blocks.Clear();
            BiographyText.Document.Blocks.AddRange(CharSheetPresenter.CharSheetData.Biography);
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
                CharSheetPresenter.CharSheetData.Gender = (string)(GenderComboBox.SelectedItem);
            }
        }

        private void SizeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_isloading == false)
            {
                CharSheetPresenter.CharSheetData.Size = SizeTextBox.Text;
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

        #endregion

    }
}