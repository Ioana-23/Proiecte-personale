using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Azul_atestat
{
    public partial class FormMeniu : Form
    {
        ComboBox numarJucatoriComboBox = new ComboBox();
        int nrJucatori = 0;
        string[] numeJucatori = new string[4];
        TextBox numarJucatoriTextBox = new TextBox();
        TextBox numeJucatorTextBox = new TextBox();
        TextBox Nume1 = new TextBox();
        TextBox Nume2 = new TextBox();
        TextBox Nume3 = new TextBox();
        TextBox Nume4 = new TextBox();
        RadioButton dificultateMica = new RadioButton();
        //RadioButton dificultateMare = new RadioButton();
        Button confirmare = new Button();
        TextBox dificultate = new TextBox();
        Button nuRegulament = new Button();
        Button daRegulament = new Button();
        TextBox regulamentTextBox = new TextBox();
        Button iesireDinJoc = new Button();
        Button inainte = new Button();
        TextBox regulament = new TextBox();
        int indexDificultate = 0;
        Button inapoi = new Button();

        public FormMeniu()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1191, 740);
            this.MaximumSize = new Size(1191, 740);
            iesireDinJoc.Text = "EXIT";
            iesireDinJoc.BackColor = Color.AntiqueWhite;
            iesireDinJoc.ForeColor = Color.Black;
            iesireDinJoc.Font = new Font(iesireDinJoc.Font.FontFamily, 16);
            iesireDinJoc.Size = new Size(100, 80);
            iesireDinJoc.Location = new Point(20, this.Size.Height - iesireDinJoc.Size.Height - 50);
            this.Controls.Add(iesireDinJoc);
            this.iesireDinJoc.Click += new System.EventHandler(this.IesireDinJoc_Click);

        }

        private void IesireDinJoc_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            this.Controls.Remove(textBox2);
            this.BackgroundImageLayout = ImageLayout.Tile;
            this.BackgroundImage = Azul_atestat.Properties.Resources.Meniu;
            this.Controls.Remove(button1);
            this.Controls.Remove(textBox1);
            numarJucatoriTextBox.Font = new Font(numarJucatoriTextBox.Font.FontFamily, 16);
            numarJucatoriTextBox.Size = new Size(400, 30);
            numarJucatoriTextBox.Location = new Point(150, 150);
            numarJucatoriTextBox.BackColor = Color.AntiqueWhite;
            numarJucatoriTextBox.ForeColor = Color.Black;
            numarJucatoriTextBox.Text = "Introduceti va rog numarul de jucatori: ";
            numarJucatoriTextBox.Enabled = false;
            this.Controls.Add(numarJucatoriTextBox);

            numarJucatoriComboBox.Font = new Font(numarJucatoriComboBox.Font.FontFamily, 16);
            numarJucatoriComboBox.Size = new Size(200, 30);
            numarJucatoriComboBox.Location = new Point(5 * numarJucatoriTextBox.Location.X, 150);
            numarJucatoriComboBox.BackColor = Color.AntiqueWhite;
            numarJucatoriComboBox.ForeColor = Color.Black;
            numarJucatoriComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            numarJucatoriComboBox.Items.Add("2 jucatori");
            numarJucatoriComboBox.Items.Add("3 jucatori");
            numarJucatoriComboBox.Items.Add("4 jucatori");
            
            this.Controls.Add(numarJucatoriComboBox);
            this.numarJucatoriComboBox.SelectedIndexChanged += new System.EventHandler(this.NrJucatori_SelectedIndexChanged);

        }

        private void NrJucatori_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            nrJucatori = numarJucatoriComboBox.SelectedIndex + 2;
            Nume1.Text = null;
            Nume2.Text = null;
            Nume3.Text = null;
            Nume4.Text = null;
            numeJucatori[0] = null;
            numeJucatori[1] = null;
            numeJucatori[2] = null;
            numeJucatori[3] = null;
            //dificultateMica.Checked = false;
            //dificultateMare.Checked = false;
            this.Controls.Remove(Nume1);
            this.Controls.Remove(Nume2);
            this.Controls.Remove(Nume3);
            this.Controls.Remove(Nume4);
           // this.Controls.Remove(dificultateMica);
            //this.Controls.Remove(dificultateMare);
            this.Controls.Remove(dificultate);
            
            numeJucatorTextBox.Font = new Font(numeJucatorTextBox.Font.FontFamily, 16);
            numeJucatorTextBox.Size = new Size(400, 30);
            numeJucatorTextBox.Location = new Point(numarJucatoriTextBox.Location.X, 250);
            numeJucatorTextBox.BackColor = Color.AntiqueWhite;
            numeJucatorTextBox.ForeColor = Color.Black;
            numeJucatorTextBox.Enabled = false;
            numeJucatorTextBox.Text = "Introduceti va rog numele fiecarui jucator: ";
            this.Controls.Add(numeJucatorTextBox);
            switch (nrJucatori)
            {
                case 2: Nume2Jucatori(); break;
                case 3: Nume3Jucatori(); break;
                case 4: Nume4Jucatori(); break;
            }
            Dificultate();
        }
        
        private void Nume2Jucatori()
        {
            MessageBox.Show("Apasati ENTER dupa fiecare nume");
            Nume1.Font = new Font(Nume1.Font.FontFamily, 14);
            Nume1.Size = new Size(300, 30);
            Nume1.Location = new Point(5 * numarJucatoriTextBox.Location.X, 250);
            Nume1.BackColor = Color.AntiqueWhite;
            Nume1.ForeColor = Color.Black;
            Nume2.Font = new Font(Nume2.Font.FontFamily, 14);
            Nume2.Size = new Size(300, 30);
            Nume2.Location = new Point(5 * numarJucatoriTextBox.Location.X, 300);
            Nume2.BackColor = Color.AntiqueWhite;
            Nume2.ForeColor = Color.Black;
            this.Controls.Add(Nume1);
            this.Controls.Add(Nume2);
            this.Nume1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nume1_KeyDown);
            this.Nume2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nume2_KeyDown);
        }
        private void Nume3Jucatori()
        {

            Nume2Jucatori();
            Nume3.Font = new Font(Nume3.Font.FontFamily, 14);
            Nume3.Size = new Size(300, 30);
            Nume3.Location = new Point(5 * numarJucatoriTextBox.Location.X, 350);
            Nume3.BackColor = Color.AntiqueWhite;
            Nume3.ForeColor = Color.Black;
            this.Controls.Add(Nume3);
            this.Nume3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nume3_KeyDown);
        }
        private void Nume4Jucatori()
        {

            Nume3Jucatori();
            Nume4.Font = new Font(Nume4.Font.FontFamily, 14);
            Nume4.Size = new Size(300, 30);
            Nume4.Location = new Point(5 * numarJucatoriTextBox.Location.X, 400);
            Nume4.BackColor = Color.AntiqueWhite;
            Nume4.ForeColor = Color.Black;
            this.Controls.Add(Nume4);
            this.Nume4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nume4_KeyDown);

        }
        private void Dificultate()
        {
            //dificultate.Font = new Font(dificultate.Font.FontFamily, 16);
            dificultate.Size = new Size(400, 30);
            dificultate.Location = new Point(numarJucatoriTextBox.Location.X, 250 + 50 * nrJucatori);
            /*dificultate.BackColor = Color.AntiqueWhite;
            dificultate.ForeColor = Color.Black;
            dificultate.Enabled = false;
            dificultate.Text = "Alegeti va rog nivelul de dificultate: ";*/
            this.Controls.Add(dificultate);
            dificultate.Visible = false;
            /*dificultateMica.Font = new Font(dificultateMica.Font.FontFamily, 12);
            dificultateMica.Text = "Dificultate mica";*/
            dificultateMica.Size = new Size(150, 30);
            dificultateMica.Location = new Point(5 * numarJucatoriTextBox.Location.X, dificultate.Location.Y);
            /*dificultateMica.BackColor = Color.AntiqueWhite;
            dificultateMica.ForeColor = Color.Black;
            dificultateMare.Font = new Font(dificultateMare.Font.FontFamily, 12);
            dificultateMare.Text = "Dificultate mare";
            dificultateMare.Size = new Size(150, 30);
            dificultateMare.Location = new Point(5 * numarJucatoriTextBox.Location.X + dificultateMica.Width + 25, dificultate.Location.Y);
            dificultateMare.BackColor = Color.AntiqueWhite;
            dificultateMare.ForeColor = Color.Black;
            this.Controls.Add(dificultateMare);*/
            this.Controls.Add(dificultateMica);
            dificultateMica.Visible = false;
            /*this.dificultateMica.CheckedChanged += new System.EventHandler(this.DificultateMica_CheckedChanged);
            this.dificultateMare.CheckedChanged += new System.EventHandler(this.DificultateMare_CheckedChanged);*/
        }
        private bool VerificaNumeComplete()
        {
            for(int j = 0; j < nrJucatori; j++)
            {
                if(numeJucatori[j] == null)
                {
                    return false;
                }
            }
            return true;
        }
        private void VerificaDatele()
        {
            
            confirmare.Font = new Font(dificultate.Font.FontFamily, 16);
            confirmare.Size = new Size(400, 30);
            confirmare.Location = new Point(numarJucatoriTextBox.Location.X + numarJucatoriTextBox.Width / 2, dificultateMica.Location.Y + 150);
            confirmare.BackColor = Color.AntiqueWhite;
            confirmare.ForeColor = Color.Black;
            confirmare.Text = "Salvati setarile";
            //confirmare.Enabled = false;
            this.Controls.Add(confirmare);
            this.confirmare.Click += new System.EventHandler(this.Confirmare_Click);
        }
        private void Confirmare_Click(object sender, EventArgs e)
        {
            this.ClearEverything();
            //ClearEverything();
            regulamentTextBox.Font = new Font(dificultate.Font.FontFamily, 24);
            regulamentTextBox.Size = new Size(600, 50);
            regulamentTextBox.Location = new Point(this.Width / 2 - regulamentTextBox.Width / 2, this.Height / 2 - regulamentTextBox.Height - 150);
            regulamentTextBox.BackColor = Color.AntiqueWhite;
            regulamentTextBox.ForeColor = Color.Black;
            regulamentTextBox.Text = "Doriti sa cititi regulamentul?";
            daRegulament.Font = new Font(dificultate.Font.FontFamily, 24);
            daRegulament.Size = new Size(200, 50);
            daRegulament.Location = new Point(regulamentTextBox.Location.X, regulamentTextBox.Location.Y + regulamentTextBox.Height + 100);
            daRegulament.BackColor = Color.AntiqueWhite;
            daRegulament.ForeColor = Color.Black;
            daRegulament.Text = "Da, doresc";
            nuRegulament.Font = new Font(dificultate.Font.FontFamily, 24);
            nuRegulament.Size = new Size(400, 50);
            nuRegulament.Location = new Point(daRegulament.Location.X + daRegulament.Width, regulamentTextBox.Location.Y + regulamentTextBox.Height + 100);
            nuRegulament.BackColor = Color.AntiqueWhite;
            nuRegulament.ForeColor = Color.Black;
            nuRegulament.Text = "Nu, nu doresc";
            this.Controls.Add(regulamentTextBox);
            this.Controls.Add(daRegulament);
            this.Controls.Add(nuRegulament);
            this.nuRegulament.Click += new System.EventHandler(this.StartJoc);
            this.daRegulament.Click += new System.EventHandler(this.Regulament);
        }


        private void StartJoc(object sender, EventArgs e)
        {
            this.Hide();
            Form joc = new FormGame(nrJucatori, numeJucatori);
            joc.Show();

        }
        private void Regulament(object sender, EventArgs e)
        {
            this.Controls.Remove(regulamentTextBox);
            this.Controls.Remove(daRegulament);
            regulament.Size = new Size(700, 300);
            regulament.Location = new Point(this.Width / 2 - regulament.Width / 2, this.Height / 4);
            regulament.BackColor = Color.AntiqueWhite;
            regulament.ForeColor = Color.Black;
            regulament.Font = new Font(regulament.Font.FontFamily, 14);
            regulament.Multiline = true;
            nuRegulament.Text = "START JOC";
            nuRegulament.Size = new Size(100, 80);
            nuRegulament.Location = new Point(this.Width - nuRegulament.Width - 20, iesireDinJoc.Location.Y);
            nuRegulament.Font = new Font(nuRegulament.Font.FontFamily, 14);
            regulament.Text = "OBIECTIVITATEA JOCULUI: Cel care a acumulat cele mai multe puncte la sfarsitul jocului va fi castigatorul. " +
                "Jocul se termina dupa runda in care, cel putin un jucator a completat o linie orizontala a " +
                "peretelui sau cu 5 piese consecutive. \r\n DESFASURAREA JOCULUI: Jocul se desfasoara de-a lungul mai multor runde, fiecare " +
                "dintre acestea fiind compusa din trei faze: Oferta fabricilor, Decorarea peretilor si Pregatirea rundei urmatoare.";
            this.Controls.Add(regulament);
            inainte.Size = new Size(100, 80);
            inainte.Location = new Point(this.Width / 2 + 5, iesireDinJoc.Location.Y);
            inainte.BackColor = Color.AntiqueWhite;
            inainte.ForeColor = Color.Black;
            inainte.Font = new Font(inainte.Font.FontFamily, 16);
            inainte.Text = "Inainte";
            this.Controls.Add(inainte);
            this.inainte.Click += new System.EventHandler(this.Inainte1_Click);

        }

        private void Inainte1_Click(object sender, EventArgs e)
        {
            regulament.Size = new Size(900, 450);
            regulament.Location = new Point(this.Width / 2 - regulament.Width / 2, this.Height / 6);
            regulament.Text = "OFERTA FABRICILOR: Fiecare jucator, pe tura sa, va alege piesele in unul din modurile urmatoare: ia toate piesele " +
                "de aceeasi culoare de pe una din fabrici iar restul pieselor sunt mutate in centrul spatiului de joc; sau ia toate " +
                "piesele de aceeasi culoare din centrul spatiului de joc. Cel care alege primul sa ia piesele din centru, ia de asemenea " +
                "si piesa de marcaj, care este plasata pe primul spatiu liber al podelei, de la stanga la dreapta.\r\nApoi, plasati piesele pe care le-ati luat pe unul din cele 5 randuri de formare de pe " +
                "plansa proprie. Daca randul de formare contine " +
                "deja piese, puteti adauga piese doar de aceeasi culoare pe aceasta. Randul de formare este considerat complet daca " +
                "toate spatiile sale sunt ocupate. Daca ati luat mai multe piese decat numarul liber de spatii din randul de formare " +
                "ales, trebuie sa plasati piesele in plus pe linia podelei.\r\nNu aveti voie sa plasati piese de o anumita culoare pe o linie " +
                "de formare al carui rand corespunzator de pe perete contine o piesa de aceasta culoare. Piesele de pe linia podelei se considera ca au " +
                "cazut pe podea si va scad puncte in faza de decorare. Aceasta faza se incheie atunci cand in nicio fabrica si " +
                "nici in spatiul central al jocului nu mai exista piese.";
            inapoi.Size = new Size(100, 80);
            inapoi.Location = new Point(this.Width / 2 - 5 - inapoi.Width, iesireDinJoc.Location.Y);
            inapoi.BackColor = Color.AntiqueWhite;
            inapoi.ForeColor = Color.Black;
            inapoi.Font = new Font(inapoi.Font.FontFamily, 16);
            inapoi.Text = "Inapoi";
            this.Controls.Add(inapoi);
            this.inapoi.Click += new System.EventHandler(this.Inapoi1_Click);
            this.inainte.Click += new System.EventHandler(this.Inainte2_Click);
        }

        private void Inapoi1_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(inapoi);
            Regulament(sender, e);
        }

        private void Inainte2_Click(object sender, EventArgs e)
        {
            
            regulament.Text = "DECORARE A PERETILOR: Analizati randurile de formare de sus in jos. O piesa de pe o linie completata va fi mutata pe perete. " +
                "Apoi, toate piesele de pe acea linie vor fi eliminate" + 
                "\r\nDupa aceasta operatie, piesele care inca se afla pe randurile de formare, ramana pe plansa pentru runda urmatoare. " +
                "Daca nu exista piese adiacente(pe directie verticala sau orizontala) cu noua piesa plasata, castigati 1 punct pe tabla de " +
                "scor.Daca exista 1 sau mai multe piese conectate orizontal, scorul va fi actualizat astfel: se iau in calcul toate piesele " +
                "conectate, inclusiv cea nou plasata.Apoi, verificati daca exista piese conectate vertical, si se iau in calcul toate aceste " +
                "piese, inclusiv cea noua. In sfarsit, daca aveti piese pe linia podelei, pentru fiecare veti pierde numarul de puncte indicat " +
                "deasupra acestora. Apoi, piesele de pe linia podelei vor fi indepartate.";
            this.inapoi.Click += new System.EventHandler(this.Inapoi2_Click);
            this.inainte.Click += new System.EventHandler(this.Inainte3_Click);

        }
        private void Inapoi2_Click(object sender, EventArgs e)
        {
            Inainte1_Click(sender, e);
        }

        private void Inainte3_Click(object sender, EventArgs e)
        {
            nuRegulament.Location = inainte.Location;
            this.Controls.Remove(inainte);
            regulament.Size = new Size(700, 300);
            regulament.Location = new Point(this.Width / 2 - regulament.Width / 2, this.Height / 4);
            regulament.Text = "PREGATIREA RUNDEI URMATOARE: Daca niciun jucator nu a completata peretele sau o linie orizontala de 5 piese consecutive, jocul continua. Cel care detine " +
                "piesa de marcaj este primul jucator in runda urmatoare. \r\nSFARSITUL JOCULUI: Jocul se termina imediat dupa faza de " +
                "decorare in care cel putin un jucator a completat peretele sau sau o linie orizontala de 5 piese consecutive. " +
                "In plus fata de punctele acordate in faza de Decorare a peretilor, se mai acorda: 2 puncte pentru fiecare linie orizontala " +
                "completa, 7 puncte pentru fiecare linie verticala completa si 10 puncte pentru fiecare culoare din care ati plasat toate cele " +
                "5 piese pe peretele propriu.";
            this.inapoi.Click += new System.EventHandler(this.Inapoi3_Click);

        }

        private void Inapoi3_Click(object sender, EventArgs e)
        {
            nuRegulament.Location = new Point(this.Width - nuRegulament.Width - 20, iesireDinJoc.Location.Y);
            Inainte2_Click(sender, e);
        }


        private void ClearEverything()
        {
            this.Controls.Remove(numarJucatoriComboBox);
            this.Controls.Remove(numarJucatoriTextBox);
            this.Controls.Remove(Nume1);
            this.Controls.Remove(Nume2);
            this.Controls.Remove(Nume3);
            this.Controls.Remove(Nume4);
            this.Controls.Remove(dificultateMica);
           // this.Controls.Remove(dificultateMare);
            this.Controls.Remove(dificultate);
            this.Controls.Remove(confirmare);
            this.Controls.Remove(numeJucatorTextBox);
        }
        /*private void DificultateMica_CheckedChanged(object sender, EventArgs e)
        {
            indexDificultate = 0;
            VerificaDatele();
            
        }
        private void DificultateMare_CheckedChanged(object sender, EventArgs e)
        {
            indexDificultate = 1;
            VerificaDatele();
        }*/
        private void Nume1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                numeJucatori[0] = Nume1.Text;
                if (VerificaNumeComplete())
                {
                    
                        VerificaDatele();
                    
                }
            }
            
        }
        private void Nume2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                numeJucatori[1] = Nume2.Text;
                if (VerificaNumeComplete())
                {
                   
                        VerificaDatele();
                    
                }
            }
            

        }
        private void Nume3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                numeJucatori[2] = Nume3.Text;
                if (VerificaNumeComplete())
                {
                    
                        VerificaDatele();
                    
                }
            }

        }
        private void Nume4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                numeJucatori[3] = Nume4.Text;
                if (VerificaNumeComplete())
                {
                   
                        VerificaDatele();
                    
                }
            }
            

        }

        
    } 
}
