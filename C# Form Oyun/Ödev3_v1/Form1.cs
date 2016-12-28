using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ödev3_v1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();          
        }
        Button[,] b = new Button[8, 8];//Çift boyutlu bir button array tanımladık
        int best=0;
        private void ekranolustur()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    b[i,j] = new Button();
                    b[i,j].Size=new Size(50, 50);
                    b[i, j].Location = new Point(j * 50 + 50, i * 50 + 50);
                    b[i, j].BackColor = Color.FromKnownColor(KnownColor.Control);
                    b[i, j].Margin = new Padding(0);
                    b[i, j].Tag =8*i+j;//Tıkladığımızda o tıklanan butona ulaşa bilmek için her bir butona bir tag degeri atadık. 
                    this.Controls.Add(b[i, j]);//Butonlarımızı form ekranına ekledik.
                    b[i, j].Click += B_Click;
                    
                }
            }

        }
        int x, y;
        int score = 0;
        private void B_Click(object sender, EventArgs e)
        {
            Button tik = (Button)sender;
            score++;
            x = (int)tik.Tag / 8;//x ve y degerlerini tiklanan butonun tag degerinden mod ve bölme işlemi ile aldık
            y = (int)tik.Tag % 8;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (b[i, j].BackColor == Color.Yellow)
                        b[i, j].BackColor = Color.FromKnownColor(KnownColor.Control);//Gitme ihtimalı sarı olan buton reklerini default haline döndür.
                }
            }
            //en iyi ihtimalle 8 olasılık olucagı için 8 ihtimalın gerçekleşme ihtimallerini belirledik ve gidebileceği yolları sarı renk olarak belirledik.
                        if ((7 - y > 1) && (x > 0))
                        {
                            if(b[x - 1, y + 2].BackColor != Color.Red)// Eger önceden gidilmediyse backcolor sarı olucak. 
                               b[x - 1, y + 2].BackColor = Color.Yellow;
                        }

                        if ((7 - y > 1) && (7 - x > 0))
                        {
                            if (b[x + 1, y + 2].BackColor != Color.Red)
                                b[x + 1, y + 2].BackColor = Color.Yellow;
                        }
                        if ((y > 1) && (x > 0))
                        {
                            if (b[x - 1, y - 2].BackColor != Color.Red)
                                b[x - 1, y - 2].BackColor = Color.Yellow;
                        }
                        if ((y > 1) && (7 - x > 0))
                        {
                            if (b[x + 1, y - 2].BackColor != Color.Red)
                                b[x + 1, y - 2].BackColor = Color.Yellow;
                        }

                        if ((x > 1) && (y > 0))
                        {
                            if (b[x - 2, y - 1].BackColor != Color.Red)
                                b[x - 2, y - 1].BackColor = Color.Yellow;
                        }
                        if ((x > 1) && (7 - y > 0))
                        {
                            if (b[x - 2, y + 1].BackColor != Color.Red)
                                b[x - 2, y + 1].BackColor = Color.Yellow;
                        }
                        if ((7 - x > 1) && (y > 0))
                        {
                            if (b[x + 2, y - 1].BackColor != Color.Red)
                                b[x + 2, y - 1].BackColor = Color.Yellow;
                        }
                        if ((7 - x > 1) && (7 - y > 0))
                        {
                            if (b[x + 2, y + 1].BackColor != Color.Red)
                                b[x + 2, y + 1].BackColor = Color.Yellow;
                        }
                    tik.BackColor = Color.Red;
                    tik.Text = score.ToString();//her adımda artan score degerini textbox ve button textinde gösterdik.  
                    stripscore.Text = score.ToString();
                    if(best<score)
                    {
                        best = score;//eger yapılan score bestscoredan iyiyse bestscoru o score yaptık.
                    }
                    if (score == 64)
                            MessageBox.Show("Oyunu Kazandın!!!", "Tebrikler");//kazanma durumundaki mesajımız.
                    ihtimal();
                    if(Bittimi())
                       {
                            MessageBox.Show("Oyun Bitti Score: " + score.ToString(), "Malesef");//Gidecek yer kalmadığında aldığımız mesaj.
                            stripbestscore.Text = best.ToString();
                            aboutToolStripMenuItem.Visible = true;
                       }
        }
        private void ihtimal()//Buttona tıkladığımızda sadece sarı yanan yerlere gidebileceğini belirledik.
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (b[i, j].BackColor == Color.Yellow)
                    {
                        b[i, j].Enabled = true;
                    }
                    else
                    {
                        b[i, j].Enabled = false;
                    }
                    if (b[i, j].BackColor == Color.Red)
                    {
                        b[i, j].Enabled = false;
                    }}}
      }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ekranolustur();
            StripNew.Visible = true;
            playToolStripMenuItem.Visible = false;
        }

        private void StripNew_Click(object sender, EventArgs e)//Yeniden başlamak istenildiğinde Button dizisini boşaltıp tekrardan ekranolustur metodunu çalıştırdık.
        {
            stripscore.Text = "0";
            score = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.Controls.Remove(b[i, j]);
                }
            }
            ekranolustur();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Erdem Özgedik ve Fatih Çelik tarafından ödev amacılığıyla gerçekleştirilmiştir.", "Proje Hakkında");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool Bittimi()//Gidilecek yol kalıp kalmadığını konrol eden metodumuz. 
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (b[i,j].Enabled==true)
                    {
                        return false;
                    }}}
            return true;
        }
    }
}
