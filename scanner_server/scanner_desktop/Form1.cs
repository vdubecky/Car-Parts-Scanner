using System.Net;
using System.Net.Sockets;
using System.Text;


namespace scanner_desktop
{
    public partial class Form1 : Form
    {
        public const string configFile = "config.bin";
        public const string backupFolder = "DatabazeDilu";
        public const string backupFileName = "_backup.txt";
        public const string fileName = "db.txt";
        public const int port = 54321;

        private ListViewItem? selectedItem;
        private List<ListViewItem> listViewItemsList;

        private int changeCounter;

        public Form1()
        {
            InitializeComponent();
            InitializeListView();

            changeCounter = 0;

            LoadData();
            StartListening();
        }

        private void InitializeListView()
        {
            listViewItemsList = new();

            listView1.MultiSelect = false;
            listView1.FullRowSelect = true;
        }

        private void StartListening()
        {
            Task.Run(() => ListenForClients());
        }

        private async Task ListenForClients()
        {
            TcpListener tcpListener = new(IPAddress.Any, port);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                using CancellationTokenSource cts = new();
                await ProcessClientAsync(tcpClient, cts.Token);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    bytesRead = 0;

                    try
                    {
                        bytesRead = await clientStream.ReadAsync(message.AsMemory(0, 4096), cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        MessageHandling.ShowErrorMessage(ex.Message);
                        break;
                    }

                    if (bytesRead <= 0)
                    {
                        break;
                    }

                    UTF8Encoding encoder = new();
                    string data = encoder.GetString(message, 0, bytesRead);
                    if (data == "PING")
                    {
                        continue;
                    }

                    LocalStorage.AppendToFile(fileName, data);
                    AddToListView(data);
                }
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage(ex.Message);
            }
            finally
            {
                tcpClient.Close();
                clientStream.Close();
            }
        }

        private void LoadData()
        {
            listViewItemsList = LocalStorage.LoadFile(fileName);
            listView1.Items.AddRange(listViewItemsList.ToArray());
        }

        private void AddToListView(string data)
        {
            string[] inputData = data.Split(";");
            ListViewItem item = new(inputData[0]);
            item.SubItems.Add(inputData[1]);
            listView1.Invoke(new MethodInvoker(delegate
            {
                listView1.Items.Add(item);
                item.Checked = true;

            }));
 
            listViewItemsList.Add(item);
            changeCounter++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Utils.ShowServerConfiguration();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                selectedItem = e.Item;
                SetLabels(e.Item.SubItems[1].Text, e.Item.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageHandling.ShowInfoMessage("Vyberte produkt, který chcete smazat", "Smazání produktu");
                return;
            }

            SetLabels("", "");

            listView1.Items.Remove(selectedItem);
            listViewItemsList.Remove(selectedItem);
            changeCounter++;
            selectedItem = null;

            LocalStorage.RewriteFile(fileName, listViewItemsList);
        }

        private void SetLabels(string name, string code)
        {
            if (name.Length > 40)
            {
                name = string.Concat(name.AsSpan(0, 40), "\n", name.AsSpan(40));
            }

            label1.Text = "Název: " + name;
            label2.Text = "Kód: " + code;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                listView1.Items.Clear();
                listView1.Items.AddRange(listViewItemsList.ToArray());
                return;
            }

            if (nameRadio.Checked)
            {
                SearchByName(textBox1.Text);
            }
            else
            {
                SearchByCode(textBox1.Text);
            }
        }

        private void SearchByName(string name)
        {
            name = name.Trim();
            if (name.Length == 0)
            {
                return;
            }
            listView1.Items.Clear();
            listView1.Items.AddRange(listViewItemsList
                    .Where(item => item.SubItems[1].Text.ToLower().Contains(name.ToLower()))
                    .ToArray());
        }

        private void SearchByCode(string code)
        {
            code = code.Trim();
            if (code.Length == 0)
            {
                return;
            }

            listView1.Items.Clear();
            listView1.Items.AddRange(listViewItemsList
                    .Where(item => item.SubItems[0].Text.ToLower().Contains(code.ToLower()))
                    .ToArray());
        }

        private void codeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (codeRadio.Checked)
            {
                SearchByCode(textBox1.Text);
            }
        }

        private void nameRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (nameRadio.Checked)
            {
                SearchByName(textBox1.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();
            string code = textBox3.Text.Trim();

            if (code.Length != 0 && name.Length != 0)
            {
                if (code.Contains(';') || name.Contains(';'))
                {
                    MessageHandling.ShowErrorMessage("Vstup nesmí obsahovat znak ';'", "Nový produkt");
                    return;
                }

                string data = code + ";" + name;
                LocalStorage.AppendToFile(fileName, data);
                AddToListView(data);

                textBox2.Text = "";
                textBox3.Text = "";
            }
            else
            {
                MessageHandling.ShowErrorMessage("Nìkteré z vstupních polí je prázdné. Vyplòte obì pole a zkuste to znovu.", "Nový produkt");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changeCounter == 0)
            {
                return;
            }

            CreateBackup(LocalStorage.LoadCounter(configFile));
        }

        private void CreateBackup(int counter)
        {
            try
            {
                string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), backupFolder);
                Directory.CreateDirectory(directoryPath);
                string backupPath = Path.Combine(directoryPath, (c % 20).ToString() + "_" + backupFileName);
                counter++;

                LocalStorage.MakeBackup(fileName, backupPath);
                LocalStorage.WriteCounter(configFile, c);
            }
            catch (Exception ex)
            {
                MessageHandling.ShowErrorMessage("Nepodaøilo se vytvoøit zálohu databáze" + ex.Message, "Záloha");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}