using NAudio.Wave;

namespace WpfApplication1.Plumbing
{
    static class BufferedWaveProviderExtensions
    {
        public static byte[] ToByteArray(this BufferedWaveProvider source)
        {
            byte[] data = new byte[source.BufferedBytes];
            source.Read(data, 0, source.BufferedBytes);

            return data;
        }
    }
}
