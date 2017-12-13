using ElectronicRaffle.Data;
using ElectronicRaffle.Data.Repositories;
using MaterialDesignThemes.Wpf.Transitions;
using Sorschia.Gsm;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ElectronicRaffle.MainApplication.Controls
{
    /// <summary>
    /// Interaction logic for RandomTeacherGeneratorControl.xaml
    /// </summary>
    public partial class RandomTeacherGeneratorControl : UserControl
    {
        public RandomTeacherGeneratorControl()
        {
            InitializeComponent();
            RevealTimer = new Timer(4000);
            RevealTimer.Elapsed += RevealTimer_Elapsed;
            _MediaPlayer = new MediaPlayer();
        }

        private readonly MediaPlayer _MediaPlayer;

        private void Invoke(Action callback)
        {
            Application.Current.Dispatcher.Invoke(callback);
        }

        private void RevealTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Invoke(DrumRollSound.Stop);
            RevealTimer.Stop();
            btnReveal_Click(this, new RoutedEventArgs());
        }

        private Timer RevealTimer;
        private Teacher _PickedTeacher;

        public Teacher PickedTeacher
        {
            get { return _PickedTeacher; }
            set
            {
                if (_PickedTeacher != value)
                {
                    _PickedTeacher = value;
                    ClearTeacher();
                }
            }
        }

        private void ClearTeacher()
        {
            tbxWinner.Text = string.Empty;
            tbxWinner_School.Text = string.Empty;
        }

        private void DisplayTeacher(Teacher teacher)
        {
            tbxWinner.Text = teacher.InformalFullName;
            tbxWinner_School.Text = teacher.School.Name;
        }

        private void GenerateRandomTeacherPick()
        {
            try
            {
                PickedTeacher = TeacherRepository.GenerateRandomPick();
                if (PickedTeacher != null)
                {
                    GsmSms sms = new GsmSms();
                    var coms = sms.GetComs();
                    var com = coms.FirstOrDefault();
                    if (com != null)
                    {
                        sms.Connect(com);

                        //1st Message
                        //var message = $"Happy Teacher's Day, Mr/Ms. {PickedTeacher.InformalFullName}!\r\nGreetings from Mayor John Reynald M. Tiangco.";
                        //sms.Send(PickedTeacher.ContactNumber, message);
                        //sms.Send(PickedTeacher.ContactNumber, message);
                        //Thread.Sleep(10000);
                        //2nd Message
                        //message = "Pls bring your cellphones tomorrow for the Navotas Teachers Summit electronic raffle. Thank you. \r\n#NavotasDBEST";
                        //sms.Send(PickedTeacher.ContactNumber, message);

                        var congratulatoryMessage = $"Congratulations, Mr./Ms. {PickedTeacher.InformalFullName}!\r\n#NavotasDBEST";
                        sms.Send(PickedTeacher.ContactNumber, congratulatoryMessage);
                        sms.Disconnect();

                        //btnReveal_Click(this, new RoutedEventArgs());
                    }
                    DrumRollSound.Play();
                    Transitioner.SelectedItem = SecretSlide;
                    RevealTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured while generating random pick.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            GenerateRandomTeacherPick();
        }

        private void btnReveal_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Transitioner.SelectedItem = RevealSlide;
            });
            //btnGenerate_Click(this, new RoutedEventArgs());
        }

        private async void Transitioner_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var transitionerSlide = (TransitionerSlide)Transitioner.SelectedItem;

            if (transitionerSlide == SecretSlide)
            {
                //btnReveal.Visibility = Visibility.Visible;
                btnGenerate.Visibility = Visibility.Collapsed;

                await Task.Delay(1000);
                if (PickedTeacher != null)
                {
                    DisplayTeacher(PickedTeacher);
                }
                else
                {
                    Transitioner.SelectedItem = NothingSlide;
                }
            }
            else if (transitionerSlide == RevealSlide)
            {
                //btnReveal.Visibility = Visibility.Collapsed;
                btnGenerate.Visibility = Visibility.Visible;
            }
        }

        private void DrumRollSound_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
