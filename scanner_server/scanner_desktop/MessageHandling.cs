namespace scanner_desktop
{
    internal static class MessageHandling
    {
        public static void ShowErrorMessage(string message, string title = "chyba")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfoMessage(string message, string title = "chyba")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
