using ElectronicRaffle.Data;
using ElectronicRaffle.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ElectronicRaffle.CommonUi.Controls
{
    /// <summary>
    /// Interaction logic for RegisterNewTeacherControl.xaml
    /// </summary>
    public partial class RegisterNewTeacherControl : UserControl
    {
        public RegisterNewTeacherControl()
        {
            InitializeComponent();
        }

        private async Task LoadSchoolListAsync()
        {
            await Dispatcher.BeginInvoke(new Action(() => cmbxSchool.Items.Clear()));

            try
            {
                var schoolArray = await SchoolRepository.GetArrayAsync();

                if (schoolArray != null && schoolArray.Any())
                {
                    foreach (School school in schoolArray)
                    {
                        await Dispatcher.BeginInvoke(new Action(() => cmbxSchool.Items.Add(school)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertTeacher()
        {
            var firstName = tbxFirstName.Text?.Trim();
            var middleName = tbxMiddleName.Text?.Trim();
            var lastName = tbxLastName.Text?.Trim();
            var contactNumber = tbxContactNumber.Text?.Trim();
            var school = (School)cmbxSchool.SelectedItem;
            //var address = tbxAddress.Text?.Trim();
            //var gender = cmbxGender.SelectedItem as Gender;
            //var birthDate = dpBirthDate.SelectedDate ?? DateTime.Now;
            //var educationalAttainment = cmbxEducationAttainment.SelectedItem as EducationalAttainment;
            //var member4Ps = tbtnMember4Ps.IsChecked ?? false;
            //var householdNumber = tbxHouseholdNumber.Text?.Trim();

            if (school == null && !string.IsNullOrWhiteSpace(cmbxSchool.Text))
            {
                cmbxSchool.Text = cmbxSchool.Text.ToUpper();
                school = InsertSchool(new School(0)
                {
                    Name = cmbxSchool.Text
                });
            }

            var teacher = new Teacher(0)
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                ContactNumber = contactNumber,
                School = school,
                //Address = address,
                //Gender = gender,
                //BirthDate = birthDate,
                //EducationalAttainment = educationalAttainment,
                //Member4Ps = member4Ps,
                //HouseholdNumber = householdNumber
            };

            if (ValidateTeacher(teacher))
            {
                try
                {
                    teacher = TeacherRepository.Insert(teacher);
                    MessageBox.Show($"Successfully saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetTeacherUiFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured during saving new teacher.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ResetTeacherUiFields()
        {
            tbxFirstName.Text = string.Empty;
            tbxMiddleName.Text = string.Empty;
            tbxLastName.Text = string.Empty;
            tbxContactNumber.Text = string.Empty;
            cmbxSchool.SelectedItem = null;
            //tbxAddress.Text = string.Empty;
            //cmbxGender.SelectedItem = null;
            //dpBirthDate.SelectedDate = null;
            //cmbxEducationAttainment.SelectedItem = null;
            //tbtnMember4Ps.IsChecked = false;
            //tbxHouseholdNumber.Text = string.Empty;
        }

        private School InsertSchool(School school)
        {
            if (!SchoolRepository.Exists(school.Name))
            {
                try
                {
                    school = SchoolRepository.Insert(school);
                    cmbxSchool.Items.Add(school);
                    cmbxSchool.SelectedItem = school;
                }
                catch (Exception)
                {
                    school = null;
                }
            }

            return school;
        }

        private bool ValidateTeacher(Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.FirstName))
            {
                MessageBox.Show("First Name is empty.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(teacher.LastName))
            {
                MessageBox.Show("Last Name is empty.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(teacher.ContactNumber))
            {
                MessageBox.Show("Contact No. is empty.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (teacher.School == null)
            {
                MessageBox.Show("No selected School.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (TeacherRepository.Exists(teacher.FirstName, teacher.MiddleName, teacher.LastName))
            {
                MessageBox.Show("Already exists.", "Validation Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button button)
            {
                if (button.Tag is ICommand command)
                {
                    command.Execute(button.CommandParameter);
                }
            }
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadSchoolListAsync();
            //cmbxGender.Items.Add(Gender.Male);
            //cmbxGender.Items.Add(Gender.Female);
            //cmbxEducationAttainment.Items.Add(EducationalAttainment.Elementary);
            //cmbxEducationAttainment.Items.Add(EducationalAttainment.HighSchool);
            //cmbxEducationAttainment.Items.Add(EducationalAttainment.College);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            InsertTeacher();
        }
    }
}
