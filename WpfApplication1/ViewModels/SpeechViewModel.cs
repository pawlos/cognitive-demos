using System;
using System.Windows.Input;
using NAudio.Wave;
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
            if (_isRecording)
            {
                _waveIn.StopRecording();
                AnalysisResult = "Recording stopped...";
                _isRecording = false;
                return;
            }
            AnalysisResult = "Recording started...";
            _waveIn = new WaveIn {DeviceNumber = 0};
            _bufferedWaveProvider = new BufferedWaveProvider(_waveIn.WaveFormat);
            _waveIn.NumberOfBuffers = 3;
            _waveIn.DataAvailable += waveIn_DataAvailable;
            int sampleRate = 16000; // 8 kHz
            int channels = 1; // mono
            _waveIn.WaveFormat = new WaveFormat(sampleRate, channels);
            _waveIn.StartRecording();
            _isRecording = true;
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            _bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
            //AnalysisData = bufferedWaveProvider;
        }

        private BufferedWaveProvider _bufferedWaveProvider;
        private bool _isRecording;
        private WaveIn _waveIn;
        private async void CreateProfile(object obj)
        {
            AnalysisResult = "Creating profile...";
            var apiResult = await Api.CreateSpeakerProfile();
            AnalysisResult = JsonHelper.FormatJson(await apiResult.Content.ReadAsStringAsync());
        }

        private async void Verify(object obj)
        {
            AnalysisResult = "Verifivation...";
            byte[] data = _bufferedWaveProvider.ToByteArray();
            var apiResult = await Api.RequestVoiceVerification(data, SelectedSpeaker);
            AnalysisResult = JsonHelper.FormatJson(await apiResult.Content.ReadAsStringAsync());
        }

        private async void Enroll(object obj)
        {
            AnalysisResult = "Enrollment...";
            byte[] data = _bufferedWaveProvider.ToByteArray();
            var apiResult = await Api.RequestVoiceEnrollment(data, SelectedSpeaker);
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
