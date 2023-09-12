using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace test
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;
        public Form1()
        {
            InitializeComponent();
        }
        void musteriGetir()
        {
            baglanti = new SqlConnection("server=.;Initial Catalog=ticaret;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT * FROM musteri", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            musteriGetir();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtdtarih.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txttel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO musteri(ad,soyad,dtarih,tel) VALUES (@ad,@soyad,@dtarih,@tel)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.Add(new SqlParameter("@ad", txtAd.Text));
            komut.Parameters.Add(new SqlParameter("@soyad", txtSoyad.Text));
            komut.Parameters.Add(new SqlParameter("@dtarih", txtdtarih.Text));
            komut.Parameters.Add(new SqlParameter("@tel", txttel.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            musteriGetir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM musteri WHERE mno=@mno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@mno", Convert.ToInt32(txtNo.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            musteriGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE musteri SET ad=@ad,soyad=@soyad,dtarih=@dtarih,tel=@tel WHERE mno=@mno";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@mno", Convert.ToInt32(txtNo.Text));
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@dtarih", txtdtarih.Value);
            komut.Parameters.AddWithValue("@tel", txttel.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            musteriGetir();


        }
    }
}