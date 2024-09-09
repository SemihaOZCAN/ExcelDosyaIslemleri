using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ExcelIslem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Semiha\Desktop\Bilgisayar_Muhendisligi_Ders_Programi.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;'");

        void listele()
        {
            try
            {
                baglanti.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", baglanti);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Genel hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
        }

        private void buttonListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("INSERT INTO [Sheet1$] (Tarih, Saat, [Ders Başlığı]) VALUES (?, ?, ?)", baglanti);
                komut.Parameters.AddWithValue("?", textBoxTarih.Text);
                komut.Parameters.AddWithValue("?", textBoxSaat.Text);
                komut.Parameters.AddWithValue("?", textBoxDers.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Yeni ders bilgisi eklendi");
                listele();
            }
            catch (OleDbException ex)
            {
                MessageBox.Show("baglantı hatası: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Genel hata: " + ex.Message);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
        }
    }
}
