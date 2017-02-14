using System;
using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using PropertyChanged;
using WpfApplication1.Engine;
using WpfApplication1.Plumbing;

namespace WpfApplication1.ViewModels
{
    [ImplementPropertyChanged]
    class SpeechViewModel
    {
        public SpeechViewModel()
        {
            RecordCommand = new RelayCommand(p=>true, Record);
            EnrollCommand = new RelayCommand(p=>true, Enroll);
            VerifyCommand = new RelayCommand(p=>true, Verify);
            CreateProfileCommand = new RelayCommand(p=>true, CreateProfile);
        }

        private void Record(object obj)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() ?? false)
            {
                AnalysisData = File.ReadAllBytes(ofd.FileName);
            }
            AnalysisResult = "Loaded";
        }



        private async void CreateProfile(object obj)
        {
            AnalysisResult = "Creating profile...";
            var apiResult = await Api.CreateSpeakerProfile();
            AnalysisResult = JsonHelper.FormatJson(await apiResult.Content.ReadAsStringAsync());
        }

        private async void Verify(object obj)
        {
            AnalysisResult = "Verifivation...";
            var apiResult = await Api.RequestVoiceVerification(AnalysisData, SelectedSpeaker);
            AnalysisResult = JsonHelper.FormatJson(await apiResult.Content.ReadAsStringAsync());
        }

        private async void Enroll(object obj)
        {
            AnalysisResult = "Enrollment...";
            var apiResult = await Api.RequestVoiceEnrollment(AnalysisData, SelectedSpeaker);
            AnalysisResult = JsonHelper.FormatJson(await apiResult.Content.ReadAsStringAsync());
        }
        
        public Guid SelectedSpeaker { get; set; }

        public byte[] AnalysisData { get; set; }

        public ICommand RecordCommand { get; set; }
        public ICommand CreateProfileCommand { get; set; }
        public ICommand EnrollCommand { get; set; }
        public ICommand VerifyCommand { get; set; }

        public string AnalysisResult { get; set; }
    }
}
