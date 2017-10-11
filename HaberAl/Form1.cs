using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HaberAl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        HaberContext ctx = new HaberContext();

        private void button1_Click(object sender, EventArgs e)
        {
            Kaynak k = new Kaynak();
            k.KaynakURL = textBox1.Text;
            k.SonTarama = DateTime.MinValue;

            ctx.Kaynaklar.Add(k);
            ctx.SaveChanges();
            MessageBox.Show("Kaynak eklendi.");
            KaynakYenile();
        }

        void KaynakYenile()
        {
            listBox1.Text = "";
            listBox1.DataSource = (from x in ctx.Kaynaklar select x).ToList();
            listBox1.DisplayMember = "KaynakURL";
            listBox1.ValueMember = "KaynakID";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KaynakYenile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form cikti = new Form();
            RichTextBox r = new RichTextBox();
            r.Dock = DockStyle.Fill;
            r.ScrollBars = RichTextBoxScrollBars.Vertical; //Dikey kaydırma çubuğu
            cikti.Controls.Add(r); 
            cikti.Show();

            WebClient indiren = new WebClient();
            foreach (var item in listBox1.Items)
            {
                Application.DoEvents();
                Kaynak eleman = (Kaynak)item;
                string gelenXML = indiren.DownloadString(eleman.KaynakURL);

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(gelenXML);
                Application.DoEvents();

                XmlNode katNode = xml.SelectSingleNode("/rss/channel/title");
                Kategori kat = new Kategori();
                kat.KategoriAdi = katNode.InnerText;

                Kategori dbkat = ctx.Kategoriler.Where(a => a.KategoriAdi == kat.KategoriAdi).FirstOrDefault();
                //kategori yoksa null ise if ile ekliyoruz.
                if (dbkat == null)
                {
                    ctx.Kategoriler.Add(kat);
                    ctx.SaveChanges();
                }
                //Varsa veya if de eklediğimiz için dbkat id si ile birlikte geldi. Haber ekleyebilmek için.Artık o kategori var.
                dbkat = ctx.Kategoriler.Where(a => a.KategoriAdi == kat.KategoriAdi).FirstOrDefault();


                //Haberleri almaya başla
                XmlNodeList haberListe = xml.SelectNodes("/rss/channel/item");
                foreach (XmlNode h in haberListe) // h nesnesi : içinde bir habere ait xml var.
                {
                    try
                    {
                        Application.DoEvents();
                        Haber haber = new Haber();
                        haber.Baslik = h.SelectSingleNode("title").InnerText;
                        haber.Aciklama = h.SelectSingleNode("description").InnerText;
                        haber.HaberLink = h.SelectSingleNode("link").InnerText;
                        haber.KategoriID = dbkat.KategoriID;
                        Application.DoEvents();
                        try
                        {
                            //resim hata verirse atla.
                            haber.ResimURL = h.ChildNodes[5].Attributes["url"].Value;
                        }
                        catch { }

                        string gelenTarih = h.SelectSingleNode("pubDate").InnerText;
                        //<pubDate>Thu, 01 Sep 2016 23:18:08 GMT</pubDate>
                        string[] temizlik = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun", "GMT",","};
                        foreach (var t in temizlik)
                        {
                            gelenTarih = gelenTarih.Replace(t,"");
                        }
                        gelenTarih = gelenTarih.Trim();
                        DateTime d = DateTime.MaxValue;
                        DateTime.TryParse(gelenTarih, out d);
                        haber.YayinTarih = d;


                        ctx.Haberler.Add(haber);
                        r.Text += haber.Baslik + " başarıyla eklendi \n";
                    }
                    catch(Exception ex)
                    {
                        r.Text += "HATA: " + ex.Message + "\n \n";
                    }
                }
                ctx.SaveChanges();
            }           
        }
    }
}
