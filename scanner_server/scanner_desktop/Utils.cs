using System.Net;

namespace scanner_desktop
{
    internal static class Utils
    {
        public static void ShowServerConfiguration()
        {
            try
            {
                IPAddress? localIPAddress = GetLocalIPAddress();

                string result = localIPAddress != null
                    ? $"IP adresa: {localIPAddress}\nPort: {Form1.port}"
                    : "Nepodařilo se získat IP adresu.";

                MessageHandling.ShowInfoMessage(result, "Konfigurace serveru");
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage("Chyba při získávání IP adresy: " + ex.Message, "Konfigurace serveru");
            }
        }

        private static IPAddress? GetLocalIPAddress()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ipAddress in hostEntry.AddressList)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ipAddress;
                }
            }

            return null;
        }
    }
}
