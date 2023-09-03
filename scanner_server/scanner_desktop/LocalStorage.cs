using System.IO.Compression;

namespace scanner_desktop
{
    internal static class LocalStorage
    {
        public static void WriteCounter(string filename, int counter)
        {
            try
            {
                using BinaryWriter writer = new(File.Open(filename, FileMode.OpenOrCreate));
                writer.Write(counter);
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage("Chyba při ukládání configurace: " + ex.Message);
            }
        }

        public static int LoadCounter(string filename)
        {
            try
            {
                using BinaryReader reader = new(File.Open(filename, FileMode.Open));
                return reader.ReadInt32();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Chyba při načítání configurace: " + ex.Message);
            }

            return 1;
        }

        public static List<ListViewItem> LoadFile(string path)
        {
            List<ListViewItem> list = new();
            try
            {
                using StreamReader reader = new(path);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] inputData = line.Split(";");
                    if (inputData.Length != 2)
                    {
                        continue;
                    }

                    ListViewItem item = new(inputData[0]);
                    item.SubItems.Add(inputData[1]);

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage("Chyba při načítání databáze: " + ex.Message);
            }

            return list;
        }

        public static void AppendToFile(string path, string data)
        {
            try
            {
                using StreamWriter writer = new(path, true);
                writer.WriteLine(data);
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage("Chyba při ukládání do souboru: " + ex.Message);
            }
        }

        public static void RewriteFile(string path, List<ListViewItem> data)
        {
            try
            {
                using StreamWriter writer = new(path, false);
                foreach (ListViewItem item in data)
                {
                    writer.WriteLine(item.Text + ";" + item.SubItems[1].Text);
                }
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage("Chyba při mazání: " + ex.Message);
            }
        }

        public static void MakeBackup(string pathOriginal, string pathDestination)
        {
            try
            {
                using FileStream sourceStream = new(pathOriginal, FileMode.Open);
                using FileStream compressedStream = File.Create(pathDestination);
                using GZipStream compressionStream = new(compressedStream, CompressionMode.Compress);
                sourceStream.CopyTo(compressionStream);
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage("Nepodařilo se provést zálohu databáze " + ex.Message);
            }
        }
    }
}
